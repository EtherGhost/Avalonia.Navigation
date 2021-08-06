using Avalonia.Navigation.Event;
using Avalonia.Navigation.Service;
using Prism.Commands;
using Prism.Events;

namespace Avalonia.Navigation.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        protected readonly IEventAggregator EventAggregator;
        protected readonly IMessageDialogService MessageDialogService;
        private string _title;
        private bool _isChanged;
        private bool _isValid;
        private int _id;

        public DetailViewModelBase(IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            EventAggregator = eventAggregator;
            MessageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecuteAsync, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute, OnDeleteCanExecute);
            ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
            CloseDetailViewCommand = new DelegateCommand(CloseDetailViewExecute);
        }

        public bool IsChanged
        {
            get => _isChanged;
            protected set
            {
                if (_isChanged == value)
                    return;
                _isChanged = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
                ResetCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsValid
        {
            get => _isValid;
            protected set
            {
                if (_isValid == value)
                    return;
                _isValid = value;
                OnPropertyChanged();
                SaveCommand.RaiseCanExecuteChanged();
                ResetCommand.RaiseCanExecuteChanged();
            }
        }

        public int Id
        {
            get => _id;
            protected set => _id = value;
        }


        public string Title
        {
            get => _title;
            protected set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand DeleteCommand { get; private set; }
        public DelegateCommand ResetCommand { get; private set; }
        public DelegateCommand CloseDetailViewCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }

        public abstract void LoadAsync(int id);

        protected abstract bool OnDeleteCanExecute();
        protected abstract void OnDeleteExecute();
        protected abstract bool OnSaveCanExecute();
        protected abstract void OnSaveExecuteAsync();
        protected abstract void OnResetExecute();
        protected abstract bool OnResetCanExecute();

        protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Publish(new AfterDetailDeletedEventArgs
            {
                Id = modelId,
                ViewModelName = GetType().Name
            });
        }

        protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember, string tip)
        {
            EventAggregator.GetEvent<AfterDetailSavedEvent>()
                .Publish(new AfterDetailSavedEventArgs
            {
                Id = modelId,
                DisplayMember = displayMember,
                ViewModelName = GetType().Name
            });
        }

        protected virtual void RaiseCollectionSavedEvent()
        {
            EventAggregator.GetEvent<AfterCollectionSavedEvent>()
              .Publish(new AfterCollectionSavedEventArgs
              {
                  ViewModelName = GetType().Name
              });
        }

        protected virtual void CloseDetailViewExecute()
        {
#if WPF
            if (IsChanged)
            {
                var result = MessageDialogService.ShowOkCancelDialog(
                    "There are changes that have not yet been saved. If you continue, they will be lost. " +
                    "Continue?", "Work will be lost! Continue?");
            
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
#else
            // TODO: Implement MessageDialogService for Avalonia.UI
#endif

            EventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Publish(new AfterDetailClosedEventArgs
                {
                    Id = Id,
                    ViewModelName = GetType().Name
                });
        }
    }
}