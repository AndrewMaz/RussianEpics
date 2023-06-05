using Abstracts;
using SpawnElements;
using System.Collections;
using UnityEngine;

public class Bird : Enemy
{
    [SerializeField] private float newSpeed = 12f;

    private float _startSpeed;

    private void Start()
    {
        _startSpeed = _motor.Speed;
    }
    public override void React()
    {
        _animator.SetTrigger("attack");
        _motor.Speed = 0f;
        StartCoroutine(StartAttack());
    }
    public override void GetDamage(int damage, object sender)
    {
        _animator.SetTrigger("takeDamage"); 
        _capsuleCollider.offset += Vector2.up;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _motor.Speed = _startSpeed;
    }
    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(1f);
        _motor.Speed = newSpeed;
        _capsuleCollider.offset += Vector2.down;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor _))
        {
            _rb.bodyType = RigidbodyType2D.Static;
            _capsuleCollider.enabled = false;
        }
    }
}
