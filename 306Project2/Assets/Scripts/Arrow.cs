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
		TurnDefault();
		state = State.Default;
		errorTime = 0.3f;
		timeCount = 0;
	}
	
	void Update () {
		// Gradually fade away the red colour if it's in the wrong state.
		if (state == State.Wrong){
			if (timeCount > errorTime){
				timeCount = 0;
				state = State.Default;
				gameObject.GetComponent<Image>().sprite = blueState;
			}

			float l = Mathf.Lerp(0f, 1f, timeCount/errorTime);
            gameObject.GetComponent<Image>().color = new Color(158f * l/255f, 178f* l/255f, 230f* l/255f);

            // Increase the time since it changed.
			timeCount += Time.deltaTime;
		} else {
			gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
			timeCount = 0;
		}
	}

	// Change state to red.
	public void TurnRight(){
		gameObject.GetComponent<Image>().sprite = greenState;
		state = State.Right;
        //PlayCorrectSound();
	}

	public void TurnWrong(){
		gameObject.GetComponent<Image>().sprite = redState;
		state = State.Wrong;
        //PlayWrongSound();
	}

	public void TurnDefault(){
		gameObject.GetComponent<Image>().sprite = blueState;
		if (state != State.Wrong){
			state = State.Default;
		}
	}

    public void PlayCorrectSound()
    {
        Debug.Log("Play Sound");
        GameObject sound = GameObject.Find("Arrow Correct");
        //sound.GetComponent<AudioSource>().Play(0);

    }

    public void PlayWrongSound()
    {
        Debug.Log("Play Sound");
        GameObject sound = GameObject.Find("Arrow Wrong");
        //sound.GetComponent<AudioSource>().Play(0);
    }
}
