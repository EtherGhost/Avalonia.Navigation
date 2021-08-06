using Avalonia.Navigation.Service;
using Prism.Events;

namespace Avalonia.Navigation.ViewModel
{
    public class SystemDetailViewModel : DetailViewModelBase
    {
        public SystemDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
            : base(eventAggregator, messageDialogService)
        {
        }

        public override void LoadAsync(int id)
        {
            Id = id;
            Title = $"SystemDetailView ({id})";
        }

        protected override bool OnDeleteCanExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnDeleteExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnSaveExecuteAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnResetExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override bool OnResetCanExecute()
        {
            throw new System.NotImplementedException();
        }
    }
}