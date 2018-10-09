using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IScrollHandler {

    private bool allowScrolling = false;
    private GameObject player;
    private GameObject miniMap;

    private float maxSize;
    private float minSize;
    private Vector3 initialCamPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        miniMap = GameObject.FindGameObjectWithTag("minimap");

        maxSize = miniMap.GetComponent<Camera>().orthographicSize;
        minSize = 8f;
        initialCamPos = miniMap.transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        allowScrolling = true;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        allowScrolling = false;
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (allowScrolling)
        {
            if (eventData.scrollDelta.y == 1)
            {
                Camera cam = miniMap.GetComponent<Camera>();

                if (cam.orthographicSize > minSize)
                {
                    cam.orthographicSize = cam.orthographicSize - 1f;
                    cam.transform.position = player.transform.position;
                }
            }
            else
            {
                Camera cam = miniMap.GetComponent<Camera>();
      
                if (cam.orthographicSize < maxSize)
                {
                    cam.orthographicSize = cam.orthographicSize + 1f;
                    cam.transform.position = player.transform.position;
                }
                else
                {
                    cam.transform.position = initialCamPos;
                }

            }
            
        }
    }


}
