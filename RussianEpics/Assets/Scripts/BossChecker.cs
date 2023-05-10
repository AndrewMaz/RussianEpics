using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChecker : MonoBehaviour
{
    public event Action<BossYaga> IsBossShowed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BossYaga bossYaga))
        {
            IsBossShowed?.Invoke(bossYaga);
            gameObject.SetActive(false);
        }
    }
}
