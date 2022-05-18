using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kegel : MonoBehaviour
{
    private Rigidbody rb;
    public bool isStanding;

    private AudioSource hitAudioSource;
    public float rX;
    public float rZ;

    void Start()
    {
        isStanding = true;
        rb = GetComponent<Rigidbody>();
        hitAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        hitAudioSource.Play();
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
                GameManager.Instance.OnKegelFall(this.gameObject);
            }
        }
    }
}
