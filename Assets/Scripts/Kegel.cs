using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kegel : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 centerOfMass;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        centerOfMass = new Vector3(0, 0, 0);
        rb.centerOfMass = centerOfMass;
    }

    void Update()
    {

    }
}
