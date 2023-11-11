using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float keyboardInputSensitivity = 1f;
    [SerializeField] private float mouseInputSensitivity = 1f;
    [SerializeField] private bool continuous = true;
    [SerializeField] private Transform bottomLeftBorder;
    [SerializeField] private Transform topRightBorder;
    Vector3 input;
    Vector3 pointOfOrigin;
    
    private void Update()
    {
        NullInput();
        MoveCameraInput();
        MoveCamera();
    }
    private void NullInput()
    {
        input.x = 0;
        input.y = 0;
        input.z = 0;
    }
    private void MoveCamera()
    {
        Vector3 position = transform.position;
        position += (input * Time.deltaTime);
        //position.x = Mathf.Clamp(position.x, bottomLeftBorder.position.x, topRightBorder.position.x);
        //position.y = Mathf.Clamp(position.y, bottomLeftBorder.position.y, topRightBorder.position.y);
        transform.position = position;
        
    }
    private void MoveCameraInput()
    {
        AxisInput();
        MouseInput();
    }
    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(2))
        {
            pointOfOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            input += (Input.mousePosition - pointOfOrigin) * mouseInputSensitivity;
            if (!continuous)
            {
                pointOfOrigin = Input.mousePosition;
            }
        }
    }

    private void AxisInput()
    {
        input.x += Input.GetAxisRaw("Horizontal") * keyboardInputSensitivity;
        input.y += Input.GetAxisRaw("Vertical") * keyboardInputSensitivity;
    }
}
