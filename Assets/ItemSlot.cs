using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public RectTransform rectTransformTarget;
    public RectTransform rectTransformTargetMILK;
    public RectTransform rectTransformTarget3;
    public int x;
    public int y;

    //uses the image of an empty cup
    public Image cup;
    public Image jug;

    //the sprties being used for coffee
    public Sprite emptyCup;
    public Sprite espressoCup;
    public Sprite latteCup;
    public Sprite cappacinoCup;

    //hot chocolates
    public Sprite hotchoco;
    public Sprite hotchocoCream;
    public Sprite hotchocoCreamMarshmellows;
    public Sprite hotchocoCreamSprinkles;

    //milkJugs
    public Sprite emptyJug;
    public Sprite milkyJug;
    public Sprite chocoJug;

    //toppings
    public Sprite chocoPowder;

    //accesses the gameStateManager
    private GameStateManager gsm;

    //variable to identify the item slot
    public string ItemSlotID;

    //used from https://www.w3schools.com/cs/cs_enums.php
    //enum to track the state of the cup
    public enum CupState { Empty, Espresso, Latte, Cappacino, HotChoco, ChocoCream, ChocoMarshmellow, ChocoSprinkles }
    public enum MilkJugState {NoMilk, Milky, Choco}

    public CupState cupstate = CupState.Empty;
    public MilkJugState milkJugState = MilkJugState.NoMilk;

    void Start()
    {
        gsm = GameObject.Find("GameState").GetComponent<GameStateManager>();
    }


    public void OnDrop(PointerEventData eventData)
    {
    
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        DraggableImage item = dropped.GetComponent<DraggableImage>();

        if (item == null) return;

        Debug.Log("An Item has been dropped");

        //getting the itemslot from the game object dropped
        ItemSlot droppedSlot = dropped.GetComponent<ItemSlot>();

        //snaps the dropped items to the item slot
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        //checking if the item slot being used is the coffee machine
        if (ItemSlotID == "CoffeeMachine")
        {
            Debug.Log("Item has been dropped on the coffee machine");

            //checking if a cup has been placed onto the coffee machine
            if (dropped.CompareTag("Empty")) //.Compare Tag is used from https://community.gamedev.tv/t/the-difference-between-other-gameobject-tag-and-other-gameobject-comparetag/214190
            {
                Debug.Log("The item on the coffee machine is a cup");

                //checking if the cup is empty
                if (droppedSlot.cupstate == CupState.Empty)
                {

                    Debug.Log("The cup on the coffee machine is empty and is changing to an espresso");
                    //checking cupSlot isnt null
                    if (droppedSlot != null)
                    {
                        rectTransformTarget.anchoredPosition = new Vector2(x, y);  //-330 10
                        //changing the cup item slot's enum to update the cup state
                        droppedSlot.cup.sprite = droppedSlot.espressoCup; //changing the empty cup to be an espresso
                        droppedSlot.cupstate = CupState.Espresso; //changing the enum to be an espresso
                        Debug.Log("cups state is " + droppedSlot.cupstate);
                    }
                    return;
                }
                else
                {
                    //the cup is not empty
                    Debug.Log("Cup is not empty");
                }
            }
            //checking if the item dropped is the milk jug
            else if (dropped.CompareTag("MilkJug"))
            {
                Debug.Log("a milk jug has been placed onto the coffee machine");
                if (droppedSlot.milkJugState == MilkJugState.NoMilk)
                {
                    rectTransformTargetMILK.anchoredPosition = new Vector2(-250, 15);
                    droppedSlot.jug.sprite = droppedSlot.milkyJug;
                    droppedSlot.milkJugState = MilkJugState.Milky;
                    Debug.Log("Jug is now filled with milk" + droppedSlot.milkJugState);
                }


            }
        }
        else if (ItemSlotID == "MilkJug")
        {
            Debug.Log("hola " + milkJugState);
            if (dropped.CompareTag("CocoaPowder"))
            {
                Debug.Log("hi");
                if (milkJugState == MilkJugState.Milky)
                {
                    jug.sprite = chocoJug;
                    milkJugState = MilkJugState.Choco;
                    Debug.Log("choco milk");
                }
                else if (milkJugState == MilkJugState.Choco)
                {
                    Debug.Log("Milk is already chocolaty");
                }
                else if (milkJugState == MilkJugState.NoMilk)
                {
                    Debug.Log("Jug needs to have milk in it first");
                }
            }
        }
        //checking if the item slot being used is the cup
        else if (ItemSlotID == "Cup")
        {
            Debug.Log("Cup has been used as a item slot");

            //checking if the milk jug has been dropped onto the cup
            if (dropped.CompareTag("MilkJug"))
            {
                Debug.Log("Milk jug has been placed ontop of the cup" + droppedSlot.milkJugState);

                //checking if the state of the cup's item slot's enum is an espresso
                if (cupstate == CupState.Espresso && droppedSlot.milkJugState == MilkJugState.Milky)
                {
                    //changes the espresso to be a latte
                    cup.sprite = latteCup;
                    //updates the enum to be a latte
                    cupstate = CupState.Latte;
                    jug.sprite = emptyJug;
                    milkJugState = MilkJugState.NoMilk;
                    droppedSlot.milkJugState = MilkJugState.NoMilk;
                    Debug.Log(milkJugState + " " + droppedSlot.milkJugState);
                    Debug.Log("Cup is changing to a latte");
                }
                else if (cupstate == CupState.Empty && droppedSlot.milkJugState == MilkJugState.Choco)
                {
                    cup.sprite = hotchoco;
                    cupstate = CupState.HotChoco;
                    jug.sprite = emptyJug;
                    milkJugState = MilkJugState.NoMilk;
                    droppedSlot.milkJugState = MilkJugState.NoMilk;
                    Debug.Log("hot chocolate");
                }
                else
                {
                    //the cup is not an espresso
                    Debug.Log("Espresso is not present or milk jug doesnt contain milk");
                }

            }
            else if (dropped.CompareTag("CocoaPowder"))
            {
                if (cupstate == CupState.Latte)
                {
                    cup.sprite = cappacinoCup;
                    cupstate = CupState.Cappacino;
                    Debug.Log("Cup is now a cappacino");
                }
            }
            else if (dropped.CompareTag("WhippedCream"))
            {
                if (cupstate == CupState.HotChoco)
                {
                    cup.sprite = hotchocoCream;
                    cupstate = CupState.ChocoCream;
                    Debug.Log("Hot chocolate and cream");
                }
            }
            else if (dropped.CompareTag("Marshmellows"))
            {
                if (cupstate == CupState.ChocoCream)
                {
                    cup.sprite = hotchocoCreamMarshmellows;
                    cupstate = CupState.ChocoMarshmellow;
                    Debug.Log("Hot chocolate and marshmellows");
                }
                else
                {
                    Debug.Log("Cup needs to be cream and marshmellow before putting marshmellows on it");
                }
            }
            else if (dropped.CompareTag("Sprinkles"))
            {
                if (cupstate == CupState.ChocoCream)
                {
                    cup.sprite = hotchocoCreamSprinkles;
                    cupstate = CupState.ChocoSprinkles;
                    Debug.Log("Hot chocolate and sprinkles");
                }
                else
                {
                    Debug.Log("Cup needs to be cream and sprinkles before putting marshmellows on it");
                }
            }
        }

        //checking if the item slot being used is the sink
        else if (ItemSlotID == "Sink")
        {
            Debug.Log("An item has been dropped onto the sink");

            //checking to see if the item being dropped was a cup
            if (dropped.CompareTag("Empty"))
            {
                //checking if the cup is an espresso or a latte 
                if (droppedSlot.cupstate == CupState.Espresso || droppedSlot.cupstate == CupState.Latte || droppedSlot.cupstate == CupState.Cappacino || droppedSlot.cupstate == CupState.HotChoco || droppedSlot.cupstate == CupState.ChocoCream || droppedSlot.cupstate == CupState.ChocoMarshmellow || droppedSlot.cupstate == CupState.ChocoSprinkles)
                {
                    droppedSlot.cup.sprite = droppedSlot.emptyCup; //changing the cup to look empty
                    droppedSlot.cupstate = CupState.Empty; //changing the cupstate enum to be back to empty

                    Debug.Log("Cup is back to being empty.");
                }
                else
                {
                    Debug.Log("the cup is not an espresso");
                }
            }
            else if (dropped.CompareTag("MilkJug"))
            {
                if (droppedSlot.milkJugState == MilkJugState.Choco || droppedSlot.milkJugState == MilkJugState.Milky)
                {
                    droppedSlot.jug.sprite = droppedSlot.emptyJug;
                    droppedSlot.milkJugState = MilkJugState.NoMilk;
                    Debug.Log("milk jug is now empty");
                }
            }
            else
            {
                Debug.Log("This item cannot go into the sink");
            }
        }

        //checking if the item slot being used is the table
        else if (ItemSlotID == "Table")
        {
            Debug.Log("item has been dropped onto the table");

            //checking if the cup has been placed on the table
            if (dropped.CompareTag("Empty"))
            {
                //changing to a latte code is within cup slot 
                Debug.Log("Cup has been placed onto the table");
            }
            else
            {
                Debug.Log("Item cannot be poured down the sink");
            }
        }else if(ItemSlotID == "Saucer")
        {
            if (dropped.CompareTag("Empty"))
            {
                Debug.Log("Cup has been placed onto the saucer");
                rectTransformTarget3.anchoredPosition = new Vector2(-75, -11);
            }
        }
    }
 }



