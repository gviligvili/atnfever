using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

    public float speed = 3f;
    public float rotationSpeed = 200f;
    public bool isDead = false;

    float horizontal = 0;

	// Update is called once per frame
	void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
	}

	private void FixedUpdate()
	{
        if (!isDead) {
            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.forward * -horizontal * rotationSpeed * Time.deltaTime);
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("TRIGGERRR");
        if (collision.tag == "KillsPlayer") {
            isDead = true;
            FindObjectOfType<GameManager>().EndGame();
        }
	}
}
