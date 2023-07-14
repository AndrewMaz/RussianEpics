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
        _startSpeed = Speed;
    }
    public override void React()
    {
        if (!IsAlive) return;

        SetAnimatorTrigger("attack");
        SetSpeed(0f);
        StartCoroutine(StartAttack());
    }
    public override void GetDamage(int damage, object sender)
    {
        SetAnimatorTrigger("takeDamage");
        AddColiderOffset(Vector2.up);
        SetRbDinamic();
        StopAllCoroutines();
        SetSpeed(_startSpeed);
        SetDead();
        AddPoints(this);
    }
    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(1f);
        SetSpeed(newSpeed);
        AddColiderOffset(Vector2.down);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor _))
        {
            SetRbStatic();
            SwitchColider(false);
        }
    }
}
