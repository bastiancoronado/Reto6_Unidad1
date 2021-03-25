using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class SerialConfigurate : MonoBehaviour
{
    public GameObject Control;
    public GameObject obj;
    public GameObject Canvas;
    public GameObject Canon;

    string[] boudrate = { "9600",  "19200", "38400", "115200" };
    public Dropdown Por32, PorArd, Bou32, BouArd;
    public string[] serialPorts;
    
    void Start()
    {

        List<string> Ports = new List<string>();
        List<string> BoundRates = new List<string>();

        serialPorts = SerialPort.GetPortNames();

        for (uint i = 0; i<serialPorts.Length; i++) Ports.Add(serialPorts[i]);

        Por32.AddOptions(Ports);
        PorArd.AddOptions(Ports);

        for (uint i = 0; i < boudrate.Length; i++) BoundRates.Add(boudrate[i]);

        Bou32.AddOptions(BoundRates);
        BouArd.AddOptions(BoundRates);

    }

    
    void Update()
    {
        
    }

    public void Activar()
    {
        Control.SetActive(true);
        obj.SetActive(true);     
        Canon.SetActive(true);
    }

    public void Desactivar()
    {
        Control.SetActive(false);
        obj.SetActive(false);
        Canon.SetActive(false);
    }

    public void Window()
    {
        Activar();
        Canvas.SetActive(false);
    }
}
