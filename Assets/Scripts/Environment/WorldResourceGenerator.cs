using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldResourceGenerator : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _navSurface;
    [SerializeField] private Transform _resourceParent;
    [SerializeField] private ResourceData[] _resources;
    [SerializeField] private string _alternateNavCenterName = "Nav Center";
    [SerializeField] private Transform _resourceButtonsHolder;
    [SerializeField] private ResourceButton _resourceButton;

    private Camera _mainCamera;
    private ResourceButton[] _resourceButtons;

    private GameObject _focusedObj;
    private Vector3 _goalPos;
    private Vector3 _clickOffset;
    private int _selectedIndex;
    private bool _offsetCalculated;
    private bool _resourceInstantiated;

    private void Start()
    {
        _mainCamera = Camera.main;

        _resourceButtons = new ResourceButton[_resources.Length];
        for (int i = 0; i < _resources.Length; i++)
        {
            ResourceButton resourceButton = Instantiate(_resourceButton, _resourceButtonsHolder);

            int index = i;
            resourceButton.Button.onClick.AddListener(() =>
            {
                _resourceButtons[_selectedIndex].Image.color = Color.white;
                _selectedIndex = index;
                resourceButton.Image.color = Color.green;
            });
            resourceButton.Text.text = _resources[i].ObjectTag;

            _resourceButtons[i] = resourceButton;
        }

        _resourceButtons[0].Image.color = Color.green;
    }

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_focusedObj)
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out RaycastHit hit))
                {
                    print(hit.transform);
                    if (FindResourceTag(hit.transform))
                    {
                        _focusedObj = hit.transform.gameObject;

                        _clickOffset = Vector3.zero;
                        _offsetCalculated = false;
                    }
                    else
                    {
                        _goalPos = hit.point;
                        _focusedObj = Instantiate(_resources[_selectedIndex].Prefab, _goalPos, Quaternion.identity);
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
                        GWorld.Instance.AddGameObjectToQueue(_resources[_selectedIndex].QueueName, _focusedObj);
                    }
                    else
                    {
                        Transform navCenter = _focusedObj.transform.Find("Nav Center");
                        GWorld.Instance.AddGameObjectToQueue(_resources[_selectedIndex].QueueName,
                            navCenter ? navCenter.gameObject : _focusedObj);
                    }

                    GWorld.Instance.StateHandler.ModifyState(_resources[_selectedIndex].WorldState, 1);

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
                    GWorld.Instance.RemoveGameObjectFromQueue(_resources[_selectedIndex].QueueName, _focusedObj);
                }
                else
                {
                    Transform navCenter = _focusedObj.transform.Find("Nav Center");
                    GWorld.Instance.RemoveGameObjectFromQueue(_resources[_selectedIndex].QueueName,
                        navCenter ? navCenter.gameObject : _focusedObj);
                }

                GWorld.Instance.StateHandler.ModifyState(_resources[_selectedIndex].WorldState, -1);
                Destroy(_focusedObj);
            }
        }
    }

    private bool FindResourceTag(Transform comparer)
    {
        int i = 0;
        foreach (ResourceData resource in _resources)
        {
            if (comparer.CompareTag(resource.ObjectTag))
            {
                _selectedIndex = i;
                return true;
            }

            i++;
        }

        return false;
    }
}
