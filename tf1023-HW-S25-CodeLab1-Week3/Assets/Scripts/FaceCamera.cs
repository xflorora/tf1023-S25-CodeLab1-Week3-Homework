using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        Quaternion newRotation=Camera.main.transform.rotation;
        transform.rotation=newRotation;
    }
}
