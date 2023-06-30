using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHUD : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Stopwatch _stopwatch;
    [SerializeField] private TextMeshProUGUI _finalScoreText;

    private PlayerCharacteristicsService[] _playerCharacteristicsService;
    private ScoreSystem _scoreSystem;

    public event Action IsRestart;
    public event Action IsQuit;
    public void Initialize(PlayerCharacteristicsService[] playerCharacteristicsService, ScoreSystem scoreSystem)
    {
        _playerCharacteristicsService = playerCharacteristicsService;
        _menuButton.gameObject.SetActive(true);
        _stopwatch.StartStopwatch();
        _scoreSystem = scoreSystem;

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
            characteristic.IsPlayerDead -= ShowDeathUI;
        }
    }
    public void Pause()
    {
        _menuPanel.SetActive(true);
        Time.timeScale = 0f;
        _stopwatch.StopStopwatch();
        _stopwatch.UpdateUI();
    }
    public void Restart()
    {
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
        _deathPanel.SetActive(true);
        _finalScoreText.text = "Î×ÊÈ: \n" + _scoreSystem.TotalScore.ToString();         // + Environment.NewLine
        _menuButton.gameObject.SetActive(false);
    }
}
