using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paperwork : GAction
{
    public override bool PrePerform()
    {
        _target = GWorld.Instance.GetAvailableGameObjectFromQueue("office")?.transform;
        if (_target == null)
            return false;

        _agentInventory.AddItem(_target.gameObject);
        GWorld.Instance.StateHandler.ModifyState("FreeOffice", -1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.AddGameObjectToQueue("office", _target.gameObject);
        _agentInventory.RemoveItem(_target.gameObject);
        GWorld.Instance.StateHandler.ModifyState("FreeOffice", 1);
        return true;
    }
}
