using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitToilet : GAction
{
    public override bool PrePerform()
    {
        _target = GWorld.Instance.GetAvailableGameObjectFromQueue("toilet")?.transform;
        if (_target == null)
            return false;

        _agentInventory.AddItem(_target.gameObject);
        GWorld.Instance.StateHandler.ModifyState("FreeToilet", -1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.AddGameObjectToQueue("toilet", _target.gameObject);
        GWorld.Instance.StateHandler.ModifyState("FreeToilet", 1);

        _agentInventory.RemoveItem(_target.gameObject);
        _agentBeliefs.RemoveState("NeedRelief");

        return true;
    }
}
