using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SampleASCIIProtocol : MonoBehaviour
{
    public SerialControllerASCIIProtocol serialController;

    void Start()
    {
        serialController = GameObject.Find("SerialControllerASCII").GetComponent<SerialControllerASCIIProtocol>();
    }

    
    void Update()
    {
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Sending A");
            serialController.SendSerialMessage("A");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Sending Z");
            serialController.SendSerialMessage("Z");
        }


        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, "__Connected__"))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, "__Disconnected__"))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
            Debug.Log("Message arrived: " + message);
    }
}

