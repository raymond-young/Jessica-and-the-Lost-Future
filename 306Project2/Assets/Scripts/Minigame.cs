using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour {
    ///////////////////////////////////

    enum Hardness { Easy, Hard};

    //////////////////////////////////

    enum Key { Up, Down, Left, Right};
    int keyNum = 4;
    int timeLimit;
    int currentIndex;

    public GameObject HolderPrefab;
    public GameObject UpKeyPrefab;
    public GameObject DownKeyPrefab;
    public GameObject LeftKeyPrefab;
    public GameObject RightKeyPrefab;

    List<GameObject> gameKeys = new List<GameObject>();
 

    //private Keys[] keys;
    private Key[] keys = {Key.Up, Key.Down, Key.Left, Key.Right};

 /* Don't worry about anything between this, still working on it */
    //Trying to generate the minigame bar according to the screen size and number of arrows

    GameObject holder;
    
    //Coefficient
    //Number of arrows changes according to hardness
    private int maxArrowNum = 10;

    private float holderSizeVsScreenWidth = 0.8f;
    private float holderYOffsetVsScreenHeight = 0.1f;

    private float holderX;
    private float holderY;
    private float holderWidth;
    private float holderHeight;
    private float arrowSize;

    private void CalcMeasurements()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        holderX = 0f;
        holderY = screenHeight * holderYOffsetVsScreenHeight;

        arrowSize = (screenWidth * holderSizeVsScreenWidth) / maxArrowNum;

        holderHeight = arrowSize;
        holderWidth = arrowSize * keyNum;
  

        
        holder = Instantiate(HolderPrefab);
        holder.transform.position = holder.transform.position + new Vector3(0, -2, 0);
        Vector3 pos = new Vector3(holderX, holderY, 0);

        Debug.Log("scale = " + pos);
        //holder = Instantiate(HolderPrefab, holder.transform.position + pos, transform.rotation);
        holder.GetComponent<SpriteRenderer>().color = Color.red;

    }
 /* Don't worry about anything between this */

    // Use this for initialization
    void Start () {
        //Initialise keyNum and timeLimit according to hardness
        //keys = getKeys();

        Debug.Log("Started\n");
        //Generate holder
        //holder = Instantiate(HolderPrefab, transform.position, transform.rotation);
        //holder.transform.localScale = new Vector2(1, 1);
        CalcMeasurements();


        //Generate game keys
        foreach (Key k in keys)
        {
            GameObject key;
            switch (k)
            {
                case Key.Up:
                    key = Instantiate(UpKeyPrefab);
                    break;
                case Key.Down:
                    key = Instantiate(DownKeyPrefab);
                    break;
                case Key.Left:
                    key = Instantiate(LeftKeyPrefab);
                    break;
                case Key.Right:
                    key = Instantiate(RightKeyPrefab);
                    break;
                default:
                    key = null;
                    break;
            }
            gameKeys.Add(key);
            key.transform.parent = holder.transform;
        }
        currentIndex = 0;
	}

    bool correct = false;
    bool pressed = false;
    
    // Update is called once per frame
    void Update ()
    {
        Debug.Log("Current index = " + currentIndex + "\n");
        correct = false;
        pressed = false;
        switch (keys[currentIndex])
        {
            case Key.Up:
                Debug.Log("Current index = " + currentIndex + "\n");
                Debug.Log("Need to press Up \n");
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    correct = true;
                    pressed = true;
                    Debug.Log("pressed Up\n");
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    correct = false;
                    pressed = true;
                }
                break;
            case Key.Down:
                Debug.Log("Current index = " + currentIndex + "\n");
                Debug.Log("Need to press Down \n");
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    correct = true;
                    pressed = true;
                    Debug.Log("pressed Down\n");
                }else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    correct = false;
                    pressed = true;
                }
                break;
            case Key.Left:
                Debug.Log("Current index = " + currentIndex + "\n");
                Debug.Log("Need to press Left \n");
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    correct = true;
                    pressed = true;
                    Debug.Log("pressed Left\n");
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    correct = false;
                    pressed = true;
                }
                break;
            case Key.Right:
                Debug.Log("Current index = " + currentIndex + "\n");
                Debug.Log("Need to press Right \n");
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    correct = true;
                    pressed = true;
                    Debug.Log("pressed Right\n");
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    correct = false;
                    pressed = true;
                }
                break;
         }
         Debug.Log("Corrent = " + correct + "\n");
         Debug.Log("Pressed = " + pressed + "\n");
        if (pressed)
        {
            if (correct)
            {
                //Change color of the correctly pressed key
                Debug.Log("Pressed right key. Change color!");
                gameKeys[currentIndex].GetComponent<SpriteRenderer>().color = Color.green;
                //Still more keys to press
                if (currentIndex < gameKeys.Count) 
                {
                    currentIndex++;
                    Debug.Log("More key to press. Index changed to " + currentIndex);
                }
                //Finish game
                else 
                {
                    Finish();
                }
            }
            else //Press wrong key, need to restart the game
            {
                for (int i = 0; i < currentIndex; i++)
                {
                    gameKeys[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                currentIndex = 0;
                Debug.Log("Pressed wrong key. Start over. Index = " + currentIndex);
            }
        }
	}



    private Key[] getKeys()
    {
        Key[] keys = new Key[keyNum];
        for (int i = 0; i < keyNum; i++)
        {
            keys[i] = (Key)Random.Range(0, 5);
        }
        return keys;
    }
    

    public void Finish()
    {
        Debug.Log("Game Finished. You won");
    }

}
