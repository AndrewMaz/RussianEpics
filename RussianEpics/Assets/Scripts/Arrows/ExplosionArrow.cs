using Assets.Scripts.Interfaces;
using SpawnElements;
using UnityEngine;

public class ExplosionArrow : Arrow
{
    [SerializeField] DamageArea _explosionDamageArea;
    [SerializeField] GameObject _arrowParticle;
    [SerializeField] GameObject _explosionParticle;

    private CircleCollider2D _circleCollider;
    private ParticleSystem _particleSystem;

    private const int _baseColliderRadius = 15;
    private const int _baseParticleRadius = 1;
    private new void Awake()
    {
        base.Awake();

        _circleCollider = _explosionDamageArea.GetComponent<CircleCollider2D>();
        _particleSystem = _explosionParticle.GetComponent<ParticleSystem>();
    }
    public void ChangeRadius(float lvl)
    {
        _circleCollider.radius = _baseColliderRadius + lvl;
        var explosionShape = _particleSystem.shape;
        explosionShape.radius = _baseParticleRadius + lvl / 15f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _explosionDamageArea.gameObject.SetActive(true);
        _arrowParticle.SetActive(false);

        isRotating = false;
        _rb.bodyType = RigidbodyType2D.Static;
        _damageArea.gameObject.SetActive(false);

        _explosionDamageArea.IsDamageDealt += OnIsDamageDealt;
    }
}
