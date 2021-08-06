using Prism.Events;

namespace Avalonia.Navigation.Event
{
    public class AfterCollectionSavedEvent : PubSubEvent<AfterCollectionSavedEventArgs>
    {

    }
    
    public class AfterCollectionSavedEventArgs
    {
        public string ViewModelName { get; init; }
    }
}