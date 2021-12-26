using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    private Dictionary<string, int> _goals;
    private bool _removable;

    public SubGoal(string key, int value, bool removable)
    {
        _goals = new Dictionary<string, int> {{key, value}};
        _removable = removable;
    }

    public Dictionary<string, int> Goals { get { return _goals; } }

    public bool Removable { get { return _removable; } }
}

public class GAgent : MonoBehaviour
{
    private List<GAction> _actions = new List<GAction>();
    private Queue<GAction> _actionQueue;
    private GInventory _inventory = new GInventory();
    
    protected StateHandler _beliefs = new StateHandler();
    protected Dictionary<SubGoal, int> _goals = new Dictionary<SubGoal, int>();

    private GPlanner _planner;
    private GAction _currentAction;
    private SubGoal _currentGoal;

    private bool _invokedAction;

    public IReadOnlyList<GAction> Actions { get { return _actions; } }

    public GInventory Inventory { get { return _inventory; } }

    public StateHandler Beliefs { get { return _beliefs; } }

    public Dictionary<SubGoal, int> Goals { get { return _goals; } }

    public GAction CurrentAction { get { return _currentAction; } }

    private void Awake()
    {
        GAction[] actions = GetComponents<GAction>();
        foreach (GAction action in actions)
        {
            action.Inventory = _inventory;
            action.Beliefs = _beliefs;
            _actions.Add(action);
        }
    }

    private void LateUpdate()
    {
        if (_currentAction != null && _currentAction.IsRunning)
        {
            _currentAction.TryStopRunning();
        }
        else
        {
            if (_planner == null || _actionQueue == null)
            {
                _planner = new GPlanner();

                IOrderedEnumerable<KeyValuePair<SubGoal, int>> sortedGoals =
                    from subGoal in _goals orderby subGoal.Value descending select subGoal;

                foreach (KeyValuePair<SubGoal, int> subGoal in sortedGoals)
                {
                    _actionQueue = _planner.Plan(_actions, subGoal.Key.Goals, _beliefs);
                    if (_actionQueue != null)
                    {
                        _currentGoal = subGoal.Key;
                        break;
                    }
                }
            }

            if (_actionQueue != null)
            {
                if (_actionQueue.Count > 0)
                {
                    _currentAction = _actionQueue.Dequeue();
                    if (_currentAction.PrePerform())
                        _currentAction.TryStartRunning();
                    else
                        _actionQueue = null;
                }
                else
                {
                    if (_currentGoal.Removable)
                        _goals.Remove(_currentGoal);

                    _planner = null;
                }
            }
        }
    }
}
