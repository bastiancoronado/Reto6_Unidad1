using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text;

public class SerialThreadBinaryProtocol : AbstractSerialThread
{
    // Buffer where a single message must fit
    private byte[] buffer = new byte[1024];
    private int bufferUsed = 0;

    public SerialThreadBinaryProtocol(string portName,
                                       int baudRate,
                                       int delayBeforeReconnecting,
                                       int maxUnreadMessages)
        : base(portName, baudRate, delayBeforeReconnecting, maxUnreadMessages, false)
    {

    }
    
    protected override void SendToWire(object message, SerialPort serialPort)
    {
        byte[] binaryMessage = (byte[])message;
        serialPort.Write(binaryMessage, 0, binaryMessage.Length);
    }

    protected override object ReadFromWire(SerialPort serialPort)
    {
        if (serialPort.BytesToRead > 0)
        {
            serialPort.Read(buffer, 0, 1);
            bufferUsed = 1;
            // wait for the rest of data
            while (bufferUsed < (buffer[0] + 1))
            {
                bufferUsed = bufferUsed + serialPort.Read(buffer, bufferUsed, buffer[0]);
            }

            // Verify Checksum and
            if (buffer[0] != 0x65)
            {
                if (verifyChecksum(buffer, buffer[0]) == true)
                {
                    // send the package to the application
                    byte[] returnBuffer = new byte[bufferUsed];
                    System.Array.Copy(buffer, returnBuffer, bufferUsed);
                    bufferUsed = 0;
                    return returnBuffer;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Packet: ");
                    foreach (byte data in buffer)
                    {
                        sb.Append(data.ToString("X2") + " ");
                    }
                    sb.Append("Checksum fails");
                    Debug.Log(sb);
                    return null;
                }
            }
            else 
            {
                serialPort.Read(buffer, 0, bufferUsed);
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    private bool verifyChecksum(byte[] packet, int len)
    {
        ushort s = packet[1];
        for (ushort i = 2; packet[0] > i; i++)
        {
            s += packet[i];
        }
        if (s == packet[len]) return true;
        else return false;
    }


}
