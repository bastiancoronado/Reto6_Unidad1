using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{

    float rotSpeed = 0;
    float rotate;
    SampleASCIIProtocol obj_ascii;
    SampleBinaryProtocol obj_binary;

    Rigidbody rigBox;

    void Start()
    {
        rigBox = GetComponent<Rigidbody>();
        obj_ascii = GameObject.Find("Obj").GetComponent<SampleASCIIProtocol>();
        obj_binary = GameObject.Find("Obj").GetComponent<SampleBinaryProtocol>();
    }

    void Update()
    {
        int pos = obj_binary.valrote;
        
        switch (pos)
        {
            case 0:
                rotSpeed = -100;
                break;
            case 1:
                rotSpeed = -20;
                break;
            case 2:
                rotSpeed = 0;
                break;
            case 3:
                rotSpeed = 20;
                break;
            case 4:
                rotSpeed = 100;
                break;
        }
        rotate = Time.deltaTime * rotSpeed;
        Quaternion rote = Quaternion.Euler(0, rotate, 0);
        rigBox.MoveRotation(rigBox.rotation * rote);
        Debug.Log(rotSpeed);
    }
}
