using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHUD : MonoBehaviour
{
    [SerializeField] GameObject _menuPanel;
    [SerializeField] GameObject _deathText;
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _menuButton;

    PlayerCharacteristicsService[] _playerCharacteristicsService;
    SpeedControlService _speedControlService;
    public void Initialize(PlayerCharacteristicsService[] playerCharacteristicsService, SpeedControlService speedControlService)
    {
        _playerCharacteristicsService = playerCharacteristicsService;
        _speedControlService = speedControlService;
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
    }
    public void Restart()
    {
        /*        Time.timeScale = 1.0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);*/
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        _menuPanel.SetActive(false);
    }
    public void Quit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
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
