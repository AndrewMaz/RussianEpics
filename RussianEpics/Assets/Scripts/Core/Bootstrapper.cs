using Assets.Scripts.Interfaces.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    private MainMenu _mainMenu;
    private GameplayEntryPoint _gameplayEntryPoint;

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
        _mainMenu.IsWeaponChanged += ChangeWeapon;
        //Gameplay Entry Point
        _gameplayEntryPoint = FindObjectOfType<GameplayEntryPoint>();
    }
    private void OnDisable()
    {
        _mainMenu.IsWeaponChanged -= ChangeWeapon;
    }
    private void StartGame()
    {
        _mainMenu.gameObject.SetActive(false);
    }
    private void ChangeWeapon()
    {
        _gameplayEntryPoint._playerWeapon = _mainMenu.Weapon;
        if (_gameplayEntryPoint._playerWeapon is Quiver)
        {
            _gameplayEntryPoint._hammerModel.SetActive(false);
            _gameplayEntryPoint._animator.SetBool("isQuiver", true);
            _gameplayEntryPoint._animator.SetBool("isHammer", false);
        }
        if (_gameplayEntryPoint._playerWeapon is Hammer)
        {
            _gameplayEntryPoint._hammerModel.SetActive(true);
            _gameplayEntryPoint._animator.SetBool("isQuiver", false);
            _gameplayEntryPoint._animator.SetBool("isHammer", true);
        }
    }
}
