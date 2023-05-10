using Assets.Scripts.Interfaces;
using Assets.Scripts.Interfaces.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField] private DamageArea _damageArea;
    [SerializeField] Hammer _hammerPrefab;

    private readonly Queue<WeaponRune> _hammer = new();

    private readonly int damage = 2;

    const float angleOffset = 90f;

    protected Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _damageArea.IsDamageDealt += OnIsDamageDealt;
    }

    private void OnDisable()
    {
        _damageArea.IsDamageDealt -= OnIsDamageDealt;
    }

    protected virtual void OnIsDamageDealt(IDamageable target)
    {
        target.GetDamage(damage, this);
    }
    public override void ApplyRune(WeaponRune rune)
        => _hammer.Enqueue(rune);
    protected override bool CanShoot(Vector2 firePosition, float time)
        => firePosition.x - transform.position.x > ShootingMinDistance && time > ShootingDelay;
    public override void Attack(Vector2 firePosition, float force)
    {
        if (!CanShoot(firePosition, force))
            return;

        if (_hammer.Count > 0)
        {
            var arrowRune = _hammer.Dequeue();

            switch (arrowRune)
            {
                //hammer rune attacks
                case ExplosionRune rune:
                    //shatter.SetActive(true);
                    if (force == 1)
                    {
                        ThrowHammer(firePosition);
                    }
                    break;

                case TripleShotRune rune:
                    if (force == 1)
                    {
                        ThrowHammer(firePosition + Vector2.up);
                        ThrowHammer(firePosition);
                        ThrowHammer(firePosition + Vector2.down);
                    }
                    break;
            }
        }
        else
        {
            if (force == 1)
            {
                ThrowHammer(firePosition);
            }
        }
    }
    private void ThrowHammer(Vector2 firePosition)
    {
        var instance = Instantiate(_hammerPrefab);

        instance.transform.localScale = gameObject.transform.localScale;
        instance.Fly(transform.position, firePosition);
    }
    public void Fly(Vector2 from, Vector2 to)
    {
        transform.position = from + new Vector2(0, 1);
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0.0f;

        Vector2 fireVector = to - from;
        Vector2 groundVector = new Vector2(0, from.y) - from;
        var angle = Vector2.Angle(fireVector, groundVector);

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - angleOffset);
        _rb.AddForce(fireVector.normalized * Force);
    }
}
