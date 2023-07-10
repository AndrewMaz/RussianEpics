using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunner : MonoBehaviour
{
    [SerializeField] private Bootstrapper _bootstrapper;
    private void Awake()
    {
        if (FindObjectOfType<Bootstrapper>() != null) return;

        Instantiate(_bootstrapper);
    }
}
