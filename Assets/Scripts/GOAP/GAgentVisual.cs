using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GAgentVisual : MonoBehaviour
{
    private GAgent _agent;

    public GAgent Agent { get { return _agent; } }

    void Awake()
    {
        _agent = GetComponent<GAgent>();
    }
}
