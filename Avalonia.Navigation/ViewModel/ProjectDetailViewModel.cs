using System.Threading.Tasks;
using Avalonia.Navigation.Service;
using Prism.Events;

namespace Avalonia.Navigation.ViewModel
{
    public class ProjectDetailViewModel : DetailViewModelBase
    {
        public ProjectDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) 
            : base(eventAggregator, messageDialogService)
        {
        }         

        public override void LoadAsync(int id)
        {
            Id = id;
            Title = $"ProjectDetailView ({id})";
        }

        protected override Task OnSaveExecuteAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override Task OnDeleteExecuteAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnResetExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override bool OnSaveCanExecute(object arg)
        {
            return false;
        }

        protected override bool OnDeleteCanExecute(object arg)
        {
            return false;
        }

        protected override bool OnResetCanExecute()
        {
            return false;
        }
    }
}