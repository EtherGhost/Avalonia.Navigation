using Prism.Events;

namespace Avalonia.Navigation.Event
{
    public class OpenDetailViewEvent : PubSubEvent<OpenDetailViewEventArgs>
    {
    }
    
    public class OpenDetailViewEventArgs
    {
        public int Id { get; init; }
        public string ViewModelName { get; init; }
    }
}