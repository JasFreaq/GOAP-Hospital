using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    private Node _parent;
    private float _cost;
    private Dictionary<string, int> _states;
    private GAction _action;

    public Node(Node parent, float cost, Dictionary<string, int> states, GAction action)
    {
        _parent = parent;
        _cost = cost;
        _states = new Dictionary<string, int>(states);
        _action = action;
    }

    public Node Parent { get { return _parent; } }
    public float Cost { get { return _cost; } }
    public Dictionary<string, int> States { get { return _states; } }
    public GAction Action { get { return _action; } }
}

public class GPlanner
{
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goals, WorldStateHandler worldStates)
    {
        List<GAction> usableActions = new List<GAction>();
        foreach (GAction action in actions)
        {
            if (action.IsAchievable()) 
                usableActions.Add(action);
        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, GWorld.Instance.WorldStateHandler.WorldStates, null);

        if (BuildGraph(start, leaves, usableActions, goals))
        {
            Node cheapest = leaves[0];
            foreach (Node leaf in leaves)
            {
                if (leaf.Cost < cheapest.Cost)
                    cheapest = leaf;
            }

            List<GAction> result = new List<GAction>();
            Node n = cheapest;
            while (n != null)
            {
                if (n.Action != null)
                    result.Insert(0, n.Action);

                n = n.Parent;
            }

            return new Queue<GAction>(result);
        }

        Debug.Log("No Plan Generated");
        return null;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> actions, Dictionary<string, int> goals)
    {
        bool foundPath = false;
        foreach (GAction action in actions)
        {
            if (action.IsAchievableGiven(parent.States))
            {
                Dictionary<string, int> currentStates = new Dictionary<string, int>(parent.States);
                foreach (KeyValuePair<string, int> effect in action.Effects)
                {
                    if (!currentStates.ContainsKey(effect.Key))
                        currentStates.Add(effect.Key, effect.Value);
                }

                Node node = new Node(parent, parent.Cost + action.Cost, currentStates, action);

                if (Util.CompareDictValues(goals, currentStates))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> actionsSubset = Util.GetSubset(actions, action);
                    if (BuildGraph(node, leaves, actionsSubset, goals))
                        foundPath = true;
                }
            }
        }

        return foundPath;
    }
}
