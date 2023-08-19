using System;
using System.ComponentModel;
using Autofac;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Navigation.ViewModel;

namespace Avalonia.Navigation.View
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        
        public MainWindow()
        {
            var container = ((App) Application.Current).Container;
            _viewModel = container.Resolve<MainViewModel>();
            DataContext = _viewModel;
            InitializeComponent();
            
            Opened += MainWindow_Loaded;
            Closing += MainWindow_Closing;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void MainWindow_Closing(object? sender, WindowClosingEventArgs e)
        {
            _viewModel.OnClosing(e);
        }

        private void MainWindow_Loaded(object? sender, EventArgs e)
        {
            _viewModel.LoadAsync();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}