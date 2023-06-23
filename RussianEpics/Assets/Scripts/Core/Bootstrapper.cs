using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    private MainMenu _mainMenu;
    private GameplayEntryPoint _gameplayEntryPoint;
    private MenuHUD _menuHUD;

    private bool _isRestarted = false, _isHammerPlayer = false;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadScene(1);
    }
    private void Start()
    {
        //main Menu
         _mainMenu = FindObjectOfType<MainMenu>();
        _mainMenu.startButton.onClick.AddListener(StartGame);
        _mainMenu.IsPlayerChangedToBow += ChangePlayerToBow;
        _mainMenu.IsPlayerChangedToHammer += ChangePlayerToHammer;
        //MenuHUD
        _menuHUD = FindObjectOfType<MenuHUD>();
        _menuHUD.IsRestart += RestartGame;
        _menuHUD.IsQuit += QuitToMainMenu;
        //Entry point
        _gameplayEntryPoint = FindObjectOfType<GameplayEntryPoint>();
        if (_isRestarted)
        {
            _isRestarted = false;
/*            if (_isHammerPlayer)
            {
                ChangePlayerToHammer();
            }*/

            StartGame();
        }
    }
    private void OnDisable()
    {
        _mainMenu.IsPlayerChangedToBow -= ChangePlayerToBow;
        _mainMenu.IsPlayerChangedToHammer -= ChangePlayerToHammer;
        _menuHUD.IsRestart -= RestartGame;
        _menuHUD.IsQuit -= QuitToMainMenu;
    }
    private void StartGame()
    {
        _mainMenu.gameObject.SetActive(false);

        _gameplayEntryPoint.Initialize();
    }
    private void RestartGame()
    {
        _isRestarted = true;
        //_isHammerPlayer = _gameplayEntryPoint.IsHammerPlayerActive();

        StartCoroutine(LoadSceneWithWait());
        StartCoroutine(WaitForSceneElements());
    }
    private void QuitToMainMenu()
    {
        StartCoroutine(LoadSceneWithWait());
        StartCoroutine(WaitForSceneElements());
    }
    private void ChangePlayerToBow()
    {
        _gameplayEntryPoint.SwitchPlayer(0);
    }
    private void ChangePlayerToHammer()
    {
        _gameplayEntryPoint.SwitchPlayer(1);
    }
    IEnumerator LoadSceneWithWait()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    IEnumerator WaitForSceneElements()
    {
        yield return new WaitForSeconds(1f);
        Start();
    }
}
