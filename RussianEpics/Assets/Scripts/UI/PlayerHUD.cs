using Abstracts;
using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class PlayerHUD : MonoBehaviour, IPlayerInput
{
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _powerSlider;
    [SerializeField] private Transform _runeIcons;
    [SerializeField] private GameObject _runeIcon;
    [SerializeField] private TextMeshProUGUI _percentageText;

    private float _maxHealth;

    private List<GameObject> _runeIconsPool = new();

    private PlayerCharacteristicsService[] _playerCharacteristics;
    private Weapon _weapon;

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
            characteristic.SubscribeToWeapon(RemoveFromHUD);
            characteristic.OnPlayerHealthChange += UpdateHealthBar;
        }
    }
    private void OnDisable()
    {
        _jumpButton.onClick.RemoveAllListeners();

        foreach (var characteristic in _playerCharacteristics)
        {
            characteristic.UnsibscribeToWeapon(RemoveFromHUD);
            characteristic.OnPlayerHealthChange -= UpdateHealthBar;
        }
    }
    private void OnJump()
    {
        IsJumped?.Invoke();
    }
    private void UpdateHealthBar(int currentHealth)
    {
        _healthSlider.value = currentHealth / _maxHealth;
        _percentageText.text = currentHealth.ToString();
    }
    public void UpdatePowerSlider(float value)
    {
        _powerSlider.value = value;
    }
    public void AddToHUD(Rune rune)
    {
        var instance = Instantiate(_runeIcon, _runeIcons);
        if (rune is WeaponRune)
        {
            _runeIconsPool.Add(instance);
            SetSprite(rune, _runeIconsPool.LastOrDefault());
        }
        else
        {
            SetSprite(rune, instance);
            StartCoroutine(RemoveTimedRune(instance));
        }
    }
    private static void SetSprite(Rune rune, GameObject instance)
    {
        instance.GetComponent<Image>().sprite = rune.GetComponentInChildren<SpriteRenderer>().sprite;
    }
    public void RemoveFromHUD()
    {
        Destroy(_runeIconsPool[0]);
        _runeIconsPool.RemoveAt(0);
    }
    public IEnumerator RemoveTimedRune(GameObject instance)
    {
        yield return new WaitForSeconds(3f);

        Destroy(instance);
    }
}

