using Abstracts;
using System;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public event Action<Enemy> IsBossShowed;
    public event Action<NPC> OnEventShowed;
    private EventService _eventService;

    public void Initialize(EventService eventService)
    {
        _eventService= eventService;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.React();

            if (enemy is Boss boss)
            {
                IsBossShowed?.Invoke(enemy);
                _eventService.StartEvent(boss);
                gameObject.SetActive(false);
            }
        }
    }
}