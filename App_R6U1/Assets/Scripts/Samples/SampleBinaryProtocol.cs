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

    // Initialization
    void Start()
    {
        serialController = GameObject.Find("Controller").GetComponent<SerialControllerBinaryProtocol>();
        
    }

    // Update is called once per frame
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
        Debug.Log(sb);
    }
}
