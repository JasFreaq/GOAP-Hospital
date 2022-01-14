using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clean : GAction
{
    public override bool PrePerform()
    {
        _target = GWorld.Instance.GetAvailableGameObjectFromQueue("puddle")?.transform;

        return _target != null;
    }

    public override bool PostPerform()
    {
        Destroy(_target.gameObject);
        GWorld.Instance.StateHandler.ModifyState("PuddlesLeft", -1);

        return true;
    }
}
