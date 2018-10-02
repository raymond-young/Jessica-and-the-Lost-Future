using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{

	private Rigidbody2D playerBody;
	private int speed = 10;
	public Slider confidenceBar;

	private GameObject storeLives;

	private List<GameObject> power = new List<GameObject>();

    public GameObject scoreTransfer;
	// Variables for camera movement.
	private bool isTransitioning = false;
	private bool isTeleporting = false;
	public Camera cam;
	private Vector2 newCameraPosition;
	private Collider2D currentRoom;
	private float teleportX;
	private float teleportY;

    private float score = 0;
    private int numLives = 0;
    public Text scoreText;

    private Animator anim;
	
	private bool calcNextPos = true;
    private Vector2 previousPos;
	
	// YarnSpinner
    public float interactionRadius = 2.0f;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        playerBody = gameObject.GetComponent<Rigidbody2D>();

        storeLives = GameObject.FindGameObjectWithTag("StoreLives");

        for (int i = storeLives.transform.childCount - 1; i >= 0; i--)
        {
            if (storeLives.transform.GetChild(i).gameObject.name.Equals("Life"))
            {
                power.Add(storeLives.transform.GetChild(i).gameObject);
                numLives++;
            }

        }
        
        UpdateScoreText();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
		
		// Remove all player control when we're in dialogue
		if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
		{
			return;
		}

		// Not transitioning between rooms.
		if (!isTransitioning)
		{
			var x = Input.GetAxis("Horizontal");
			var y = Input.GetAxis("Vertical");

			Vector2 movement = new Vector2(x, y);

			//Movement of character * speed
			playerBody.velocity = (movement * speed);

			//Prevents rotation on collision
			if (playerBody.rotation != 0)
			{
				playerBody.rotation = 0;

			}

			//update character Animation to face the right direction
			anim.SetFloat("MoveX",Input.GetAxisRaw("Horizontal"));
			anim.SetFloat("MoveY",Input.GetAxisRaw("Vertical"));

			//Calculates the players last position every second
			if (calcNextPos)
			{
				StartCoroutine(CalcLastPos());
			}
		}
		else
		{
			// Is moving between rooms.
			TransitionCamera();
		}

		// Detect if we want to start a conversation
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CheckForNearbyNPC();
		}

	}

    //Gets last player pos to get their last direction before minigame starts
    private IEnumerator CalcLastPos()
    {
        calcNextPos = false;

        previousPos = gameObject.transform.position;

        yield return new WaitForSeconds(1);

        calcNextPos = true;

    }

    //Gets the players prevoius position
    public Vector3 GetPreviousPos()
    {
        return previousPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Prevents rotation of character on corners
        playerBody.angularVelocity = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Prevents rotation of character on corners
        playerBody.angularVelocity = 0;
    }

    // Confidence is a percentage between 0-100
    public void ChangeConfidence(double damagePercent)
    {
        confidenceBar.value = confidenceBar.value + (float)(damagePercent / 100.0);
        UpdateScoreText();
    }

    public void LoseOnePower()
    {
        bool gameOver = true;

        //Loop to remove a life if one is still avaliable
        foreach (GameObject power in power)
        {
            if (power.GetComponent<Image>().enabled == true)
            {
                power.GetComponent<Image>().enabled = false;
                gameOver = false;
                numLives--;
                UpdateScoreText();
                break;
            }

        }

        if (gameOver)
        {
            //Game over here code----------------------------------------
            Vector3 pos = new Vector3(0,0,0);
            GameObject scoreTransferObject = Instantiate(scoreTransfer, pos, Quaternion.identity);
            DontDestroyOnLoad(scoreTransferObject);
            scoreTransferObject.GetComponent<ScoreTransferScript>().setScore(score);
            SceneManager.LoadScene("EndOfLevelScene");
        }
    }

    public void GainOnePower()
    {
        foreach (GameObject power in power)
        {
            if (power.GetComponent<Image>().enabled == false)
            {
                power.GetComponent<Image>().enabled = true;
                numLives++;
                UpdateScoreText();
                break;
            }

        }
    } 

	// Triggers camera transition when the player enters a room.
	void OnTriggerEnter2D(Collider2D collider)
	{
		// If there is no current room, set it.
		if (currentRoom == null)
		{
			currentRoom = collider;
		}
		// If the player enters a collision area that is a new room, set the new camera position to the center of the new room.
		if (collider.tag == "Room" && !isTransitioning && currentRoom != collider)
		{
			isTransitioning = true;
			newCameraPosition = new Vector3(collider.transform.position.x, collider.transform.position.y, cam.transform.position.z);
			// Set the new room.
			currentRoom = collider;
		}
		else if (collider.tag == "Door" && !isTransitioning)
		{
			// A door, which teleports the player.
			Collider2D newRoom = collider.GetComponent<Door>().linksToRoom;
			Debug.Log(newRoom);
			// Teleport the player and also set the new room.
			isTransitioning = true;
			isTeleporting = true;
			currentRoom = newRoom;
			newCameraPosition = new Vector3(newRoom.transform.position.x, newRoom.transform.position.y, cam.transform.position.z);

			// Set the position of the player to be teleported.
			teleportX = collider.GetComponent<Door>().playerX;
			teleportY = collider.GetComponent<Door>().playerY;
		}
	}

    //Used to stop the player moving when the minigame starts
    public void SetMotionToZero()
    {
        playerBody.velocity = new Vector2(0,0);
        playerBody.rotation = 0;
        playerBody.angularVelocity = 0;
    }


    public float CalculateScore() 
    {
        // Score calulated from confidence + the number of lives left.
        score = (confidenceBar.value * 100) + ((float)numLives * 20);
        return score;
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + CalculateScore().ToString();
    }
	
	// Move the camera to the center of the new room.
	void TransitionCamera()
	{
		cam.transform.position = newCameraPosition;
		isTransitioning = false;
		if (isTeleporting)
		{
			playerBody.transform.position = new Vector3(teleportX, teleportY, playerBody.transform.position.z);
			isTeleporting = false;
		}
	}

	/// Find all DialogueParticipants
	/** Filter them to those that have a Yarn start node and are in range; 
	 * then start a conversation with the first one
	 */
	public void CheckForNearbyNPC()
	{
		var allParticipants = new List<GoodNPC>(FindObjectsOfType<GoodNPC>());
		var target = allParticipants.Find(delegate (GoodNPC p) {
			return string.IsNullOrEmpty(p.talkToNode) == false && // has a conversation node?
			(p.transform.position - this.transform.position)// is in range?
			.magnitude <= interactionRadius;
		});
		if (target != null)
		{
			// Kick off the dialogue at this node.
			FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
		}
	}
}

