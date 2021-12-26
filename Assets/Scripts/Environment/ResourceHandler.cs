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
    }
}
