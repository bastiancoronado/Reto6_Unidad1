using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hola : MonoBehaviour
{
    // Start is called before the first frame update
    public float val = 10;
    float t = 0;
    void Start()
    {
        
    }
    void Update() {
        t += Time.deltaTime;
        if (t > 5)
        {
            float translation = t * val;
            transform.Translate(0, 0, 0.1f);
        }

    }
}

