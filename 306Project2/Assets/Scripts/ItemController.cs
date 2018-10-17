using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

/* Controls players currently held items */
public class ItemController : MonoBehaviour {

    private List<GameObject> items = new List<GameObject>();

    private List<Sprite> itemSprites = new List<Sprite>();

    private Collider2D currentItemZone;

    public  GameObject freeItemSlot;

    private bool inItemZone = false;

    private PlayerController player;

    private bool noteRead = false;
    private bool needCoffee = false;


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
                if (currentItemZone.name == "Printer" )
                {
                    if (noteRead)
                    {
                        pickupItem();
                    }
                }else if(currentItemZone.name == "Coffee")
                {
                    if (needCoffee)
                    {
                        pickupItem();
                    }
                }
                else {
                    pickupItem();
                }
            }
        }

    }

    private void pickupItem()
    {
        freeItemSlot.GetComponent<Image>().sprite = currentItemZone.GetComponent<SpriteRenderer>().sprite;
        items.Remove(currentItemZone.gameObject);
        Destroy(currentItemZone.gameObject);
        player.ChangeConfidence(20);
    }

    /* Sets item zone when approaching item */
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Item"))
        {
            currentItemZone = collision;
            inItemZone = true;
            Debug.Log("triggered");
            //Loop through item slots to find one that is free
            foreach (GameObject item in items)
            {
                Debug.Log("triggered");
                    freeItemSlot = item;
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

    [YarnCommand("remove")]
    public void RemoveItem(string destination)
    {
        Debug.Log("item removed triggered");
        GameObject ItemSlots = GameObject.FindGameObjectWithTag("ItemSlots");
        ItemSlots.GetComponentInChildren<Image>().sprite = null;
    }

    [YarnCommand("readNote")]
    public void ReadNote(string destination)
    {
        noteRead = true;
    }

    [YarnCommand("needCoffee")]
    public void NeedCoffee(string destination)
    {
        needCoffee = true;
    }
}
