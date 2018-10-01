﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameGenerator : MonoBehaviour {

    //Arrow enum must match the keycode
	enum Arrow { UpArrow, DownArrow, LeftArrow, RightArrow };

    public GameObject left;
	public GameObject right;
	public GameObject up;
	public GameObject down;
    public Slider slider;

    List<GameObject> arrows = new List<GameObject>();
    // List<Arrow> arrowRef = new List<Arrow>();
    Slider bar;

	static System.Random random = new System.Random();

    int noOfArrows;
    float timeLimit;
    int currentIndex;
    float currentTime;

    void Start () {
        //Set up config using default values
		noOfArrows = 6;
        timeLimit = 10f;

       RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
        
        //Generate bar
        //TODO adjust size of the bar
        bar = Instantiate(slider);
        RectTransform barRectTransform = bar.GetComponent<RectTransform>();
        barRectTransform.SetParent(parentRectTransform);
        barRectTransform.localPosition = new Vector2(0, 0);

 		//Get measurements
        float sliderWidth = barRectTransform.rect.width;
		Debug.Log(sliderWidth);
		float sliderHeight = barRectTransform.rect.height;
		float arrowHeight;// = up.GetComponent<RectTransform>().rect.height;
		float arrowWidth;// = up.GetComponent<RectTransform>().rect.width;
		
        //Generate arrows
        for (int i = 0; i < noOfArrows; i++){
			GameObject arrow = null;
			switch (random.Next(0,4)){
				case 0:
					arrow = Instantiate(up);
                    arrows.Add(arrow);
                    // arrowRef.Add(Arrow.Up);
					break;
				case 1:
					arrow = Instantiate(down);
                    arrows.Add(arrow);
                    // arrowRef.Add(Arrow.Down);
                    break;
				case 2:
					arrow = Instantiate(left);
                    arrows.Add(arrow);
                    // arrowRef.Add(Arrow.Left);
                    break;
				case 3:
					arrow = Instantiate(right);
                    arrows.Add(arrow);
                    // arrowRef.Add(Arrow.Right);
                    break;

			}

            arrow.GetComponent<Image>().color = Color.gray;
            RectTransform arrowRectTransform = arrow.GetComponent<RectTransform>();
			arrowHeight = arrowRectTransform.rect.height;
			arrowWidth = arrowRectTransform.rect.width;
			Debug.Log(arrowHeight);
			Debug.Log(arrowWidth);
			float arrowGap = arrowHeight/2;
			arrowRectTransform.SetParent(barRectTransform);
			arrowRectTransform.localPosition = new Vector2((-sliderWidth/2) + (i + 0.5f) * arrowWidth + (i + 0.5f) * arrowGap, 0);
		}
        // test.GetComponent<RectTransform>().localPosition = new Vector2(canvasWidth,canvasHeight);

        //Get ready for the game
		//the left most arrow is the last index of the array it appears
        currentIndex = 0;
        currentTime = 0f;
    }

	void OnGUI(){
		if(currentIndex < 0){
			//game is over
		}
		Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if(e.keyCode.ToString() == arrows[currentIndex].name && e.keyCode != KeyCode.None){
				arrows[currentIndex].GetComponent<Image>().color = Color.green;
				currentIndex--;
			}else{
				Debug.Log(e.keyCode.ToString());
				Debug.Log(arrows[currentIndex].name);
				Fail();
			}
        }
	}
    

    bool correct = false;
    bool pressed = false;

    void Update()
    {
        // correct = false;
        // pressed = false;
        // switch (arrowRef[currentIndex])
        // {
        //     case Arrow.Up:
        //         Debug.Log("Need to press Up \n");
        //         if (Input.GetKeyDown(KeyCode.UpArrow))
        //         {
        //             correct = true;
        //             pressed = true;
        //         }
        //         else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        //         {
        //             correct = false;
        //             pressed = true;
        //         }
        //         break;
        //     case Arrow.Down:
        //         Debug.Log("Need to press Down \n");
        //         if (Input.GetKeyDown(KeyCode.DownArrow))
        //         {
        //             correct = true;
        //             pressed = true;
        //         }
        //         else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        //         {
        //             correct = false;
        //             pressed = true;
        //         }
        //         break;
        //     case Arrow.Left:
        //         Debug.Log("Need to press Left \n");
        //         if (Input.GetKeyDown(KeyCode.LeftArrow))
        //         {
        //             correct = true;
        //             pressed = true;
        //         }
        //         else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        //         {
        //             correct = false;
        //             pressed = true;
        //         }
        //         break;
        //     case Arrow.Right:
        //         Debug.Log("Need to press Right \n");
        //         if (Input.GetKeyDown(KeyCode.RightArrow))
        //         {
        //             correct = true;
        //             pressed = true;
        //         }
        //         else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        //         {
        //             correct = false;
        //             pressed = true;
        //         }
        //         break;
        // }

        // if (pressed)
        // {
        //     if (correct)
        //     {
        //         //Change color of the correctly pressed key
        //         arrows[currentIndex].GetComponent<Image>().color = Color.green;
        //         //Still more keys to press
        //         if (currentIndex < noOfArrows)
        //         {
        //             currentIndex++;
        //         }
        //         //Finish game
        //         else
        //         {
        //             Finish();
        //         }
        //     }
        //     else //Press wrong key, need to restart the game
        //     {
        //         for (int i = 0; i < currentIndex; i++)
        //         {
        //             arrows[i].GetComponent<Image>().color = Color.gray;
        //         }
        //         currentIndex = 0;
        //     }
        // }
        //Update bar
        bar.value = Mathf.Lerp(0f, 1f, currentTime / timeLimit);
        currentTime += Time.deltaTime;
        if (currentTime >= timeLimit)
        {
            Fail();
        }
    }


    public void Finish()
    {
        Debug.Log("Game Finished. You won");
    }

    public void Fail()
    {
        Debug.Log("Game Finished. You lost");
    }
}