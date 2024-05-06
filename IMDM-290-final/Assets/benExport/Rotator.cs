using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateY = 3; 

    void Update()
    {
        transform.Rotate(0, rotateY*Time.deltaTime, 0, Space.Self); 
    }
}
