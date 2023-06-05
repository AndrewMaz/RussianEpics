using Assets.Scripts.Interfaces.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Weapon _))
        {
            Destroy(collision.gameObject);
        }
    }
}
