using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {


    public GameObject mainCamera;
    public GameObject minimapImage;
    public GameObject miniMapPlayer;
    public bool setStartActive = false;

    private GameObject player;
    private int level;

    private List<GameObject> fogOfWar = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        level = player.GetComponent<PlayerController>().GetLevel();

        Camera camera = gameObject.GetComponent<Camera>();

        int resWidth = Screen.width;
        int resHeight = Screen.height;

        RenderTexture renderTexture = new RenderTexture(resWidth, resHeight, 24);

        //Aspect ratio scalling for camera and rendered texture
        float cachedCameraAspect = camera.aspect;
        camera.targetTexture = renderTexture;
        camera.aspect = cachedCameraAspect;


        minimapImage.GetComponent<RawImage>().texture = camera.targetTexture;

        //Gets fog of war map components
        GameObject fwObj = GameObject.FindGameObjectWithTag("FogOfWar");

        for (int i = 0; i < fwObj.transform.childCount; i++)
        {
            fogOfWar.Add(fwObj.transform.GetChild(i).gameObject);
        }

        //Set minimap not active at start of scene for cutscenes
        if (level == 1) {
            minimapImage.transform.parent.gameObject.SetActive(false);
        }
        else if (level == 2) {
            minimapImage.transform.parent.gameObject.SetActive(true);
        }
        else if (level == 3) {
            minimapImage.transform.parent.gameObject.SetActive(true);
        }
        
    }


    // Removes fog of war as the player moves through the map
    public void RemoveFogOfWar(Collider2D collisonRoom)
    {
        GameObject fogToRemove = null;
        
        //Loop through each fog of war object in game
        foreach (GameObject fog in fogOfWar)
        {
            //Check if collision matches fog of war object and if so delete it
            if (fog.name.Equals(collisonRoom.name))
            {
                fogToRemove = fog;
                break;
                
            }
        }

        //Remove from map if fog deleted
        if (fogToRemove != null)
        {
            fogOfWar.Remove(fogToRemove);
            Destroy(fogToRemove);
        }
    }


    void LateUpdate () {

	}
}
