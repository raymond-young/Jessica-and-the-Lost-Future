using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler {

    private bool allowScrolling = false;
    private GameObject player;
    private GameObject miniMap;

    public float maxSize;
    public float minSize;
    private Vector3 initialCamPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        miniMap = GameObject.FindGameObjectWithTag("minimap");

        //Min and max sizes for scrolling
        maxSize = miniMap.GetComponent<Camera>().orthographicSize;
        minSize = 8f;
        initialCamPos = miniMap.transform.position;
    }

    //Allow scrolling on pointer entering minimap
    public void OnPointerEnter(PointerEventData eventData)
    {
        allowScrolling = true;
        
    }

    //Remove scrolling on pointer exiting minimap
    public void OnPointerExit(PointerEventData eventData)
    {
        allowScrolling = false;
    }

    //Manages scrolling
    public void OnScroll(PointerEventData eventData)
    {
        if (allowScrolling)
        {
            //If zoom in
            if (eventData.scrollDelta.y >= 1)
            {
                Camera cam = miniMap.GetComponent<Camera>();

                //Check max size hasnt been exceeded
                if (cam.orthographicSize > minSize)
                {
                    //Zoom in and change camera pos
                    cam.orthographicSize = cam.orthographicSize - 1f;
                    cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
                }
            }
            //If zoom out
            else
            {
                Camera cam = miniMap.GetComponent<Camera>();

                //Check min size hasnt been exceeded
                if (cam.orthographicSize < maxSize)
                {
                    //Zoom in and change camera pos
                    cam.orthographicSize = cam.orthographicSize + 1f;
                    cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
                }
                else
                {
                    //Resets camera pos to map size if fully zoomed out
                    cam.transform.position = initialCamPos;
                }

            }
            
        }
    }

    void LateUpdate()
    {
        //Updates the camera pos to the player if zoomed in
        Camera cam = miniMap.GetComponent<Camera>();
        if (cam.transform.position != initialCamPos)
        {
            cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        }
    }


}
