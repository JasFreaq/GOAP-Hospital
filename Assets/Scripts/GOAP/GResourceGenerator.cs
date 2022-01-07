using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GResourceGenerator : MonoBehaviour
{
    [SerializeField] private string _state;
    [SerializeField] private float _stateStrength;
    [SerializeField] private float _stateDecayRate;
    [SerializeField] private string _queueName;
    [SerializeField] private string _worldState;
    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private GAction _action;

    private bool _stateFound;
    private float _initialStrength;
    
    private StateHandler _beliefs;

    void Awake()
    {
        _beliefs = GetComponent<GAgent>().Beliefs;
    }

    private void Start()
    {
        _initialStrength = _stateStrength;
    }

    void LateUpdate()
    {
        if (_action.IsRunning)
        {
            _stateFound = false;
            _stateStrength = _initialStrength;
        }
        else if (!_stateFound && _beliefs.HasState(_state))
            _stateFound = true;

        if (_stateFound)
        {
            _stateStrength -= _stateDecayRate * Time.deltaTime;
            if (_stateStrength <= 0)
            {
                _stateFound = false;
                _stateStrength = _initialStrength;

                Vector3 resourcePosition = new Vector3(transform.position.x, _resourcePrefab.transform.position.y,
                    transform.position.z);
                GameObject resourceInstance = Instantiate(_resourcePrefab, resourcePosition, Quaternion.identity);

                _beliefs.RemoveState(_state);
                GWorld.Instance.AddGameObjectToQueue(_queueName, resourceInstance);
                GWorld.Instance.StateHandler.ModifyState(_worldState, 1);
            }
        }
    }
}
