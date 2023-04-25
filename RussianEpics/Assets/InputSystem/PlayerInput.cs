using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player _player;

    private SchemePlayerInput _input;

    private void Awake()
    {
        _input = new SchemePlayerInput();

        _input.Player.Jump.performed += context => Jump();
    }

    public void Jump()
    {
        _player.Jump();
    }

    private void OnEnable()
    {
        //_input.Player.Jump.performed += OnJumpPerformed;
        _input.Enable();
    }

/*    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        _player.Jump();
    }*/

    private void OnDisable()
    {
        //_input.Player.Jump.performed -= OnJumpPerformed;
        _input.Disable();
    }
}
