using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    void Start()
    {
        GameObject[] cubicles = GameObject.FindGameObjectsWithTag("Cubicle");
        if (cubicles.Length > 0) 
        {
            foreach (GameObject cubicle in cubicles)
                GWorld.Instance.AddGameObjectToQueue("cubicle", cubicle);

            GWorld.Instance.StateHandler.AddState("FreeCubicle", cubicles.Length);
        }

        GameObject[] offices = GameObject.FindGameObjectsWithTag("Office");
        if (offices.Length > 0)
        {
            foreach (GameObject office in offices)
                GWorld.Instance.AddGameObjectToQueue("office", office);

            GWorld.Instance.StateHandler.AddState("FreeOffice", offices.Length);
        }

        GameObject[] toilets = GameObject.FindGameObjectsWithTag("Toilet");
        if (toilets.Length > 0) 
        {
            foreach (GameObject toilet in toilets)
                GWorld.Instance.AddGameObjectToQueue("toilet", toilet);

            GWorld.Instance.StateHandler.AddState("FreeToilet", toilets.Length);
        }

        GWorld.Instance.StateHandler.AddState("PuddlesLeft", 0);
    }
}
