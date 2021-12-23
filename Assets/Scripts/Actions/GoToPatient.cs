using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPatient : GAction
{
    public override bool PrePerform()
    {
        _target = GWorld.Instance.RemoveGameObjectFromQueue("patient")?.transform;

        return _target != null;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
