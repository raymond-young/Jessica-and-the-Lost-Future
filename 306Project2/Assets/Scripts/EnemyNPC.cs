using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour {

    private Rigidbody2D enemyBody;
    public float xBound = 0;
    public int maxXBound = 2;
    
   
	// Use this for initialization
	void Start () {
        enemyBody = gameObject.GetComponent<Rigidbody2D>();
        enemyBody.velocity = new Vector2(1, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        xBound = gameObject.transform.position.x;

        //x-direction
        if (xBound >= maxXBound)
        {
            enemyBody.velocity = new Vector2(-1, 0);
        }
        else if (xBound <= (maxXBound * - 1))
        {
            enemyBody.velocity = new Vector2(1, 0);
        }

    }
}
