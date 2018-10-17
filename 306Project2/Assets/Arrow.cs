using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour {

	// Arrow states.
	public Sprite blueState;
	public Sprite greenState;
	public Sprite redState;

	// Use this for initialization
	void Start () {
		TurnDefault();
	}
	
	// Change state to red.
	public void TurnRight(){
		gameObject.GetComponent<Image>().sprite = greenState;
	}

	public void TurnWrong(){
		gameObject.GetComponent<Image>().sprite = redState;
	}

	public void TurnDefault(){
		gameObject.GetComponent<Image>().sprite = blueState;
	}
}
