using Abstracts;
using System.Collections.Generic;

public class EventService
{
    private EnemyChecker _enemyChecker;
    private EventsSystem _eventsSystem;
    private ScoreSystem _scoreSystem;

    private List<Event> _allEvents = new();
    private List<Event> _finishedEvents = new();

    Enemy _boss = null;
    Event _event;
    public EventService(EnemyChecker enemyChecker, EventsSystem eventsSystem, ScoreSystem scoreSystem)
    {
        _enemyChecker = enemyChecker;
        _eventsSystem = eventsSystem;

        _enemyChecker.OnEventShowed += StartEvent;
        _scoreSystem.OnThreshold += InvokeEvent;
    }
    private void StartEvent(Event eventItem)
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
    public void AddEvent(Event eventItem)
    {
        _allEvents.Add(eventItem);
    }
    public void AddFinishedEvent(Event eventItem)
    {
        _finishedEvents.Add(eventItem);
    }
    ~EventService() 
    {
        _scoreSystem.OnThreshold -= InvokeEvent;
        _enemyChecker.OnEventShowed -= StartEvent;
    }
}
