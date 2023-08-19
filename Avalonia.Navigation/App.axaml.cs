using Autofac;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Navigation.Startup;
using Avalonia.Navigation.View;
using Microsoft.Extensions.DependencyInjection;

namespace Avalonia.Navigation
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.BuildServiceProvider();
            
            var bootstrapper = new Bootstrapper(serviceCollection);
            Container = bootstrapper.Bootstrap();
            
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = Container.Resolve<MainWindow>();
            }

            base.OnFrameworkInitializationCompleted();
        }
        
        public IContainer Container { get; private set; }
    }
}