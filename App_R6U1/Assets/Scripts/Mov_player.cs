using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_player : MonoBehaviour
{
    SampleASCIIProtocol obj_ascii;
    public CharacterController controller;

    public float speed = 15f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;
    string post_dir = "*";

    Vector3 move;
    Vector3 velocity;

    void Start()
    {
        obj_ascii = GameObject.Find("Obj").GetComponent<SampleASCIIProtocol>();
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        string dir = obj_ascii.mesj;//f,b,&
        int magnitud = 0;

        switch (dir)
        {
            case "f":
                magnitud = 1;
                break;
            case "b":
                magnitud = -1;
                break;
            case "&":
                if (isGrounded) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
                break;
        }
        if (!isGrounded) magnitud = 1;
        move = transform.forward * magnitud;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
