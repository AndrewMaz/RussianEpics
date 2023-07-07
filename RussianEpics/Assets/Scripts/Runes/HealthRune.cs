using Abstracts;
using System;
using UnityEngine;

public class HealthRune : Rune
{
    [SerializeField] private int _healAmount;

    public int HealAmount { get { return _healAmount; } private set {; } }
}
