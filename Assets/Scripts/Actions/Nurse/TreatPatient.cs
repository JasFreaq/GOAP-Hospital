using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatPatient : GAction
{
    public override bool PrePerform()
    {
        _target = _agentInventory.FindItemWithTag("Cubicle")?.transform;
        if (_target == null)
            return false;

        GWorld.Instance.StateHandler.AddOrModifyState("TreatingPatient", 1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.StateHandler.AddOrModifyState("TreatingPatient", -1);
        GWorld.Instance.AddGameObjectToQueue("cubicle", _target.gameObject);
        GWorld.Instance.StateHandler.ModifyState("FreeCubicle", 1);

        _agentInventory.RemoveItem(_target.gameObject);

        return true;
    }
}
