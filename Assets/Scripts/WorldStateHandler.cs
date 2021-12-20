using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldState
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

public class WorldStateHandler
{
    private Dictionary<string, int> _worldStatesDict;

    public WorldStateHandler()
    {
        _worldStatesDict = new Dictionary<string, int>();
    }

    public IReadOnlyDictionary<string, int> WorldStates
    {
        get { return _worldStatesDict; }
    }

    public bool HasState(string key)
    {
        return _worldStatesDict.ContainsKey(key);
    }

    public void AddState(string key, int value)
    {
        if (!HasState(key))
            _worldStatesDict.Add(key, value);
        else
            Debug.LogWarning($"State 'Key:{key}' already exists.");
    }

    public void RemoveState(string key)
    {
        if (HasState(key))
            _worldStatesDict.Remove(key);
        else
            Debug.LogWarning($"State 'Key:{key}' does not exist.");
    }

    public void ModifyState(string key, int value)
    {
        if (HasState(key))
        {
            _worldStatesDict[key] += value;
            if (_worldStatesDict[key] <= 0) 
                RemoveState(key);
        }
        else
            Debug.LogWarning($"State 'Key:{key}' does not exist.");
    }
    
    public void SetState(string key, int value)
    {
        if (HasState(key))
            _worldStatesDict[key] = value;
        else
            Debug.LogWarning($"State 'Key:{key}' does not exist.");
    }
}
