using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using Avalonia.Navigation.Event;
using Prism.Commands;
using Prism.Events;

namespace Avalonia.Navigation.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        protected readonly IEventAggregator EventAggregator;
        private string _title;
        private bool _isBusy;
        private bool _isChanged;
        private bool _isValid;

        public DetailViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;

            SaveAsyncCommand = new AsyncCommand(OnSaveExecuteAsync, OnSaveCanExecute, OnException);
            DeleteAsyncCommand = new AsyncCommand(OnDeleteExecuteAsync, OnDeleteCanExecute, OnException);
            CloseDetailViewCommand = new DelegateCommand(CloseDetailViewExecute);
            ResetCommand = new DelegateCommand(OnResetExecute, OnResetCanExecute);
        }

        public bool IsBusy
        {
            get => _isBusy;
            protected set
            {
                if (_isBusy == value)
                    return;
                _isBusy = value;
                OnPropertyChanged();

                InvalidateCommands();
            }
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
                SaveAsyncCommand.RaiseCanExecuteChanged();
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
                SaveAsyncCommand.RaiseCanExecuteChanged();
                ResetCommand.RaiseCanExecuteChanged();
            }
        }

        public int Id { get; protected set; }


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

        public AsyncCommand SaveAsyncCommand { get; private set; }
        public AsyncCommand DeleteAsyncCommand { get; private set; }
        public DelegateCommand CloseDetailViewCommand { get; private set; }
        public DelegateCommand ResetCommand { get; private set; }

        public abstract void LoadAsync(int id);

        protected abstract Task OnSaveExecuteAsync();
        protected abstract Task OnDeleteExecuteAsync();
        protected abstract void OnResetExecute();

        protected abstract bool OnDeleteCanExecute(object arg);
        protected abstract bool OnSaveCanExecute(object arg);
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
            if (IsChanged)
            {
                // var result = await _messageDialogService.ShowOkCancelDialog(this,
                //     $"There are changes that have not yet been saved.{Environment.NewLine}" +
                //     $"If you continue, they will be lost. Continue?", "Work will be lost! Continue?");
                //
                // if (result == MessageDialogResult.Cancel)
                // {
                //     e.Cancel = result == MessageDialogResult.Cancel;
                // }
            }

            EventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Publish(new AfterDetailClosedEventArgs
                {
                    Id = Id,
                    ViewModelName = GetType().Name
                });
        }

        private void InvalidateCommands()
        {
            SaveAsyncCommand.RaiseCanExecuteChanged();
            DeleteAsyncCommand.RaiseCanExecuteChanged();
        }

        private static void OnException(Exception ex)
        {
            // Do something clever with that exception
            throw ex;
        }
    }
}