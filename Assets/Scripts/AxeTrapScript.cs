using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrapScript : MonoBehaviour
{
    public float rotationSpeed = 100.0f; // Speed of rotation around the pivot
    public float rotationDirection = 1f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate around its own axis
        transform.Rotate(0, 0, rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
