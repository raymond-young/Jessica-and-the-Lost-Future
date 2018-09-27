using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void LateUpdate () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 20.0f;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 20.0f;

        transform.Translate(x, 0, 0);
        transform.Translate(0, y, 0);
    }
}
