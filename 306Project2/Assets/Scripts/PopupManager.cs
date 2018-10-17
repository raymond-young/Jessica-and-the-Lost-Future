using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {

	// Game objects to hide and show.
    public GameObject popupEnemyPassed;
    public GameObject congratsContainer;
    public Text congratsText;
    public GameObject warningContainer;
    public Text warningText;

	// Use this for initialization
	void Start () {
		// Hide all the popup boxes.
	    if (popupEnemyPassed !=null) {
        	popupEnemyPassed.SetActive(false);
        }

        if (congratsContainer != null) {
        	congratsContainer.SetActive(false);
        }
	}

	// Displayed when an enemy is passed successfully.
    public IEnumerator showEnemyPassed() {
	    popupEnemyPassed.SetActive(true);

	    // Make sure the key is not already pressed.
	    if (Input.anyKeyDown == true) {
			while (Input.anyKeyDown == true) {
            	yield return null;
        	}
		}

		// Wait for a keyclick or button press.
	    while (Input.anyKeyDown == false) {
            yield return null;
        }

	    // Hide the text and prompt
	    popupEnemyPassed.gameObject.SetActive (false);

	}

	public IEnumerator changeConfidence(string percent) {
	    congratsContainer.SetActive(true);


	    congratsText.text = "Confidence +" + percent + "!";

	    // Make sure the key is not already pressed.
	    if (Input.anyKeyDown == true) {
			while (Input.anyKeyDown == true) {
            	yield return null;
        	}
		}

		// Wait for a keyclick or button press.
	    while (Input.anyKeyDown == false) {
            yield return null;
        }

	    // Hide the text and prompt
	    congratsContainer.gameObject.SetActive (false);
	}

	public IEnumerator showWarning(string warning) {
	    warningContainer.SetActive(true);


	    warningText.text = warning;

	    // Make sure the key is not already pressed.
	    if (Input.anyKeyDown == true) {
			while (Input.anyKeyDown == true) {
            	yield return null;
        	}
		}

		// Wait for a keyclick or button press.
	    while (Input.anyKeyDown == false) {
            yield return null;
        }

	    // Hide the text and prompt
	    warningContainer.gameObject.SetActive (false);
	}
}
