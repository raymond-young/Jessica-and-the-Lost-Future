using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;
using System;

/**
* Represents a player. Controls player movement, dialogue triggering, and camera movement. 
**/
public class PlayerController : MonoBehaviour {

    // Player body.
    private Rigidbody2D playerBody;
    // Speed of the player's movements.
    private int speed = 8;
    // The confidence bar to render the confidence.
    public Slider confidenceBar;
    // Stores the number of lives.
    private GameObject storeLives;

    // Highlights interactable objects when the player is near them.
    private GameObject glow;
    // The items a player has.
    private List<GameObject> power = new List<GameObject>();
    // Stores the score between scenes.
    public GameObject scoreTransfer;

    // Variables for camera movement.
    private bool isTransitioning = false;
    private bool isTeleporting = false;
    public Camera cam;
    private Vector2 newCameraPosition;
    private Collider2D currentRoom;
    private float teleportX;
    private float teleportY;

    // Confidence level (score).
    private float score = 0;
    // Number of lives the player has (energy).
    private int numLives = 0;
    // The text displaying the score.
    public Text scoreText;

    // Animates the player.
    private Animator anim;
    
    // If we should calculate the next position.
    private bool calcNextPos = true;
    // The previous position of the player.
    private Vector2 previousPos;
    
    // For dialogue.
    // Interaction radius until you can pick up an item.
    public float interactionRadius = 2.0f;

    public AchievementManager achievementManager;

    // If the player is currently in a zone that triggers dialogue.
    private bool inNPCZone = false;
    // The zone, if the player is in a zone.
    private Collider2D currentNPCZone;

    // Use this for initialization.
    void Start() {
        // Get the components.
        anim = GetComponent<Animator>();
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        storeLives = GameObject.FindGameObjectWithTag("StoreLives");

        //Prevents rotation of main character.
        playerBody.freezeRotation = true;
        
        // Get each life container.
        for (int i = storeLives.transform.childCount - 1; i >= 0; i--) {
            if (storeLives.transform.GetChild(i).gameObject.name.Equals("Life")) {
                // Add these to the list of life containers.
                power.Add(storeLives.transform.GetChild(i).gameObject);
                // Store the number of lives.
                numLives++;
            }

        }

        // Hide the interactable object glow.
        glow = GameObject.Find("Glow");
        glow.SetActive(false);

        // Update all score text.
        UpdateScoreText();
    }

    // Update is called once per frame.
    // This checks for conversations, room transitions, and item interactions.
    void FixedUpdate()
    {
        
        // Remove all player control when we're in dialogue
        if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true) {
            return;
        }

