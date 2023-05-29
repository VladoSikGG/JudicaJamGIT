using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject[] _typeShips;
    [SerializeField] private UIcontroller _interface;
    public void BuyScout()
    {
        if (_interface.crystals > 20)
        {
            Instantiate(_typeShips[0], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);
            _interface.crystals -= 20;
        }
        
    }

    public void BuyTank()
    {
        if (_interface.crystals > 20)
        {
            Instantiate(_typeShips[1], new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0, Camera.main.ScreenToWorldPoint(Input.mousePosition).z), Quaternion.identity);
            _interface.crystals -= 20;
        }
    }
}
