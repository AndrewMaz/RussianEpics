using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentTimeText;
    [SerializeField] private float _startSeconds;

    private bool _timerActive = false;
    private float _currentTime;

    public float StartSeconds
    {
        get => _startSeconds;

        set { _startSeconds = value; }
    }

    public event Action OnFinish;
    void Update()
    {
        if (_timerActive)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0 ) 
            {
                TakeAction();
            }
        }
    }
    public void StartTimer()
    {
        _currentTime = _startSeconds;
        _timerActive = true;
    }
    public void StopTimer()
    {
        _timerActive = false;
    }
    public void TakeAction()
    {
        StopTimer();
        _currentTimeText.gameObject.SetActive(false);
        OnFinish?.Invoke();
    }
    public void UpdateUI()
    {
        _currentTimeText.gameObject.SetActive(true);
        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        _currentTimeText.text = time.ToString(@"mm\:ss");
    }
}

