using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {


    public GameObject mainCamera;
    public GameObject minimapImage;
    public GameObject miniMapPlayer;

    private List<GameObject> fogOfWar = new List<GameObject>();

    void Start()
    {
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

    }

    // Removes fog of war as the player moves through the map
    public void RemoveFogOfWar(Collider2D collisonRoom)
    {
        GameObject fogToRemove = null;
        
        foreach (GameObject fog in fogOfWar)
        {
            Debug.Log("C=" + fog.name + "dewfwefwfw=" + collisonRoom.name);
            if (fog.name.Equals(collisonRoom.name))
            {
                fogToRemove = fog;
                break;
                
            }
        }

        if (fogToRemove != null)
        {
            fogOfWar.Remove(fogToRemove);
            Destroy(fogToRemove);
        }
    }


    void LateUpdate () {

	}
}
