using Assets.Scripts.Interfaces;
using Assets.Scripts.Interfaces.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField] private DamageArea _damageArea;
    [SerializeField] private DamageArea _shatterDamageArea;
    [SerializeField] private Hammer _hammerPrefab;
    [SerializeField] private GameObject _shatter;

    private readonly Queue<WeaponRune> _hammerQueue = new();

    private readonly int damage = 2;

    const float angleOffset = 90f;

    protected Rigidbody2D _rb;

    private bool _isThrowCD = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _damageArea.IsDamageDealt += OnIsDamageDealt;
        if (_shatterDamageArea != null)
        {
            _shatterDamageArea.IsDamageDealt += OnIsDamageDealt;
            _shatter.SetActive(false);
        }
    }
    private void OnDisable()
    {
        _damageArea.IsDamageDealt -= OnIsDamageDealt;
        if (_shatterDamageArea != null)
        {
            _shatterDamageArea.IsDamageDealt -= OnIsDamageDealt;
        }
    }
    protected virtual void OnIsDamageDealt(IDamageable target)
    {
        target.GetDamage(damage, this);
    }
    public override void ApplyRune(WeaponRune rune)
        => _hammerQueue.Enqueue(rune);
    protected override bool CanShoot(Vector2 firePosition, float time)
        => firePosition.x - transform.position.x > ShootingMinDistance && time > ShootingDelay;
    public override void Attack(Vector2 firePosition, float force)
    {
        if (!CanShoot(firePosition, force))
            return;

        if (_hammerQueue.Count > 0)
        {
            var arrowRune = _hammerQueue.Dequeue();
            DequeueInvoke();

            switch (arrowRune)
            {
                //hammer rune attacks
                case ExplosionRune _:
                    _shatter.SetActive(true);
                    StartCoroutine(ShatterLifeTime());
                    break;

                case TripleShotRune _:
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
            if (force == 1 && !_isThrowCD)
            {
                ThrowHammer(firePosition);
                StartCoroutine(ThrowCD());
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
    private IEnumerator ShatterLifeTime()
    {
        yield return new WaitForSeconds(1f);

        _shatter.SetActive(false);
    }
    private IEnumerator ThrowCD()
    {
        _isThrowCD = true;
        yield return new WaitForSeconds(4f);

        _isThrowCD = false;
    }
}
