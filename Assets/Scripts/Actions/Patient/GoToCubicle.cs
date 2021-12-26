using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCubicle : GAction
{
    public override bool PrePerform()
    {
        _target = _agentInventory.FindItemWithTag("Cubicle")?.transform;
        if (_target == null)
            return false;

        _agentBeliefs.RemoveState("AtWaitingRoom");

        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
