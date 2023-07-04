using Abstracts;
using UnityEngine;

public class BossRune : Rune
{
    [SerializeField] private float _points;

    public override float GetPoints()
    {
        return _points;
    }
}
