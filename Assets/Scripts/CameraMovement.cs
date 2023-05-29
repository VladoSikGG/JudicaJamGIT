using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float _screenWidth, _screenHeight;
    [SerializeField] private float _offset;
    [SerializeField] private float _speed;
    void Start()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = transform.position;

        if (Input.mousePosition.x < _offset)
        {
            cameraPosition.x -= _speed * Time.deltaTime;
        }
        else if (Input.mousePosition.x > _screenWidth - _offset)
        {
            cameraPosition.x += _speed * Time.deltaTime;
        }
        else if (Input.mousePosition.y < _offset)
        {
            cameraPosition.z -= _speed * Time.deltaTime;
        }
        else if (Input.mousePosition.y > _screenHeight - _offset)
        {
            cameraPosition.z += _speed * Time.deltaTime;
        }

        transform.position = new Vector3(Mathf.Clamp(cameraPosition.x, -500, 500), 50, Mathf.Clamp(cameraPosition.z, -500, 500));
    }
}
