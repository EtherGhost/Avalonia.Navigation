namespace Avalonia.Navigation.Service
{
    public interface IMessageDialogService
    {
#if WPF
        MessageDialogResult ShowOkCancelDialog(string text, string title);
        
        void ShowInfoDialog(string text);
#else
        
#endif
    }
}