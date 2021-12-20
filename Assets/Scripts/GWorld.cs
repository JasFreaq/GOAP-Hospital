using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld _instance = new GWorld();
    private static WorldStateHandler _worldStateHandler;

    static GWorld()
    {
        _worldStateHandler = new WorldStateHandler();
    }

    private GWorld() { }

    public static GWorld Instance
    {
        get { return _instance; }
    }

    public WorldStateHandler WorldStateHandler
    {
        get { return _worldStateHandler; }
    }
}