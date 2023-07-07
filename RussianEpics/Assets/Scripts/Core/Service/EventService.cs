using Abstracts;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;

public class EventService
{
    private DialogueSystem _dialogueSystem;
    private ScoreSystem _scoreSystem;

    private List<Event> _allEvents = new();
    private List<Event> _finishedEvents = new();

    private Event _event;
    public EventService(DialogueSystem dialogueSystem, ScoreSystem scoreSystem)
    {
        _dialogueSystem = dialogueSystem;
        _scoreSystem = scoreSystem;

        //_scoreSystem.OnThreshold += InvokeEvent;
    }
    public void StartEvent(IEventable eventItem)
    {
        var newEvent = eventItem.GetEvent();
        if (newEvent == null) 
            return;

        _event = newEvent;
        _event.Start();
    }
    private void EndEvent()
    {       
        _dialogueSystem.SetDialogue(_event);
    }
/*    public void InvokeEvent()
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
    }*/
    private void ResetList()
    {
        _finishedEvents.Clear();
    }
    public void AddFinishedEvent(Event eventItem)
    {
        _finishedEvents.Add(eventItem);
    }
/*    ~EventService() 
    {
        _scoreSystem.OnThreshold -= InvokeEvent;
    }*/
}
