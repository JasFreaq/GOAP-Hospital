using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : GAction
{
    private GameObject _resource;

    public override bool PrePerform()
    {
        _target = GWorld.Instance.RemoveGameObjectFromQueue("patient")?.transform;
        if (_target == null)
            return false;

        _resource = GWorld.Instance.RemoveGameObjectFromQueue("cubicle");
        if (_resource == null)
        {
            GWorld.Instance.AddGameObjectToQueue("patient", _target.gameObject);
            _target = null;
            return false;
        }

        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.StateHandler.ModifyState("Waiting", -1);
        if (_target) 
            _target.GetComponent<GAgent>().Inventory.AddItem(_resource);

        _agentInventory.AddItem(_resource);
        GWorld.Instance.StateHandler.AddOrModifyState("PatientWaiting", -1);
        GWorld.Instance.StateHandler.AddOrModifyState("FreeCubicle", -1);

        return true;
    }
}
