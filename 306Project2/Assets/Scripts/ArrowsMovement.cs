using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsMovement : MonoBehaviour {
    int index = 0;
    int previousIndex = 0;

    public GameObject[] list;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Format the button depending on the key pressed
        previousIndex = index;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index++;
            if (index > list.Length - 1) { 
                index = 0;
            }

            FormatButton();
            PlaySelectSound();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            index--;
            if (index < 0) { 
                index = list.Length - 1;
            }

            FormatButton();
            PlaySelectSound();

        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject object1 = list[index];
            Button object2 = object1.gameObject.GetComponent<UnityEngine.UI.Button>();
            object2.onClick.Invoke();
            PlaySelectSound();
        }
    }

    void FormatButton()
    {

        // Cancel the glow for the previous button 
        list[previousIndex].GetComponentInChildren<Text>().GetComponent<Outline>().enabled = false;

        // Glow the current button 
        list[index].GetComponentInChildren<Text>().GetComponent<Outline>().enabled = true;

        GameObject button = list[index];
        float buttonYPosition = button.gameObject.transform.position.y;
        // Move arrow
        transform.position = new Vector2(transform.position.x, buttonYPosition);

    }

    void PlaySelectSound()
    {
        GameObject sound = GameObject.Find("Menu Select Sound");
        sound.GetComponent<AudioSource>().Play(0);

    }
}
