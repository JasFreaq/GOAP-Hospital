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
        if (GWorld.Instance.WorldStateHandler.HasState("PatientWaiting"))
            GWorld.Instance.WorldStateHandler.ModifyState("PatientWaiting", 1);
        else
            GWorld.Instance.WorldStateHandler.AddState("PatientWaiting", 1);

        GWorld.Instance.AddGameObjectToQueue("patient", gameObject);

        return true;
    }
}
