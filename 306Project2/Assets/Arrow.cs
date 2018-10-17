using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour {
	enum State {Default, Wrong, Right};
	// Arrow states.
	public Sprite blueState;
	public Sprite greenState;
	public Sprite redState;
	State state;
	float errorTime;
	float timeCount;

	// Use this for initialization
	void Start () {
		Debug.Log(2);
		TurnDefault();
		state = State.Default;
		errorTime = 0.5f;
		timeCount = 0;
	}
	
	void Update () {
		Debug.Log(1);
		Debug.Log(state);
		if (state == State.Wrong){
			Debug.Log(timeCount);
			timeCount += Time.deltaTime;
			if (timeCount > errorTime){
				Debug.Log("hi");
				timeCount = 0;
				state = State.Default;
				gameObject.GetComponent<Image>().sprite = blueState;
			}
		} else {
			timeCount = 0;
		}
	}

	// Change state to red.
	public void TurnRight(){
		Debug.Log(3);
		gameObject.GetComponent<Image>().sprite = greenState;
		state = State.Right;
	}

	public void TurnWrong(){
		Debug.Log(4);
		gameObject.GetComponent<Image>().sprite = redState;
		state = State.Wrong;
	}

	public void TurnDefault(){
		gameObject.GetComponent<Image>().sprite = blueState;
		if (state != State.Wrong){
			state = State.Default;
		}
	}
}
