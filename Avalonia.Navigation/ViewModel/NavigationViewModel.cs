using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Navigation.Event;
using Prism.Events;

namespace Avalonia.Navigation.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public NavigationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            
            Projects = new ObservableCollection<NavigationItemViewModel>();
            Systems = new ObservableCollection<NavigationItemViewModel>();
        }

        public void LoadAsync()
        {
            Projects.Clear();
            Systems.Clear();
            
            Projects.Add(
                new NavigationItemViewModel(
                    1, "Project 1",
                    nameof(ProjectDetailViewModel),
                    _eventAggregator));
            Projects.Add(
                new NavigationItemViewModel(
                    2, "Project 2",
                    nameof(ProjectDetailViewModel),
                    _eventAggregator));
            Projects.Add(
                new NavigationItemViewModel(
                    3, "Project 3",
                    nameof(ProjectDetailViewModel),
                    _eventAggregator));
            
            Systems.Add(
                new NavigationItemViewModel(
                    1, "System 1",
                    nameof(SystemDetailViewModel),
                    _eventAggregator));
            Systems.Add(
                new NavigationItemViewModel(
                    2, "System 2",
                    nameof(SystemDetailViewModel),
                    _eventAggregator));
            Systems.Add(
                new NavigationItemViewModel(
                    3, "System 3",
                    nameof(SystemDetailViewModel), 
                    _eventAggregator));
        }
        
        public ObservableCollection<NavigationItemViewModel> Projects { get; }
        public ObservableCollection<NavigationItemViewModel> Systems { get; }
        
        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(ProjectDetailViewModel):
                    AfterDetailDeleted(Projects, args);
                    break;
                case nameof(SystemDetailViewModel):
                    AfterDetailDeleted(Systems, args);
                    break;
            }
        }
        
        private static void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items,
            AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id);
            if (item != null)
            { 
                items.Remove(item);
            }
        }
        
        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(ProjectDetailViewModel):
                    AfterDetailSaved(Projects, args);
                    break;
                case nameof(SystemDetailViewModel):
                    AfterDetailSaved(Systems, args);
                    break;
            }
        }
        
        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items,
            AfterDetailSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                items.Add(new NavigationItemViewModel(args.Id, args.DisplayMember,
                    args.ViewModelName,
                    _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }
        
        private static void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
        }
    }
}