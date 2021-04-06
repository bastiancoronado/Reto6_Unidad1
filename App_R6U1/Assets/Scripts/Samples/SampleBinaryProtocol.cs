using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class SampleBinaryProtocol : MonoBehaviour
{
    public SerialControllerBinaryProtocol serialController;

    private static float last_tmo = 0;

    private static float t = 0;

    [Tooltip("Time in seconds.")]
    public float ColdDown = 0.1f;

    public int valrote;

    void Start()
    {
        serialController = GameObject.Find("Controller").GetComponent<SerialControllerBinaryProtocol>();       
    }


    void Update()
    {
        t += Time.deltaTime;
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------
        if (t > last_tmo + ColdDown)
        {
            last_tmo = t;
            serialController.SendSerialMessage(new byte[] { 0x07, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x1B });
        }

        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        byte[] message = serialController.ReadSerialMessage();

        if (message == null)
            return;
        StringBuilder sb = new StringBuilder();
        sb.Append("Packet: ");
        foreach (byte data in message)
        {
            sb.Append(data.ToString("X2") + " ");
        }
        valrote = value(sb);
        //Debug.Log(valrote);

    }

    int value(StringBuilder sb)
    {
        int v = 2;       
        switch (sb.ToString().Length)
        {
            //quieto
            case 20://"03 00 01 01 ":
                v = 2;
                break;
            //derecha normal
            case 26://"05 0F 05 06 07 21 ":
                v = 0;
                break;
            //izquierda normal
            case 23://"04 EE 09 02 F9 ":
                v = 4;
                break;
            //derecha lento
            case 29://"06 01 DD 0C 05 06 F5 ":
                v = 1;
                break;
            //izquierda lento
            case 17://"02 CD CD ":
                v = 3;
                break;
            //default:
              //  Debug.Log("Nada papi");
                //break;

        }
        return v;
    }
        
    
}
