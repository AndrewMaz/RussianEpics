using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour, IPlayerInput
{
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _powerSlider;

    private float _maxHealth;

    private PlayerCharacteristicsService[] _playerCharacteristics;

    public event Action IsJumped;
    public event Action<Vector2, float> IsShot;
    public event Action IsStance;

    public void Initialize(PlayerCharacteristicsService[] playerCharacteristics, int maxHealth)
    {
        _playerCharacteristics = playerCharacteristics;
        _maxHealth = maxHealth;
        enabled = true;

    }

    private void OnEnable()
    {
        _jumpButton.onClick.AddListener(OnJump);
        foreach (var characteristic in _playerCharacteristics) 
        {
            characteristic.IsPlayerDamaged += Damaged;
        }
    }
    private void OnDisable()
    {
        _jumpButton.onClick.RemoveAllListeners();
        foreach (var characteristic in _playerCharacteristics)
        {
            characteristic.IsPlayerDamaged -= Damaged;
        }
    }
    private void OnJump()
    {
        IsJumped?.Invoke();
    }
    private void Damaged(int currentHealth)
    {
        _healthSlider.value = currentHealth / _maxHealth;
    }
    public void UpdatePowerSlider(float value)
    {
        _powerSlider.value = value;
    }
}

