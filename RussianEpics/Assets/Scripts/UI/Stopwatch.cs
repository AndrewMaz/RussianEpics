using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _currentTimeText;

    private bool _stopwatchActive = false;
    private float _currentTime;

    void Start()
    {
        _currentTime = 0;
    }
    void Update()
    {
        if (_stopwatchActive)
        {
            _currentTime += Time.deltaTime;
        }
    }
    public void StartStopwatch()
    {
        _stopwatchActive = true;
        _currentTimeText.gameObject.SetActive(false);
    }
    public void StopStopwatch()
    {
        _stopwatchActive = false;
    }
    public void UpdateUI()
    {
        _currentTimeText.gameObject.SetActive(true);
        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        _currentTimeText.text = time.ToString(@"mm\:ss\:ff");
    }
}
