using Abstracts;
using SpawnElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrower : Enemy
{
    [SerializeField] private Projectile _projectile;

    private const float _floorSpeed = 3f;
    public override void React()
    {
        StartCoroutine(StartAttack());
    }
    public override void GetDamage(int damage, object sender)
    {
        SetAnimatorTrigger("takeDamage");
        SetRbDinamic();
        ThrowStone();
    }
    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(1.7f, 2.5f));
        ThrowStone();
    }
    private void ThrowStone()
    {
        _projectile.Fall();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Floor _))
        {
            SetRbStatic();
            SwitchColider(false);
            SetSpeed(_floorSpeed);
        }
    }
}
