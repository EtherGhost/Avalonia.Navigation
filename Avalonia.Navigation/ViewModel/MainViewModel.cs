using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using Autofac.Features.Indexed;
using Avalonia.Navigation.Event;
using Prism.Commands;
using Prism.Events;

namespace Avalonia.Navigation.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IDetailViewModel _selectedDetailViewModel;
        private readonly IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private readonly IEventAggregator _eventAggregator;

        public MainViewModel(INavigationViewModel navigationViewModel,
            IIndex<string, IDetailViewModel> detailViewModelCreator,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _detailViewModelCreator = detailViewModelCreator;

            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>().Subscribe(AfterDetailClosed);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            OpenDetailViewCommand = new DelegateCommand<object>(OnOpenDetailViewExecute);

            NavigationViewModel = navigationViewModel;
        }

        public void LoadAsync()
        {
            NavigationViewModel.LoadAsync();
        }

        public void OnClosing(CancelEventArgs e)
        {
            // if (SelectedDetailViewModel != null && SelectedDetailViewModel.IsChanged)
            // {
            //     var result = _messageDialogService.ShowOkCancelDialog(
            //         "There are changes that have not yet been saved. If you continue, they will be lost. " +
            //         "Continue?", "Work will be lost! Continue?");
            //
            //     if (result == MessageDialogResult.Cancel)
            //     {
            //         e.Cancel = result == MessageDialogResult.Cancel;
            //     }
            // }
            //e.Cancel = false;
            
            Closing(e).SafeFireAndForget(OnException);
        }

        public IDetailViewModel SelectedDetailViewModel
        {
            get => _selectedDetailViewModel;
            set
            {
                if (_selectedDetailViewModel == value)
                    return;
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public INavigationViewModel NavigationViewModel { get; }
        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        public DelegateCommand<object> OpenDetailViewCommand { get; }
        public ICommand CreateNewDetailCommand { get; }
        
        private async Task Closing(CancelEventArgs e)
        {
            if (DetailViewModels.Count > 0 && DetailViewModels.Any(d => d is { IsChanged: true, IsValid: true }))
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
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == id
                                       && vm.GetType().Name == viewModelName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }

        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs {ViewModelName = viewModelType.Name});
        }

        private static void OnOpenDetailViewExecute(object args)
        {
        }

        private void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == args.Id
                                       && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                detailViewModel.LoadAsync(args.Id);
                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;
            
            // If async await LoadAsync
            //OpenDetailView(args).SafeFireAndForget(OnException);
        }

        private async Task OpenDetailView(OpenDetailViewEventArgs args)
        {
            /*var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == args.Id
                                       && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel is null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];

                await detailViewModel.LoadAsync(args.Id);

                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;*/
        }

        private static void OnException(Exception ex)
        {
            // Do something clever with that exception
            throw ex;
        }
    }
}