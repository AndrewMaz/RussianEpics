using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem
{
    private int _totalScore;
    public void AddPoints(int amount)
    {
        _totalScore += amount;
        Debug.Log("[ScoreSytem]: AddPoints " + _totalScore);
    }
    private void Buy()
    {

    }
}
