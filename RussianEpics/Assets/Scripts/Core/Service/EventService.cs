using Abstracts;
using System.Collections.Generic;

public class EventService
{
    private EventsSystem _eventsSystem;
    private ScoreSystem _scoreSystem;

    private List<Event> _allEvents = new();
    private List<Event> _finishedEvents = new();

    Event _event;
    public EventService(EventsSystem eventsSystem, ScoreSystem scoreSystem, Event[] events)
    {
        _eventsSystem = eventsSystem;
        _scoreSystem = scoreSystem;
        AddEvents(events);

        _scoreSystem.OnThreshold += InvokeEvent;
    }
    private void StartDialogue(Event eventItem)
    {
        _event = eventItem;
        _eventsSystem.SetDialogue(eventItem);
    }
    private void EndEvent()
    {       
        _eventsSystem.SetDialogue(_event);
    }
    public void InvokeEvent()
    {
        foreach (var eventItem in _allEvents)
        {
            if (_finishedEvents.Contains(eventItem))
            {
                continue;
            }

            eventItem.Start();
            break;
        }
    }
    private void ResetList()
    {
        _finishedEvents.Clear();
    }
    public void AddEvents(Event[] eventArray)
    {
        foreach (var eventItem in eventArray)
        {
            _allEvents.Add(eventItem);
        }
    }
    public void AddFinishedEvent(Event eventItem)
    {
        _finishedEvents.Add(eventItem);
    }
    ~EventService() 
    {
        _scoreSystem.OnThreshold -= InvokeEvent;
    }
}
