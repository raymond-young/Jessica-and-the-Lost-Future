using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Controls players currently held items */
public class ItemController : MonoBehaviour {

    private List<GameObject> items = new List<GameObject>();

    private List<Sprite> itemSprites = new List<Sprite>();

    private Collider2D currentItemZone;

    private GameObject freeItemSlot;

    private bool inItemZone = false;

    private PlayerController player;


    // Use this for initialization
    void Start() {

        GameObject itemSlots = GameObject.FindGameObjectWithTag("ItemSlots");

        player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>();


        //Loads item slots
        for (int i = 0; i < itemSlots.transform.childCount; i++)
        {
            items.Add(itemSlots.transform.GetChild(i).gameObject);
        }

        Object[] sprites = Resources.LoadAll("Sprites", typeof(Sprite));
        //Loads item sprites
        for (int i = 0; i < sprites.Length; i++)
        {
            Sprite sprite = (Sprite)sprites[i];
            itemSprites.Add(sprite);
        }
    }

    // Update is called once per frame
    void Update() {

        if (inItemZone)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !currentItemZone.Equals(null))
            {
                freeItemSlot.GetComponent<Image>().sprite = currentItemZone.GetComponent<SpriteRenderer>().sprite;
                items.Remove(currentItemZone.gameObject);
                Destroy(currentItemZone.gameObject);
                player.ChangeConfidence(20);
            }
        }

    }

    /* Sets item zone when approaching item */
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Item"))
        {
            currentItemZone = collision;
            inItemZone = true;

            //Loop through item slots to find one that is free
            foreach (GameObject item in items)
            {
                if (item.GetComponent<Image>().sprite.name.Equals("Background"))
                {
                    freeItemSlot = item;
                }
            }
        }

    }

    /* Nullifies item zone when moving away from item */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Item"))
        {
            currentItemZone = null;
            freeItemSlot = null;
            inItemZone = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //ONLY USED FOR NUNIT TESTING------------------------------------------
        if (collision.gameObject.tag.Equals("Item"))
        {
            //freeItemSlot.GetComponent<Image>().sprite = currentItemZone.GetComponent<SpriteRenderer>().sprite;
            //items.Remove(currentItemZone.gameObject);
            //Destroy(currentItemZone.gameObject);
        }
        //---------------------------------------------------
    }

}
