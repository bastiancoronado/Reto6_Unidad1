using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hola : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update() {
        float translation = Time.deltaTime * 10;
        transform.Translate(0, 0, translation);
    }
}

