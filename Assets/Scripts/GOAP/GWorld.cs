using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld _instance = new GWorld();
    
    private StateHandler _stateHandler;
    private Dictionary<string, Queue<GameObject>> _gameObjectQueues;

    private GWorld()
    {
        _stateHandler = new StateHandler();
        _gameObjectQueues = new Dictionary<string, Queue<GameObject>>();
    }

    public static GWorld Instance
    {
        get { return _instance; }
    }

    public StateHandler StateHandler
    {
        get { return _stateHandler; }
    }

    public void AddGameObjectToQueue(string key, GameObject gameObject)
    {
        if (!_gameObjectQueues.ContainsKey(key))
            _gameObjectQueues.Add(key, new Queue<GameObject>());

        _gameObjectQueues[key].Enqueue(gameObject);
    }

    public GameObject RemoveGameObjectFromQueue(string key)
    {
        if (!_gameObjectQueues.ContainsKey(key))
            return null;

        if (_gameObjectQueues[key].Count == 0)
            return null;

        return _gameObjectQueues[key].Dequeue();
    }
}