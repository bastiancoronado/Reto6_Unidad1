using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Text;

public class SerialThreadASCIIProtocol : AbstractSerialThread
{
    public SerialThreadASCIIProtocol(string portName,
                                   int baudRate,
                                   int delayBeforeReconnecting,
                                   int maxUnreadMessages)
    : base(portName, baudRate, delayBeforeReconnecting, maxUnreadMessages, true)
    {

    }

    protected override void SendToWire(object message, SerialPort serialPort)
    {
        serialPort.WriteLine((string)message);
    }

    protected override object ReadFromWire(SerialPort serialPort)
    {
        return serialPort.ReadLine();
    }

}
