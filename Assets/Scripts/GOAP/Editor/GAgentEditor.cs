using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GAgentVisual))]
[CanEditMultipleObjects]
public class GAgentVisualEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        GAgentVisual agentVis = (GAgentVisual) target;

        GUILayout.Label("Name: " + agentVis.name);
        GUILayout.Label("Current Action: " + agentVis.Agent.CurrentAction);

        GUILayout.Label("Actions: ");
        foreach (GAction action in agentVis.Agent.Actions)
        {
            string precondition = "";
            string effect = "";

            foreach (KeyValuePair<string, int> p in action.Preconditions)
                precondition += p.Key + ", ";
            foreach (KeyValuePair<string, int> e in action.Effects)
                effect += e.Key + ", ";

            GUILayout.Label("====  " + action.ActionName + "(" + precondition + ")(" + effect + ")");
        }

        GUILayout.Label("Goals: ");
        foreach (KeyValuePair<SubGoal, int> goal in agentVis.Agent.Goals)
        {
            GUILayout.Label("---: ");
            foreach (KeyValuePair<string, int> subgoal in goal.Key.Goals)
                GUILayout.Label("=====  " + subgoal.Key);
        }

        GUILayout.Label("Beliefs: ");
        foreach (KeyValuePair<string, int> sg in agentVis.Agent.Beliefs.States)
        {
            GUILayout.Label("=====  " + sg.Key);
        }

        GUILayout.Label("Inventory: ");
        foreach (GameObject g in agentVis.Agent.Inventory.Items)
        {
            GUILayout.Label("====  " + g.tag);
        }

        serializedObject.ApplyModifiedProperties();
    }
}