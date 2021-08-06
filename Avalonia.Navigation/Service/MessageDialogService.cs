namespace Avalonia.Navigation.Service
{
    public class MessageDialogService : IMessageDialogService
    {
#if WPF
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var result = WinUIMessageBox.Show(text, title, MessageBoxButton.OKCancel);
            return result == MessageBoxResult.OK
                ? MessageDialogResult.OK
                : MessageDialogResult.Cancel;
        }
        
        public void ShowInfoDialog(string text)
        {
            WinUIMessageBox.Show(text, "Info");
        }
#else
        
#endif
    }
}