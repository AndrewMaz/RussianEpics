using System;
using System.Linq;

public class ScoreSystem
{
    private float[] _thresholds;

    private float _totalScore; 

    public float TotalScore { get { return _totalScore; } }

    public event Action OnScoreChange, OnThreshold;

    private int counter = 0;

    public ScoreSystem(float[] thresholds)
    {
        _thresholds = thresholds;
    }
    public void AddPoints(float amount)
    {
        _totalScore += amount;
        OnScoreChange?.Invoke();
        
        if (counter <= _thresholds.Count() - 1 && _totalScore >= _thresholds[counter]) 
        {
            OnThreshold?.Invoke();
            counter++;
        }
    }
    private void Buy()
    {

    }
}
