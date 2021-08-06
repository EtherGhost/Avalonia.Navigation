using System;
using System.ComponentModel;
using Autofac;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Navigation.ViewModel;

namespace Avalonia.Navigation.View
{
    public class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        
        public MainWindow()
        {
            var container = ((App) Application.Current).Container;
            _viewModel = container.Resolve<MainViewModel>();
            DataContext = _viewModel;
            InitializeComponent();
            
            Opened += MainWindow_Loaded;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void MainWindow_Loaded(object? sender, EventArgs e)
        {
            _viewModel.LoadAsync();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            _viewModel.OnClosing(e);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}