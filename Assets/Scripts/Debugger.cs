using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private float timeValue = 3f;
    [SerializeField] private bool _updateTime;

    private void Awake()
    {
        if (_updateTime) 
            Time.timeScale = timeValue;
    }
}
