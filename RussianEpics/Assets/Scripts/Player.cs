using Assets.Scripts.Interfaces;
using SpawnElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _floorMask;

    private int health = 100;
    private Rigidbody2D rb;

    private const float _rayDistance = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {  
        if (Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _floorMask))
        {
            rb.AddForce(new Vector2(0, _jumpForce));
        }
    }

    public void GetDamage(int damage)
    {
        Debug.Log(health -= damage);
    }
}
