using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kegel : MonoBehaviour
{
    private Rigidbody rb;
    public bool isStanding;

    public float rX;
    public float rZ;

    void Start()
    {
        isStanding = true;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isStanding)
        {
            rX = Mathf.Abs(transform.rotation.eulerAngles.x);
            rZ = Mathf.Abs(transform.rotation.eulerAngles.z);

            if ((rX >= 70f && rX <= 270f) || (rZ >= 70f && rZ <= 270f))
            {
                isStanding = false;
                Destroy(this.gameObject, 3);
            }
        }
    }
}
