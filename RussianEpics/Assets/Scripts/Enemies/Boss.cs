using Abstracts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Boss : Enemy, IEventable
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
