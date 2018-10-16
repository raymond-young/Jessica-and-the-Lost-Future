using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        float f = Random.Range(9000, 10000);
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * f);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<RectTransform>().position.y < 0)
        {
            Destroy(gameObject);
        }

    }
}
