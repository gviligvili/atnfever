using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

    public float speed = 3f;
    public float rotationSpeed = 200f;
    public bool isAlive;
    public Animator animator;
    float horizontal = 0;

	private void Start()
	{
        animator = GetComponent<Animator>();
        animator.SetBool("alive", isAlive);
	}

	// Update is called once per frame
	void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
	}

	private void FixedUpdate()
	{
        if (isAlive) {
            animator.SetBool("alive", isAlive);

            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.forward * -horizontal * rotationSpeed * Time.deltaTime);
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("TRIGGERRR");
        if (collision.tag == "KillsPlayer") {
            killPlayer();
            FindObjectOfType<GameManager>().EndGame();
        }
	}

    public void killPlayer() {
        isAlive = false;
        animator.SetBool("alive", isAlive);
    }
}
