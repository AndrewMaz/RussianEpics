using Abstracts;
using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;


public class Quiver : Weapon
{
    [SerializeField] private Arrow _arrow;

    private readonly Queue<WeaponRune> _quiverQueue = new ();

    public override void Attack(Vector2 firePosition, float time)
    {
        if (!CanShoot(firePosition, time))
           return;

        if(_quiverQueue.Count > 0)
        {
            var arrowRune = _quiverQueue.Dequeue();
            DequeueInvoke();
          
            switch(arrowRune) 
            {
                case ExplosionRune rune:
                    InstantiateArrow(firePosition, Force * time, rune.arrow.gameObject, rune);
                    break;

                case TripleShotRune rune:
                    RepeatAction(firePosition, time, rune, GetLvl(rune));
                    break;
            }
        }
        else
        {
            InstantiateArrow(firePosition, Force * time, _arrow.gameObject);
        }
    }
    protected override bool CanShoot(Vector2 firePosition, float time)
        => firePosition.x - transform.position.x > ShootingMinDistance && time > ShootingDelay;
    public override void ApplyRune(WeaponRune rune)
        => _quiverQueue.Enqueue(rune);
    private void InstantiateArrow(Vector2 firePosition, float force, GameObject arrowPrefab)
    {
        var instance = Instantiate(arrowPrefab, gameObject.transform.position, gameObject.transform.rotation);
        if (instance.TryGetComponent(out Arrow arrow))
            arrow.Fly(transform.position, firePosition, force);
    }
    private void InstantiateArrow(Vector2 firePosition, float force, GameObject arrowPrefab, WeaponRune rune)
    {
        var instance = Instantiate(arrowPrefab, gameObject.transform.position, gameObject.transform.rotation);
        if (instance.TryGetComponent(out Arrow arrow))
            arrow.Fly(transform.position, firePosition, force);
        if (instance.TryGetComponent(out ExplosionArrow explosionArrow))
            explosionArrow.ChangeRadius(GetLvl(rune));
    }
    private void RepeatAction(Vector2 firePosition, float time, WeaponRune rune, int counter)
    {
        for (int i = counter; i >= -counter; i--)
            InstantiateArrow(firePosition + Vector2.down * i, Force * time, rune.arrow.gameObject);
    }
}
