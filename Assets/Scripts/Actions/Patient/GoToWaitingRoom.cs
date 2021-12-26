using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWaitingRoom : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.StateHandler.AddOrModifyState("PatientWaiting", 1);
        GWorld.Instance.AddGameObjectToQueue("patient", gameObject);

        _agentBeliefs.AddState("AtWaitingRoom", 1);

        return true;
    }
}
