using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody rb;

    private float hitForce = 500.0f;
    private bool isThrown;
    private Vector3 force;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isThrown = false;
        force = new Vector3(0, 0, 1);
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetButton("Submit") && !isThrown)
            Throw();
    }

    void Throw()
    {
        rb.AddForce(this.force * hitForce * Time.deltaTime, ForceMode.Impulse);
        // isThrown = true;
    }
}
