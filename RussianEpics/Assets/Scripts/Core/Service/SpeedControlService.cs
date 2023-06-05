using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpeedControlService : MonoBehaviour
{
    [SerializeField] float _timeToChangeSpeed = 3f;

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
    public void ChangeSpeed()
    {
        Multiply = 0.5f;
        StartCoroutine(SpeedChangeDuration());
    }
    public void StopSpeed()
    {
        Multiply = 0f;
    }
    public void ResetSpeed()
    {
        Multiply = 1.0f;
    }
    private IEnumerator SpeedChangeDuration()
    {
        yield return new WaitForSeconds(_timeToChangeSpeed);

        Multiply = 1f;
    }
}
