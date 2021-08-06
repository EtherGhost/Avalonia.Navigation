using Prism.Events;

namespace Avalonia.Navigation.Event
{
    public class AfterDetailClosedEvent : PubSubEvent<AfterDetailClosedEventArgs>
    {
    }
    
    public class AfterDetailClosedEventArgs
    {
        public int Id { get; init; }
        public string ViewModelName { get; init; }
    }
}