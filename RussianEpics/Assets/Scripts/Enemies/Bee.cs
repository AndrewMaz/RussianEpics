using Abstracts;
using SpawnElements;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bee : Enemy
{
    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void GetDamage(int damage, object sender)
    {
        base.GetDamage(damage, sender);

        _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor _))
        {
            _rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
