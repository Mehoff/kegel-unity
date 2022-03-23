using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    public Slider slider;
    public Text rotationText;
    public GameObject arrow;
    private float hitForce;
    public bool isThrown;

    // Право-лево
    private const float MIX_POSITION_X = -1f;
    private const float MAX_POSITION_X = 1f;
    public const float POSITION_CHANGE_INDEX = 0.05f;
    public float postitionChangeIndex;
    public bool isPositionSet;

    // Сила удара
    private const float POWER_CHANGE_INDEX = 50f;
    private const float MAX_POWER = 3000f;
    private const float MIN_POWER = 1000F;
    public float powerChangeIndex = 50f;
    public bool isPowerSet;
    private float forceXValue;
    private Vector3 force;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        setDefaults();
    }

    void setDefaults()
    {
        isThrown = false;

        isPositionSet = false;
        postitionChangeIndex = POSITION_CHANGE_INDEX;

        isPowerSet = false;
        powerChangeIndex = POWER_CHANGE_INDEX;

        hitForce = MIN_POWER;
        forceXValue = 0;
        force = new Vector3(forceXValue, 0, 1);

    }

    void setIsPositionSet(bool value)
    {
        isPositionSet = value;
    }

    void setIsPowerSet(bool value)
    {
        isPowerSet = value;
    }

    void handleSubmit()
    {
        Debug.Log("Handle submit");
        if (!isPositionSet)
        {
            setIsPositionSet(true);
            Debug.Log("Position set");
            return;
        }
        else if (!isPowerSet)
        {
            setIsPowerSet(true);
            Debug.Log("Power set");
            return;
        }

    }

    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        if (!isThrown)
        {
            if (!isPositionSet)
            {
                if (forceXValue >= MAX_POSITION_X)
                {
                    postitionChangeIndex *= -1;
                }
                else if (forceXValue <= MIX_POSITION_X)
                {
                    postitionChangeIndex = POSITION_CHANGE_INDEX;
                }

                forceXValue += postitionChangeIndex;
                UpdateRotationUI();
                return;
            }
            else if (!isPowerSet)
            {
                if (hitForce >= MAX_POWER)
                {
                    powerChangeIndex *= -1;
                }
                else if (hitForce <= MIN_POWER)
                {
                    powerChangeIndex = POWER_CHANGE_INDEX;
                }

                hitForce += powerChangeIndex;

                UpdateForceUI();
                return;
            }
            Throw();
        }
    }

    void UpdateForceUI()
    {
        slider.value = hitForce;
    }

    void UpdateRotationUI()
    {
        arrow.transform.Rotate(0, forceXValue, 0);
        rotationText.text = "X: " + forceXValue.ToString("0.00");
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isThrown)
        {
            handleSubmit();
        }
    }

    void Throw()
    {
        this.force.Set(forceXValue, 0, 1);
        rb.AddForce(this.force * hitForce * Time.deltaTime, ForceMode.VelocityChange);
        isThrown = true;
    }
}
