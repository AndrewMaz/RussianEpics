using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem
{
    private float _totalScore, _threshold1 = 50f;

    public float TotalScore { get { return _totalScore; } }

    public event Action OnScoreChange, OnThreshold1;
    public void AddPoints(float amount)
    {
        _totalScore += amount;
        OnScoreChange?.Invoke();
        
        if (_totalScore >= _threshold1 ) 
        {
            OnThreshold1?.Invoke();
            _threshold1 = Mathf.Infinity;
        }
    }
    private void Buy()
    {

    }
}
