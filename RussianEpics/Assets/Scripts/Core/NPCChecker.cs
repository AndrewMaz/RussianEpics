using Assets.Scripts.Interfaces;
using UnityEngine;

public class NPCChecker : MonoBehaviour
{
    private EventService _eventService;

    public void Initialize(EventService eventService) 
    { 
        _eventService = eventService;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out NPC npc))
        {
            _eventService.StartEvent(npc);
        }
    }
}
