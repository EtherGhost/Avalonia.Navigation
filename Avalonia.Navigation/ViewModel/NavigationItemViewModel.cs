using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Navigation.Event;
using Prism.Commands;
using Prism.Events;

namespace Avalonia.Navigation.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly string _detailViewModelName;
        private string _displayMember;
        
        public NavigationItemViewModel(int id, string displayMember,
            string detailViewModelName,
            IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            
            _eventAggregator = eventAggregator;
            _detailViewModelName = detailViewModelName;
            
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailViewExecute);
        }
        
        public int Id { get; }
        
        public string DisplayMember
        {
            get => _displayMember;
            set
            {
                if (_displayMember == value)
                    return;
                _displayMember = value;
                OnPropertyChanged();
            }
        }
        
        public DelegateCommand OpenDetailViewCommand { get; }
        
        private void OnOpenDetailViewExecute()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Publish(
                    new OpenDetailViewEventArgs
                    {
                        Id = Id,
                        ViewModelName = _detailViewModelName
                    });
        }
    }
}