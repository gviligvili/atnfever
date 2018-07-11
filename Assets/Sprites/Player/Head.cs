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
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public Animator animator;

    float horizontal = 0;
    Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("alive", isAlive);
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(LeftKey)) {
            horizontal = -1;
        } else if (Input.GetKey(RightKey)){
            horizontal = 1;
        } else {
            horizontal = 0;
        }
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
            currSpeed = baseSpeed / speedPowerUpBuffer;
        }

        return currSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillsPlayer")
        {
            headHit();
        }

        if (collision.tag == "PowerUp")
        {
            PowerUp powerUp = collision.gameObject.GetComponent<PowerUp>();
            player.pickedPowerUp(powerUp);
        }
    }

    public void setSpeedPowerUpStatus(SpeedPowerUpEnum status) {
        speedPowerUpStatus = status;
    }

    public void setKeys(string Left, string Right) {
        LeftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), Left);
        RightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), Right);
    }

    public void headHit()
    {
        player.kill();
    }

    public void killHead()
    {
        isAlive = false;
        animator.SetBool("alive", isAlive);
    }
}
