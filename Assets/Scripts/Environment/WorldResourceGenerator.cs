using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldResourceGenerator : MonoBehaviour
{
    #region Associated Data Structures

    [System.Serializable]
    class Resource
    {
        public string worldState;
        public string queueName;
        public string objectTag;
        public GameObject prefab;
    }

    #endregion

    [SerializeField] private NavMeshSurface _navSurface;
    [SerializeField] private Transform _resourceParent;
    [SerializeField] private Resource _resource;
    [SerializeField] private string _alternateNavCenterName = "Nav Center";

    private Camera _mainCamera;

    private GameObject _focusedObj;
    private Vector3 _goalPos;
    private Vector3 _clickOffset;
    private bool _offsetCalculated;
    private bool _resourceInstantiated;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_focusedObj)
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.CompareTag(_resource.objectTag))
                    {
                        _focusedObj = hit.transform.gameObject;

                        _clickOffset = Vector3.zero;
                        _offsetCalculated = false;
                    }
                    else
                    {
                        _goalPos = hit.point;
                        _focusedObj = Instantiate(_resource.prefab, _goalPos, Quaternion.identity);
                        _focusedObj.transform.parent = _resourceParent;
                        _resourceInstantiated = true;
                    }
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (_focusedObj)
            {
                if (_resourceInstantiated)
                {
                    if (string.IsNullOrWhiteSpace(_alternateNavCenterName))
                    {
                        GWorld.Instance.AddGameObjectToQueue(_resource.queueName, _focusedObj);
                    }
                    else
                    {
                        Transform navCenter = _focusedObj.transform.Find("Nav Center");
                        GWorld.Instance.AddGameObjectToQueue(_resource.queueName,
                            navCenter ? navCenter.gameObject : _focusedObj);
                    }

                    GWorld.Instance.StateHandler.ModifyState(_resource.worldState, 1);

                    _resourceInstantiated = false;
                }

                _navSurface.BuildNavMesh();
                _focusedObj = null;
            }
        }

        if (_focusedObj)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                if (!hit.transform.gameObject.Equals(_focusedObj))
                {
                    if (!_offsetCalculated)
                    {
                        _clickOffset = hit.point - _focusedObj.transform.position;
                        _offsetCalculated = true;
                    }

                    _goalPos = hit.point - _clickOffset;
                    _focusedObj.transform.position = _goalPos;

                    break;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                _focusedObj.transform.Rotate(0, 90, 0);
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
                _focusedObj.transform.Rotate(0, -90, 0);

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                if (string.IsNullOrWhiteSpace(_alternateNavCenterName))
                {
                    GWorld.Instance.RemoveGameObjectFromQueue(_resource.queueName, _focusedObj);
                }
                else
                {
                    Transform navCenter = _focusedObj.transform.Find("Nav Center");
                    GWorld.Instance.RemoveGameObjectFromQueue(_resource.queueName,
                        navCenter ? navCenter.gameObject : _focusedObj);
                }

                GWorld.Instance.StateHandler.ModifyState(_resource.worldState, -1);
                Destroy(_focusedObj);
            }
        }
    }
}
