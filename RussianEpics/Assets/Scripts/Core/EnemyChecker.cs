using Abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public event Action<Enemy> IsBossShowed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.React();
        }
        if (collision.TryGetComponent(out BossYaga bossYaga))
        {
            IsBossShowed?.Invoke(bossYaga);
            gameObject.SetActive(false);
        }
        if (collision.TryGetComponent(out BossKolobok bossKolobok))
        {
            IsBossShowed?.Invoke(bossKolobok);
            gameObject.SetActive(false);
        }
    }
}
