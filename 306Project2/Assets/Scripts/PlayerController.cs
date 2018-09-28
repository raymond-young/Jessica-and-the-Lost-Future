using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D playerBody;
    private int speed = 10;
    public Slider healthBar;

    private GameObject storeLives;

    private List<GameObject> power = new List<GameObject>();

	// Use this for initialization
	void Start () {
 
        playerBody = gameObject.GetComponent<Rigidbody2D>();

        storeLives = GameObject.FindGameObjectWithTag("StoreLives");

        
        for (int i = storeLives.transform.childCount - 1; i >= 0; i--)
        {
            if (storeLives.transform.GetChild(i).gameObject.name.Equals("Life"))
            {
                power.Add(storeLives.transform.GetChild(i).gameObject);
            }
            
        }

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
    
    //Damage is a percentage between 0-100
    public void OnDamage(double damagePercent)
    {
        healthBar.value = healthBar.value + (float)(damagePercent / 100.0);
    }

    public void LooseOnePower()
    {
        bool gameOver = true;

        //Loop to remove a life if one is still avaliable
        foreach (GameObject power in power)
        {
            if (power.GetComponent<Image>().enabled == true)
            {
                power.GetComponent<Image>().enabled = false;
                gameOver = false;
                break;
            }
           
        }

        if (gameOver)
        {
            //Game over here code----------------------------------------
        }
    }

    public void GainOnePower()
    {
        foreach (GameObject power in power)
        {
            if (power.GetComponent<Image>().enabled == false)
            {
                power.GetComponent<Image>().enabled = true;
                break;
            }

        }
    }
}
