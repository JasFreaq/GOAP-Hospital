using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    [SerializeField] private string _actionName = "Action";
    [SerializeField] private float _cost = 1.0f;
    [SerializeField] private float _duration;
    [SerializeField] private string _targetTag;
    [SerializeField] private WorldState[] _preconditions;
    [SerializeField] private WorldState[] _effects;

    private NavMeshAgent _navAgent;

    protected Transform _target;
    private Dictionary<string, int> _preconditionsDict;
    private Dictionary<string, int> _effectsDict;
    private WorldStateHandler _agentBeliefs;

    private bool _isRunning;
    private bool _invokedComplete;

    public GAction()
    {
        _preconditionsDict = new Dictionary<string, int>();
        _effectsDict = new Dictionary<string, int>();
    }

    public string ActionName { get { return _actionName; } }

    public float Cost { get { return _cost; } }
    
    public Dictionary<string, int> Preconditions { get { return _preconditionsDict; } }

    public Dictionary<string, int> Effects { get { return _effectsDict; } }
    
    public bool IsRunning { get { return _isRunning; } }

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();

        if (_preconditions != null)
        {
            foreach (WorldState precondition in _preconditions)
                _preconditionsDict.Add(precondition.Key, precondition.Value);
        }
        
        if (_effects != null)
        {
            foreach (WorldState effect in _effects)
                _effectsDict.Add(effect.Key, effect.Value);
        }
    }

    public bool IsAchievable()
    {
        return true;
    }
    
    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> precondition in _preconditionsDict)
        {
            if (!conditions.ContainsKey(precondition.Key))
                return false;
        }

        return true;
    }
    
    public abstract bool PrePerform();

    public abstract bool PostPerform();

    public void TryStartRunning()
    {
        if (_target == null)
        {
            if (_targetTag != "")
                _target = GameObject.FindGameObjectWithTag(_targetTag).transform;
        }

        if (_target != null) 
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _navAgent.SetDestination(_target.position);
            }
        }
    }

    public void TryStopRunning()
    {
        if (_navAgent.hasPath && _navAgent.remainingDistance < 1f)
        {
            if (!_invokedComplete)
            {
                StartCoroutine(CompleteActionRoutine());
                _invokedComplete = true;
            }
        }
    }

    private IEnumerator CompleteActionRoutine()
    {
        yield return new WaitForSeconds(_duration);

        _isRunning = false;
        PostPerform();

        _invokedComplete = false;
    }
}
