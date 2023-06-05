using Abstracts;
using System;
using UnityEngine;

public class HealthRune : Rune
{
    [SerializeField] int healAmount;

    public int HealAmount { get { return healAmount; } private set {; } }
}
