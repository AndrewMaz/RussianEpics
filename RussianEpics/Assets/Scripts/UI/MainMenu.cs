using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image _backGround;
    [SerializeField] private Image _vibrationImage;
    [SerializeField] private float _timeToChange;
    [SerializeField] private Animator _animator;
    [SerializeField] private Sprite _secondImage;
    [SerializeField] private Sprite _vibrationOff;
    [SerializeField] private Sprite _vibrationOn;
    
    public Button startButton;

    private float _changeTime;
    private bool _isVibrationOff = true;

    public event Action IsPlayerChangedToBow;
    public event Action IsPlayerChangedToHammer;

    private void Awake()
    {
        _changeTime = _timeToChange;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            _animator.SetBool("isSkip", true);
        }

        if (Input.touchCount > 0) 
        { 
            _animator.SetBool("isSkip", true);
        }

        _changeTime -= Time.deltaTime;
        if (_changeTime > 0) return;

        _backGround.sprite = _secondImage;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Options()
    {
        _animator.SetTrigger("options");
    }
    public void Back()
    {
        _animator.SetTrigger("back");
    }
    public void Vibration()
    {
        if (_isVibrationOff)
        {
            _vibrationImage.sprite = _vibrationOn;
            _isVibrationOff = false;
        }
        else
        {
            _vibrationImage.sprite = _vibrationOff;
            _isVibrationOff = true;
        }
    }
    public void CharacterSelection()
    {
        _animator.SetTrigger("characterSelection");
    }
    public void ChangeToBowPlayer()
    {
        IsPlayerChangedToBow?.Invoke();
    }
    public void ChangeToHammerPlayer()
    {
        IsPlayerChangedToHammer?.Invoke();
    }
}
