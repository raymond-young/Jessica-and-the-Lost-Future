using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backet : MonoBehaviour {

	private FallingBall fallingBallController;
	public Rigidbody2D bucketBody;
	private float speed = 8f;

	// Use this for initialization
	void Start () {
		fallingBallController = FindObjectOfType<FallingBall>();
	}
	
	// Update is called once per frame
	void Update () {
		 if (fallingBallController.gameStarted()) {
            float x = Input.GetAxis("Horizontal");

            // Velocity is movement of bucket * speed.
            bucketBody.velocity = (speed * x * Vector2.right);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject.FindGameObjectWithTag("MinigameFallingBall").GetComponent<FallingBall>().CatchBall(collider.gameObject);
    }
}
