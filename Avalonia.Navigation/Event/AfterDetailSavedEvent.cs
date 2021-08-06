using Prism.Events;

namespace Avalonia.Navigation.Event
{
    public class AfterDetailSavedEvent : PubSubEvent<AfterDetailSavedEventArgs>
    {
    }
    
    public class AfterDetailSavedEventArgs
    {
        public int Id { get; init; }
        public string DisplayMember { get; init; }
        public string ViewModelName { get; init; }
    }
}