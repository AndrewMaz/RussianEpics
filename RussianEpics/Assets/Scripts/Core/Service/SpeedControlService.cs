using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpeedControlService : MonoBehaviour
{
    private float _multiply;

    public event Action? OnSpeedChange;
    public float Multiply
    {   
        get => _multiply;

        private set
        { 
            _multiply = value;
            OnSpeedChange?.Invoke();
        }
    }
    private void Awake()
    {
        StopSpeed();
    }
    public void ChangeSpeed(float duration)
    {
        Multiply = 0.5f;
        StartCoroutine(SpeedChangeDuration(duration));
    }
    public void StopSpeed()
    {
        Multiply = 0f;
    }
    public void ResetSpeed()
    {
        Multiply = 1.0f;
    }
    private IEnumerator SpeedChangeDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        Multiply = 1f;
    }
}
