using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameGenerator : MonoBehaviour {

    //Arrow enum must match the keycode
	enum Arrow { UpArrow, DownArrow, LeftArrow, RightArrow };

    //Prefabs
    public GameObject left;
	public GameObject right;
	public GameObject up;
	public GameObject down;
	public GameObject box;
    public Slider slider;
    public GameObject readyPrefab;
    public GameObject goPrefab;

    //Placeholders that hold game objects that will be random generated during the game
    List<GameObject> arrows = new List<GameObject>();
    List<Arrow> arrowRef = new List<Arrow>();
    Slider bar;
	GameObject holder;
    GameObject ready;
    GameObject go;

    //Game related variables
    static System.Random random = new System.Random();

    int noOfArrows;
    
    float timeLimit;
    float currentTime;
    float readyTime = 0.9f;
    float goTime = 0.5f;

    int currentIndex;
    bool gameStart;

    float timePenalty;

    void Start () {
        //Set up config using default values
		noOfArrows = 6;
        timeLimit = 10f;
		timePenalty = timeLimit / noOfArrows;

        //Get measurements of the canvas
		RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
        //Calculate size assigned to one arrow
	  	float arrowSpace = up.GetComponent<RectTransform>().rect.width * 1.5f;
		
        //Generate holder and bar. Set default properties according to the screen size and number of arrows
		holder = Instantiate(box);
        RectTransform holderRectTransform = holder.GetComponent<RectTransform>();
        holderRectTransform.sizeDelta = new Vector2(arrowSpace * noOfArrows, arrowSpace * 1.6f);
        holderRectTransform.SetParent(parentRectTransform);
        holderRectTransform.localPosition = new Vector2(0, 0);

        bar = Instantiate(slider);
        RectTransform barRectTransform = bar.GetComponent<RectTransform>();
        barRectTransform.sizeDelta = new Vector2(gameObject.GetComponentInParent<Canvas>().pixelRect.width * 0.95f, arrowSpace * 0.25f);
		float sliderYPosition = gameObject.GetComponentInParent<Canvas>().pixelRect.height/2 - barRectTransform.rect.height;
        barRectTransform.SetParent(parentRectTransform);
        barRectTransform.localPosition = new Vector2(0, -sliderYPosition);
        
        //Generate arrows
        for (int i = 0; i < noOfArrows; i++){
			GameObject arrow = null;
			switch (random.Next(0,4)){
				case 0:
					arrow = Instantiate(up);
                    arrows.Add(arrow);
                    arrowRef.Add(Arrow.UpArrow);
					break;
				case 1:
					arrow = Instantiate(down);
                    arrows.Add(arrow);
                    arrowRef.Add(Arrow.DownArrow);
                    break;
				case 2:
					arrow = Instantiate(left);
                    arrows.Add(arrow);
                    arrowRef.Add(Arrow.LeftArrow);
                    break;
				case 3:
					arrow = Instantiate(right);
                    arrows.Add(arrow);
                    arrowRef.Add(Arrow.RightArrow);
                    break;
			}
            //Set default properties of an arrow
            arrow.GetComponent<Image>().color = Color.grey;
            RectTransform arrowRectTransform = arrow.GetComponent<RectTransform>();
			arrowRectTransform.SetParent(holderRectTransform);
            arrowRectTransform.localPosition = new Vector2(i * arrowSpace + arrowSpace * 0.5f - arrowSpace * noOfArrows / 2, 0);
            arrow.SetActive(false);
        }
        //Generate Ready/Go and set default properties
        RectTransform textRectTransform = holder.GetComponent<RectTransform>();

        ready = Instantiate(readyPrefab);
        RectTransform readyRectTransform = ready.GetComponent<RectTransform>();
        readyRectTransform.sizeDelta = new Vector2(arrowSpace * noOfArrows, arrowSpace * 1.6f);
        readyRectTransform.SetParent(parentRectTransform);
        readyRectTransform.localPosition = new Vector2(0, 0);
        ready.SetActive(false);

        go = Instantiate(goPrefab);
        RectTransform goRectTransform = go.GetComponent<RectTransform>();
        goRectTransform.sizeDelta = new Vector2(arrowSpace * noOfArrows, arrowSpace * 1.6f);
        goRectTransform.SetParent(parentRectTransform);
        goRectTransform.localPosition = new Vector2(0, 0);
        go.SetActive(false);

        //Get ready for the game
        //the left most arrow is the last index of the array it appears
        currentIndex = 0;
        currentTime = -readyTime - goTime;
        gameStart = false;
        bar.value = 0;
    }

	void OnGUI(){
        //Finish game when tehre is no more arrows to press
		if(currentIndex > noOfArrows - 1)
        {
			Finish();
		}
        if(gameStart){
        //Listen to key press event
		Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            //If the right key was pressed, change color of the game object and move to the next one
            if(e.keyCode.ToString().Equals(arrowRef[currentIndex].ToString()) && e.keyCode != KeyCode.None){
				arrows[currentIndex].GetComponent<Image>().color = Color.green;
			    currentIndex++;
			}else{
                //If the wrong key was pressed, add time penalty, reset progress
				currentTime += timePenalty;
                for (int i = 0; i <= currentIndex; i++)
                {
                    arrows[i].GetComponent<Image>().color = Color.grey;
                }
                currentIndex = 0;
			}
        }
        }
	}

    void Update()
    {
        if (!gameStart)
        {
            if (currentTime >= 0) //Start game. Set arrows visible
            {
                go.SetActive(false);
                ready.SetActive(false);
                foreach (GameObject arrow in arrows)
                {
                    arrow.SetActive(true);
                }
                gameStart = true;
                currentTime = 0;
            }
            else if (Mathf.Abs(currentTime) < goTime) //Show "Go!"
            {
                if (go.activeSelf)
                {
                    float time = Mathf.Sin(Mathf.Lerp(0f, 1f, Mathf.Abs(currentTime) / goTime));
                    go.GetComponent<Text>().color = new Color(time, time, 0);
                }
                else
                {
                    go.SetActive(true);
                    ready.SetActive(false);
                }
            }
            else //Show "Read?"
            {
                if (ready.activeSelf)
                {
                    float percentage = Mathf.Abs(currentTime) - goTime;
                    float time = Mathf.Sin(Mathf.Lerp(0f, 1f, percentage / readyTime));
                    ready.GetComponent<Text>().color = new Color(0, time, 0);
                }
                else
                {
                    go.SetActive(false);
                    ready.SetActive(true);
                }
            }
        }
        else
        {
            //Update bar according to time
            bar.value = Mathf.Lerp(0f, 1f, currentTime / timeLimit);
            //Fail the game when time limit has been reached
            if (currentTime >= timeLimit)
            {
                Fail();
            }
        }

        //Increment time
        currentTime += Time.deltaTime;
    }


    private void Finish()
    {
        //Notify the game manager that the player has successfully finished the game
        GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(true);
    }

    private void Fail()
    {
        //Notify the game manager that the player has failed the game
        GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(false);
    }
}
