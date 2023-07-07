using Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthRune : Rune
{
    [SerializeField] private int _healthAmout;

    public int HealthAmount { get { return _healthAmout; } private set {; } }
}
