using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class State
{
    [SerializeField] private string _key;
    [SerializeField] private int _value;

    public string Key
    {
        get { return _key; }
    }

    public int Value
    {
        get { return _value; }
    }
}

public class StateHandler
{
    private Dictionary<string, int> _statesDict;

    public StateHandler()
    {
        _statesDict = new Dictionary<string, int>();
    }

    public Dictionary<string, int> States
    {
        get { return _statesDict; }
    }

    public bool HasState(string key)
    {
        return _statesDict.ContainsKey(key);
    }

    public void AddState(string key, int value)
    {
        if (!HasState(key))
            _statesDict.Add(key, value);
        else
            Debug.LogWarning($"State 'Key:{key}' already exists.");
    }

    public void RemoveState(string key)
    {
        if (HasState(key))
            _statesDict.Remove(key);
        else
            Debug.LogWarning($"State 'Key:{key}' does not exist.");
    }

    public void ModifyState(string key, int value)
    {
        if (HasState(key))
            _statesDict[key] += value;
        else
            Debug.LogWarning($"State 'Key:{key}' does not exist.");
    }

    public void AddOrModifyState(string key, int value)
    {
        if (GWorld.Instance.StateHandler.HasState(key))
            GWorld.Instance.StateHandler.ModifyState(key, value);
        else
            GWorld.Instance.StateHandler.AddState(key, value);
    }

    public void SetState(string key, int value)
    {
        if (HasState(key))
            _statesDict[key] = value;
        else
            Debug.LogWarning($"State 'Key:{key}' does not exist.");
    }
}
