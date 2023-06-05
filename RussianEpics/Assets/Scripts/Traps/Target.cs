using Assets.Scripts.Interfaces.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Rigidbody2D platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Arrow _) || collision.gameObject.TryGetComponent(out Hammer _))
        {
            platform.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
