using Abstracts;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class Ghoul : Enemy
{
    [SerializeField] Animator animator;

    protected override void OnIsDamageDealt(IDamageable target)
    {
        Debug.Log(nameof(Ghoul) + "Attacked");
        base.OnIsDamageDealt(target);

        animator.SetTrigger("attack");
    }
}
