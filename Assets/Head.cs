using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedPowerUpEnum { FASTER, SLOWER, NONE }

public class Head : MonoBehaviour
{

    public float baseSpeed = 2f;
    public float speedPowerUpBuffer = 1.5f;
    public float rotationSpeed = 200f;
    public bool isAlive;
    public SpeedPowerUpEnum speedPowerUpStatus = SpeedPowerUpEnum.NONE;

    public Animator animator;
    float horizontal = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("alive", isAlive);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            animator.SetBool("alive", isAlive);
            float currSpeed = CalculateSpeed();
            transform.Translate(Vector2.up * currSpeed * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.forward * -horizontal * rotationSpeed * Time.deltaTime);
        }
    }

    float CalculateSpeed()
    {
        float currSpeed = baseSpeed;

        // If speedPowerUp is active 
        if (speedPowerUpStatus == SpeedPowerUpEnum.FASTER)
        {
            currSpeed = baseSpeed * speedPowerUpBuffer;
        }
        else if (speedPowerUpStatus == SpeedPowerUpEnum.SLOWER)
        {
            currSpeed = baseSpeed * -speedPowerUpBuffer;
        }

        Debug.Log("Speed: " + currSpeed);
        return currSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGERRR");
        if (collision.tag == "KillsPlayer")
        {
            killPlayer();
            FindObjectOfType<GameManager>().EndGame();
        }

        if (collision.tag == "PowerUp")
        {
            PowerUp powerUp = collision.gameObject.GetComponent<PowerUp>();
            handlePowerUp(powerUp);
        }
    }

    public void handlePowerUp(PowerUp powerUp)
    {
        switch (powerUp.powerUpKind)
        {
            case PowerUpsEnum.SelfSpeed:
                makeFaster();
                break;
            case PowerUpsEnum.SelfSlow:
                makeSlower();
                break;
            default:
                Debug.Log("A power up didn't catch in the switch case.");
                break;
        }
    }

    public void makeFaster()
    {
        speedPowerUpStatus = SpeedPowerUpEnum.FASTER;
    }

    public void makeSlower()
    {
        speedPowerUpStatus = SpeedPowerUpEnum.SLOWER;
    }

    public void killPlayer()
    {
        isAlive = false;
        animator.SetBool("alive", isAlive);
    }
}
