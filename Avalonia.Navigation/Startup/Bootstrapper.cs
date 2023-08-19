using Autofac;
using Autofac.Extensions.DependencyInjection;
using Avalonia.Navigation.View;
using Avalonia.Navigation.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;

namespace Avalonia.Navigation.Startup
{
    public class Bootstrapper
    {
        private readonly ServiceCollection _serviceCollection;

        public Bootstrapper(ServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }
        
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();;
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<ProjectDetailViewModel>().Keyed<IDetailViewModel>(nameof(ProjectDetailViewModel));
            builder.RegisterType<SystemDetailViewModel>().Keyed<IDetailViewModel>(nameof(SystemDetailViewModel));

            builder.Populate(_serviceCollection);
            return builder.Build();
        }
    }
}