using System.Collections;
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
	public GameObject box;
    public Slider slider;

    List<GameObject> arrows = new List<GameObject>();
    List<Arrow> arrowRef = new List<Arrow>();
    Slider bar;
	GameObject holder;

	static System.Random random = new System.Random();

    int noOfArrows;
    float timeLimit;
    int currentIndex;
    float currentTime;

	float timePenalty;

    void Start () {
        //Set up config using default values
		noOfArrows = 6;
        timeLimit = 10f;
		timePenalty = timeLimit / noOfArrows;

		RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
	  	float arrowSpace = up.GetComponent<RectTransform>().rect.width * 1.5f;
		
        //Generate bar
        //TODO adjust size of the bar
		holder = Instantiate(box);
        RectTransform holderRectTransform = holder.GetComponent<RectTransform>();
        holderRectTransform.sizeDelta = new Vector2(arrowSpace * noOfArrows, arrowSpace * 1.6f);
        holderRectTransform.SetParent(parentRectTransform);
        holderRectTransform.localPosition = new Vector2(0, 0);

        bar = Instantiate(slider);
        RectTransform barRectTransform = bar.GetComponent<RectTransform>();
        barRectTransform.sizeDelta = new Vector2((gameObject.GetComponentInParent<Canvas>().pixelRect.width - arrowSpace/2), arrowSpace * 0.25f);
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

            arrow.GetComponent<Image>().color = Color.grey;
            RectTransform arrowRectTransform = arrow.GetComponent<RectTransform>();
			arrowRectTransform.SetParent(holderRectTransform);
            arrowRectTransform.localPosition = new Vector2(i * arrowSpace + arrowSpace * 0.5f - arrowSpace * noOfArrows / 2, 0);
        }
        
        //Get ready for the game
		//the left most arrow is the last index of the array it appears
        currentIndex = 0;
        currentTime = 0f;
    }

	void OnGUI(){
		if(currentIndex > noOfArrows - 1){
			Finish();
		}
		Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if(e.keyCode.ToString().Equals(arrowRef[currentIndex].ToString()) && e.keyCode != KeyCode.None){
				arrows[currentIndex].GetComponent<Image>().color = Color.green;
			    currentIndex++;
			}else{
				currentTime += timePenalty;
                for (int i = 0; i <= currentIndex; i++)
                {
                    arrows[i].GetComponent<Image>().color = Color.grey;
                }
                currentIndex = 0;
			}
        }
	}

    void Update()
    {
        //Update bar
        bar.value = Mathf.Lerp(0f, 1f, currentTime / timeLimit);
        currentTime += Time.deltaTime;
        if (currentTime >= timeLimit)
        {
            Fail();
        }
    }


    private void Finish()
    {
        GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(true);
    }

    private void Fail()
    {
        GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(false);
    }
}
