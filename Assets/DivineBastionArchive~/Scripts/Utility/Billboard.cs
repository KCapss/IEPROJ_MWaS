using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Billboard : MonoBehaviour
{
    private Camera cameraMain;

    private void Awake()
    {
        cameraMain = Camera.main;
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        transform.LookAt(cameraMain.transform);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
