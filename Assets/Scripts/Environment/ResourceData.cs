using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Data", menuName = "Resource Data")]
public class ResourceData : ScriptableObject
{
    [SerializeField] private string _worldState;
    [SerializeField] private string _queueName;
    [SerializeField] private string _objectTag;
    [SerializeField] private GameObject _prefab;

    public string WorldState { get { return _worldState; } }

    public string QueueName { get { return _queueName; } }
    
    public string ObjectTag { get { return _objectTag; } }
    
    public GameObject Prefab { get { return _prefab; } }
}
