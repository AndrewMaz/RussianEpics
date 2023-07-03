using Abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public event Action<Enemy> IsBossShowed;
    public event Action<NPC> OnEventShowed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.React();
        }
        if (enemy is BossYaga || enemy is BossKolobok)
        {
            IsBossShowed?.Invoke(enemy);
            gameObject.SetActive(false);
        }
        else if (collision.TryGetComponent(out NPC npc))
        {
            OnEventShowed?.Invoke(npc);
        }
    }
}
