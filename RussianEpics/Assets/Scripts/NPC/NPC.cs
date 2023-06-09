using Abstracts;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class NPC : SpawnElement, IEventable
{
    private Event _eventItem;
    public void SetEventItem(Event eventItem)
    {
        _eventItem = eventItem;
    }
    public Event GetEvent()
    {
        return _eventItem;
    }
}
