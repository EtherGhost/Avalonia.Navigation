using Prism.Events;

namespace Avalonia.Navigation.Event
{
    public class AfterDetailDeletedEvent : PubSubEvent<AfterDetailDeletedEventArgs>
    {
    }
    
    public class AfterDetailDeletedEventArgs
    {
        public int Id { get; init; }
        public string ViewModelName { get; init; }
    }
}