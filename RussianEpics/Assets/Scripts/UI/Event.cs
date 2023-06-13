/*using Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : Enemy
{
    [SerializeField] GameObject _dialogueObject;

    private bool _shouldResetSpeed = false;
    private void Update()
    {
        if (_shouldResetSpeed)
        {
            if (!_dialogueObject.activeInHierarchy)
            {
                _speedControlService.ResetSpeed();
                _shouldResetSpeed = false;
            }
        }
    }
    public override void React()
    {
        _dialogueObject.SetActive(true);
        _shouldResetSpeed = true;
        _speedControlService.StopSpeed();
    }
}*/
