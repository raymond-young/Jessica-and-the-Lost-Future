using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 20.0f;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 20.0f;

        transform.Translate(x, 0, 0);
        transform.Translate(0, y, 0);

        anim.SetFloat("MoveX", x);
        anim.SetFloat("MoveY", y);
    }
}
