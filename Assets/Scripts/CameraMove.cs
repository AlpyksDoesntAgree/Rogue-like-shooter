using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float _sensitivity = 2f;
    private float _minY = -90f;
    private float _maxY = 90f;

    private float _rotationX = 0f;
    private float _rotationY = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * _sensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

        _rotationX += mouseX;
        _rotationY += mouseY * -1;
        _rotationY = Mathf.Clamp(_rotationY, _minY, _maxY);

        transform.localRotation = Quaternion.Euler(_rotationY, _rotationX, 0f);
    }
}
