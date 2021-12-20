using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    private Dictionary<string, int> _goals;
    private bool _remove;

    public SubGoal(string key, int value, bool remove)
    {
        _goals = new Dictionary<string, int> {{key, value}};
        _remove = remove;
    }
}

public class GAgent : MonoBehaviour
{
    private List<GAction> _actions = new List<GAction>();
    private Dictionary<SubGoal, int> _goals = new Dictionary<SubGoal, int>();
    private Queue<GAction> _actionQueue;

    private GPlanner _planner;
    private GAction _currentAction;
    private SubGoal _currentGoal;

    private void Awake()
    {
        GAction[] actions = GetComponents<GAction>();
        foreach (GAction action in actions)
            _actions.Add(action);
        
    }

    private void LateUpdate()
    {
        
    }
}
