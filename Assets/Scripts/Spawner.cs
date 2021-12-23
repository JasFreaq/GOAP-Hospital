using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private int _spawnNum = 1;
    [SerializeField] private Vector2 _spawnIntervalRange = new Vector2(1f, 2f);

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        int _spawnedNum = 0;
        while ((_spawnNum > 0 && _spawnedNum < _spawnNum) || 
               (_spawnNum == 0 && (_spawnIntervalRange.x >= 1f && _spawnIntervalRange.y >= 1f)))
        {
            Instantiate(_spawnPrefab, transform.position, Quaternion.identity);
            _spawnedNum++;

            if (_spawnIntervalRange.x >= 1f && _spawnIntervalRange.y >= 1f)
                yield return new WaitForSeconds(Random.Range(_spawnIntervalRange.x, _spawnIntervalRange.y));
            else
                yield return new WaitForEndOfFrame();
        }
    }
}
