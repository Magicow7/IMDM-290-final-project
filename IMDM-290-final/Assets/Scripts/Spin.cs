using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float xRotSpeed;
    public float yRotSpeed;
    public float zRotSpeed;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.eulerAngles += new Vector3(xRotSpeed, yRotSpeed, zRotSpeed);
    }
}