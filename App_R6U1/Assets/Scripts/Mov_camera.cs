using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_camera : MonoBehaviour
{
    float Sensiticy = 0;
    SampleBinaryProtocol obj_binary;
    public Transform PlayerBody;

    void Start()
    {
        obj_binary = GameObject.Find("Obj").GetComponent<SampleBinaryProtocol>();
    }

   
    void Update()
    {
        int rot = obj_binary.valrote;
        switch (rot)
        {
            case 0:
                Sensiticy = -100f;
                break;
            case 1:
                Sensiticy = -20f;
                break;
            case 2:
                Sensiticy = 0f;
                break;
            case 3:
                Sensiticy = 20f;
                break;
            case 4:
                Sensiticy = 100f;
                break;
        }

        float move = Sensiticy * Time.deltaTime;
        PlayerBody.Rotate(Vector3.up * move);
    }
}
