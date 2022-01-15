using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    [SerializeField] private ResourceData[] _resources;
    [SerializeField] private string[] _emptyStates;

    void Start()
    {
        foreach (ResourceData resourceData in _resources)
        {
            GameObject[] resourceObjects = GameObject.FindGameObjectsWithTag(resourceData.ObjectTag);
            if (resourceObjects.Length > 0)
            {
                foreach (GameObject resourceObject in resourceObjects)
                {
                    Transform navCenter = resourceObject.transform.Find("Nav Center");
                    GWorld.Instance.AddGameObjectToQueue(resourceData.QueueName,
                        navCenter ? navCenter.gameObject : resourceObject);
                }

                GWorld.Instance.StateHandler.AddState(resourceData.WorldState, resourceObjects.Length);
            }
        }

        foreach (string state in _emptyStates)
        {
            GWorld.Instance.StateHandler.AddState(state, 0);
        }
    }
}
