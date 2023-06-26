using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHUD : MonoBehaviour
{
    [SerializeField] GameObject _menuPanel;
    [SerializeField] GameObject _deathText;
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _menuButton;
    [SerializeField] Stopwatch _stopwatch;

    PlayerCharacteristicsService[] _playerCharacteristicsService;
    SpeedControlService _speedControlService;

    public event Action IsRestart;
    public event Action IsQuit;
    public void Initialize(PlayerCharacteristicsService[] playerCharacteristicsService, SpeedControlService speedControlService)
    {
        _playerCharacteristicsService = playerCharacteristicsService;
        _speedControlService = speedControlService;
        _menuButton.gameObject.SetActive(true);
        _stopwatch.StartStopwatch();
        enabled = true;
    }
    private void OnEnable()
    {
        foreach (var characteristic in _playerCharacteristicsService)
        {
            characteristic.IsPlayerDead += ShowDeathUI;
        }
    }
    private void OnDisable()
    {
        foreach (var characteristic in _playerCharacteristicsService)
        {
            characteristic.IsPlayerDead += ShowDeathUI;
        }
    }
    public void Pause()
    {
        _menuPanel.SetActive(true);
        _resumeButton.interactable = true;
        _deathText.SetActive(false);
        Time.timeScale = 0f;
        _stopwatch.StopStopwatch();
        _stopwatch.UpdateUI();
    }
    public void Restart()
    {
        Time.timeScale = 1.0f;
        IsRestart?.Invoke();
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        _menuPanel.SetActive(false);
        _stopwatch.StartStopwatch();
    }
    public void Quit()
    {
        Time.timeScale = 1.0f;
        IsQuit?.Invoke();
    }
    private void ShowDeathUI()
    {
        Time.timeScale = 0.1f;
        _menuPanel.SetActive(true);
        _resumeButton.interactable = false;
        _menuButton.gameObject.SetActive(false);
        _deathText.SetActive(true);
        _speedControlService.StopSpeed();
    }
}
