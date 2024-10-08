using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerControls controls;
    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        if (controls != null)
            controls.Enable();
    }
    private void OnDisable()
    {
        if (controls != null)
            controls.Disable();
    }
}

