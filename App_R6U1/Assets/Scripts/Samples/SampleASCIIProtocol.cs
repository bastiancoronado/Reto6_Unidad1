using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SampleASCIIProtocol : MonoBehaviour
{
    public SerialControllerASCIIProtocol serialController;

    [Tooltip("Time in seconds.")]
    public float ColdDown = 1;

    string state = "Recive_message";

    float tm = 0;

    float tm_past = 0;

    string msj = "*";

    void Start()
    {
        serialController = GameObject.Find("Controller").GetComponent<SerialControllerASCIIProtocol>();
    }


    void Update()
    {
        tm += Time.deltaTime;
        //---------------------------------------------------------------------
        // 3) Send data
        //---------------------------------------------------------------------

        switch (state)
        {
            case "Wait":
                if (tm >= tm_past + ColdDown)
                {
                    Debug.Log("time: " + tm + "| old_time: " + tm_past);
                    state = "Send_message";
                }
                break;
            case "Send_message":
                serialController.SendSerialMessage(msj);
                state = "Recive_message";
                break;
            case "Recive_message":
                break;
        }

        //---------------------------------------------------------------------
        // 1) Receive data
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


        //---------------------------------------------------------------------
        // 2) Start send data
        //---------------------------------------------------------------------

        if (message == "Fire" || message == "Reload" || message == "Fire&Reload")
        {
            tm_past = tm;
            state = "Wait";
            msj = Message(message);
        }
    }

    static string Message(string s)
    {
        string c = "_";
        switch (s)
        {
            case "Fire":
                c = "f";
                break;
            case "Reload":
                c = "r";
                break;
            case "Fire&Reload":
                c = "&";
                break;
        }
        return c;
    }
}

