using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    private Text _statesText;

    private void Awake()
    {
        _statesText = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        _statesText.text = "";
        Dictionary<string, int> worldStates = GWorld.Instance.StateHandler.States;
        foreach (KeyValuePair<string, int> state in worldStates)
        {
            _statesText.text += $"{state.Key}, {state.Value}\n";
        }
    }
}