        // If we're not transitioning between rooms we can move.
        if (!isTransitioning) {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(x, y);

            // Velocity is movement of character * speed.
            playerBody.velocity = (movement * speed);

            //update character Animation to face the right direction
            anim.SetFloat("MoveX",Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("MoveY",Input.GetAxisRaw("Vertical"));

            //Calculates the player's last position every second.
            if (calcNextPos) {
                StartCoroutine(CalcLastPos());
            }
        }
        else {
            // If we are moving between rooms.
            TransitionCamera();
        }

        // Detect if we want to start a conversation
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Start the conversation if so.
            CheckForNearbyNPC();
        }

    }

    // Gets the player's last position to get their last direction before minigame starts.
    private IEnumerator CalcLastPos() {
        calcNextPos = false;
        previousPos = gameObject.transform.position;
        yield return new WaitForSeconds(1);
        calcNextPos = true;
    }

    // Getter method for the player's previous position.
    public Vector3 GetPreviousPos() {
        return previousPos;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
    }

    private void OnCollisionStay2D(Collision2D collision) {
    }

    // Changes the player's confidence. Confidence is a percentage value between 0-100.
    public void ChangeConfidence(double damagePercent) {
        confidenceBar.value = confidenceBar.value + (float)(damagePercent / 100.0);
        if (confidenceBar.value == 1) {
            achievementManager.EarnAchievement("Maxed Out");
        }
        UpdateScoreText();
    }

    // This is used by yarn to change confidence after dialogue interactions.
    [YarnCommand("change_confidence")]
    public void YarnChangeConfidence(string percent) {
        double doublePercent = Double.Parse(percent);
        ChangeConfidence(doublePercent);
    }

    // Decreases the player's lives.
    public void LoseOnePower() {
        bool gameOver = true;

        // Loop to remove a life if one is still available.
        foreach (GameObject power in power) {
            // If there is a life to disable.
            if (power.GetComponent<Image>().enabled == true) {
                power.GetComponent<Image>().enabled = false;
                numLives--;
                if (numLives > 0)
                {
                    gameOver = false;
       
                }
                UpdateScoreText();
                break;
            }
        }

        // If the game is over.
        if (gameOver) {
            // Transfer the score.
            transferScore();
            // Display the end-game scene.
            SceneManager.LoadScene("EndOfLevelScene");
        }
    }

    // Transfers the score between scenes.
    public void transferScore()
    {
        Debug.Log("Before - Score is " + score.ToString());
        GameObject[] transferObjects = GameObject.FindGameObjectsWithTag("scoreTransferObject");
        for(int i = 0; i< transferObjects.Length;i++)
        {
            Destroy(transferObjects[i]);
        }

        Vector3 pos = new Vector3(0, 0, 0);
        GameObject scoreTransferObject = Instantiate(scoreTransfer, pos, Quaternion.identity);
        DontDestroyOnLoad(scoreTransferObject);
        scoreTransferObject.GetComponent<ScoreTransferScript>().setScore(scoreText.text);
        Debug.Log("Score is " + score.ToString());
    }

    // Transfers the achievements between scenes.
    public void transferAchievements()
    {
        // Debug.Log("Before - Score is " + score.ToString());
        // GameObject[] transferObjects = GameObject.FindGameObjectsWithTag("AchievementManager");
        // for(int i = 0; i< transferObjects.Length;i++)
        // {
        //     Destroy(transferObjects[i]);
        // }

        // Vector3 pos = new Vector3(0, 0, 0);
        // GameObject achievementTransferObject = Instantiate(scoreTransfer, pos, Quaternion.identity);
        GameObject[] achievementTransferObject = GameObject.FindGameObjectsWithTag("achieveTransferObj");
        for (int i=0; i< achievementTransferObject.Length; i++){
            DontDestroyOnLoad(achievementTransferObject[i]);
        }
        // achievementTransferObject.GetComponent<ScoreTransferScript>().setScore(scoreText.text);
        // Debug.Log("Score is " + score.ToString());
    }

    // Visually increases one energy.
    public void GainOnePower() {
        // Check each energy icon.
        foreach (GameObject power in power) {
            // If there is one available.
            if (power.GetComponent<Image>().enabled == false) {
                power.GetComponent<Image>().enabled = true;
                numLives++;
                UpdateScoreText();
                break;
            }

        }
    }

    // On exit, hide the interactable item glow.
    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag.Equals("Item") || collider.gameObject.tag.Equals("NPC")) {
            currentNPCZone = null;
            glow.SetActive(false);
            inNPCZone = false;
        }
    }

    // Triggers camera transition when the player enters a room.
    void OnTriggerEnter2D(Collider2D collider)
    {
        // If there is no current room, set it.
        if (currentRoom == null) {
            currentRoom = collider;
        }
        // If the player enters a collision area that is a new room, set the new camera position to the center of the new room.
        if (collider.tag == "Room" && !isTransitioning && currentRoom != collider) {
            isTransitioning = true;
            newCameraPosition = new Vector3(collider.transform.position.x, collider.transform.position.y, cam.transform.position.z);
            // Set the new room.
            currentRoom = collider;
        }
        else if (collider.tag == "Door" && !isTransitioning) {
            // A door, which teleports the player.
            Collider2D newRoom = collider.GetComponent<Door>().linksToRoom;
            // Teleport the player and also set the new room.
            isTransitioning = true;
            isTeleporting = true;
            currentRoom = newRoom;
            newCameraPosition = new Vector3(newRoom.transform.position.x, newRoom.transform.position.y, cam.transform.position.z);

            // Set the position of the player to be teleported.
            teleportX = collider.GetComponent<Door>().playerX;
            teleportY = collider.GetComponent<Door>().playerY;
        } else if (collider.tag == "NPC" || collider.tag == "Item" && !isTransitioning) {
            // If this is an NPC or item, show the interactable item glow.
            inNPCZone = true;
            currentNPCZone = collider;
            Vector2 temp = collider.gameObject.transform.position;
            glow.transform.position = temp;
            glow.SetActive(true);
            // achievementManager.EarnAchievement("Glow");
        }  else if (collider.tag == "EventZone" && !isTransitioning) {
            // Start any dialogue that is automatically triggered.
            SetMotionToZero();
            FindObjectOfType<DialogueRunner>().StartDialogue(collider.GetComponent<GoodNPC>().talkToNode);
        }
    }

    // Used to stop the player moving when the minigame starts
    public void SetMotionToZero() {
        playerBody.velocity = new Vector2(0,0);
        playerBody.rotation = 0;
        playerBody.angularVelocity = 0;
    }

    // Calculates the score.
    public int CalculateScore()  {
        // Score calulated from confidence + the number of lives left.
        int score = (int)(confidenceBar.value * 100) + ((int)numLives * 50);
        return score;
    }

    public void UpdateScoreText() {
        scoreText.text = "Score: " + CalculateScore().ToString();

        Debug.Log(scoreText.text+"");
    }
    
    // Move the camera to the center of the new room.
    void TransitionCamera() {
        cam.transform.position = newCameraPosition;
        isTransitioning = false;
        if (isTeleporting) {
            playerBody.transform.position = new Vector3(teleportX, teleportY, playerBody.transform.position.z);
            isTeleporting = false;
        }
    }

    /** Find all DialogueParticipants. Filter them to those that have a Yarn start node and are in range; 
     * then start a conversation with the first one
     */
    public void CheckForNearbyNPC() {
         if (inNPCZone) {
            SetMotionToZero();
            // Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(currentNPCZone.GetComponent<GoodNPC>().talkToNode);
        }
    }

    // Changes the scene to a different level.
    [YarnCommand("transition")]
    public void ChangeLevel(string destination) {
        transferScore();
        SceneManager.LoadScene(destination);
    }

    // Shows the player on game start.
    [YarnCommand("move_player")]
    public void Show(string destination) {
        Vector2 start = new Vector2(0, -2);
        gameObject.transform.position = start;
        cam.transform.position = new Vector2(1, -1);
    }

    // Repels player from walls when they are not allowed to go there yet.
    [YarnCommand("repel")]
    public void RepelPlayer() {
        // Calculates the opposite direction to the NPC from the player's input.
        Vector2 direction = (transform.position).normalized;

        transform.Translate(-direction);
    }

}