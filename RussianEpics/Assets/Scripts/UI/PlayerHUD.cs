using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour, IPlayerInput
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Slider _slider;

    private float currenntHealth;

    private PlayerCharacteristicsService _playerCharacteristics;

    public event Action IsJumped;
    public event Action<Vector2, float> IsShot;
    public event Action IsStance;

    public void Initialize(PlayerCharacteristicsService playerCharacteristics)
    {
        _playerCharacteristics = playerCharacteristics;
    }
    private void Awake()
    {
        currenntHealth = maxHealth;
    }
    private void OnEnable()
    {
        _jumpButton.onClick.AddListener(() => { IsJumped?.Invoke(); });
        _playerCharacteristics.IsPlayerDamaged += Damaged;
    }

    private void OnDisable()
    {
        _jumpButton.onClick.RemoveAllListeners();
        _playerCharacteristics.IsPlayerDamaged -= Damaged;
    }
    private void Damaged(int damage)
    {
        currenntHealth -= damage;
        if (currenntHealth > maxHealth)
        {
            currenntHealth = maxHealth;
        }
        _slider.value = currenntHealth / maxHealth;
    }
}

