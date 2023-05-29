using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private int _countOfEnemy;
    private GameObject[] _ships;
    [SerializeField] private GameObject[] _typeShips;

    private void Update()
    {
        _ships = GameObject.FindGameObjectsWithTag("Enemy");
        if (_ships.Length < _countOfEnemy)
        {
            CreateShip();
        }
    }

    private void CreateShip()
    {
        Instantiate(_typeShips[Random.Range(0, _ships.Length)], new Vector3(Random.Range(-1000, 1000), 0, Random.Range(-1000, 1000)), Quaternion.identity);
    }
}
