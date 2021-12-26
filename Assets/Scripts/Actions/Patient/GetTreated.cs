using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreated : GAction
{
    public override bool PrePerform()
    {
        _target = _agentInventory.FindItemWithTag("Cubicle")?.transform;

        return _target != null;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.StateHandler.AddOrModifyState("Treated", 1);
        _agentInventory.RemoveItem(_target.gameObject);
        _agentBeliefs.AddState("IsCured", 1);

        return true;
    }
}
