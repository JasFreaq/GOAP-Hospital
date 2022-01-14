using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    #region Associated Data Structures

    [System.Serializable]
    class ResourceData
    {
        public string objectTag;
        public string worldState;
        public string queueName;
    }

    #endregion

    [SerializeField] private ResourceData[] _resources;
    [SerializeField] private string[] _emptyStates;

    void Start()
    {
        foreach (ResourceData resourceData in _resources)
        {
            GameObject[] resourceObjects = GameObject.FindGameObjectsWithTag(resourceData.objectTag);
            if (resourceObjects.Length > 0)
            {
                foreach (GameObject resourceObject in resourceObjects)
                {
                    Transform navCenter = resourceObject.transform.Find("Nav Center");
                    GWorld.Instance.AddGameObjectToQueue(resourceData.queueName,
                        navCenter ? navCenter.gameObject : resourceObject);
                }

                GWorld.Instance.StateHandler.AddState(resourceData.worldState, resourceObjects.Length);
            }
        }

        foreach (string state in _emptyStates)
        {
            GWorld.Instance.StateHandler.AddState(state, 0);
        }
    }
}
