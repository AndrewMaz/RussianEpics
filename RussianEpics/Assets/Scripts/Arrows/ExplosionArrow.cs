using Assets.Scripts.Interfaces;
using SpawnElements;
using UnityEngine;

public class ExplosionArrow : Arrow
{
    [SerializeField] DamageArea explosionDamageArea;
    [SerializeField] GameObject arrowParticle;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        explosionDamageArea.gameObject.SetActive(true);
        arrowParticle.SetActive(false);

        isRotating = false;
        _rb.bodyType = RigidbodyType2D.Static;

        explosionDamageArea.IsDamageDealt += OnIsDamageDealt;
    }
}
