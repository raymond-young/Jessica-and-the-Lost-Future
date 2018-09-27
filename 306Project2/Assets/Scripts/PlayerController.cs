using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D playerBody;
    private int speed = 10;
    public Slider healthBar;

	// Use this for initialization
	void Start () {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {


        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(x, y);

        //Movement of character * speed
        playerBody.velocity = (movement * speed);

        //Prevents rotation on collision
        if (playerBody.rotation != 0)
        {
            playerBody.rotation = 0;
            onDamage(10.0);
  
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Prevents rotation of character on corners
        playerBody.angularVelocity = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Prevents rotation of character on corners
        playerBody.angularVelocity = 0;
    }

    private void onDamage(double damagePercent)
    {
        Debug.Log(healthBar.value);
        healthBar.value = healthBar.value - (float)(damagePercent / 100.0);
        //RectTransform rt = healthBar.GetComponent<RectTransform>();

        // double change = rt.rect.width * (damagePercent / 100.0);

        // rt.sizeDelta = new Vector2(rt.rect.width - (float) change, rt.rect.height);
        //  rt.position = new Vector2(rt.position.x - (float)change, rt.position.y);

    }
}
