using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public static Vector3 BASE_BALL_POSITION;
    private Rigidbody rb;
    public Slider slider;
    public GameObject arrow;
    private float hitForce;

    // Право-лево
    private const float MIX_POSITION_X = -1f;
    private const float MAX_POSITION_X = 1f;
    public const float POSITION_CHANGE_INDEX = 0.05f;
    public float postitionChangeIndex;

    // Сила удара
    private const float POWER_CHANGE_INDEX = 50f;
    private const float MAX_POWER = 3000f;
    private const float MIN_POWER = 1000F;
    public float powerChangeIndex = 50f;
    private float forceXValue;
    private Vector3 force;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        BASE_BALL_POSITION = this.transform.position;

        setDefaults();
    }

    public void setDefaults()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.position = BASE_BALL_POSITION;

        GameManager.Instance.ChangeState(GameState.ChooseThrowDirection);

        postitionChangeIndex = POSITION_CHANGE_INDEX;
        powerChangeIndex = POWER_CHANGE_INDEX;

        hitForce = MIN_POWER;
        forceXValue = 0;
        force = new Vector3(forceXValue, 0, 1);

    }

    void handleSubmit()
    {
        GameState state = GameManager.Instance.GetState();

        if (state == GameState.ChooseThrowDirection)
        {
            GameManager.Instance.ChangeState(GameState.ChooseThrowPower);
        }
        else if (state == GameState.ChooseThrowPower)
        {
            Throw();
        }
    }

    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        GameState state = GameManager.Instance.GetState();
        if (state != GameState.BallThrown)
        {
            if (state == GameState.ChooseThrowDirection)
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
            else if (state == GameState.ChooseThrowPower)
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
        }
    }

    void UpdateForceUI()
    {
        slider.value = hitForce;
    }

    void UpdateRotationUI()
    {
        Quaternion target = Quaternion.Euler(0, forceXValue * 50f, 0);
        arrow.transform.rotation = target;
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Return) && GameManager.Instance.GetState() != GameState.BallThrown)
        {
            handleSubmit();
        }
    }

    void Throw()
    {
        this.force.Set(forceXValue, 0, 1);

        rb.AddForce(this.force * (hitForce * 5.0f) * Time.deltaTime, ForceMode.Impulse);

        GameManager.Instance.ChangeState(GameState.BallThrown);
        GameManager.Instance.IncrementThrowsCount();

        Invoke("setDefaults", 5);
    }
}
