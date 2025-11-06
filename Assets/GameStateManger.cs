
using UnityEngine;
using UnityEngine.UI;


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public bool iscafeOpen;
    public bool maxCustomersReached;

    public string expectedSandwich;

    public string[] customerOrders;

    public Image emptySandwich;
    public Sprite noFillingSandwich;

    public bool orderFinshed;

     public Image originalOrderSlot1;
            public Image originalOrderSlot2;
            public Image originalOrderSlot3;
            public Image originalOrderSlot4;


    //used from https://www.w3schools.com/cs/cs_enums.php
    //enum to track the state of the cup
    public enum SandwichState { Nothing, HamSandwich_0, HamLettuceSandwich_0, CheeseSandwich_0, CheeseTomatoSandwich_0, CheeseHamSandwich_0, CheeseHamLettuceSandwich_0, CheeseLettuceSandwich_0, LettuceSandwich_0, TomatoSandwich_0 }

    public SandwichState sandwichState = SandwichState.Nothing;

    
    public GameStateManager()
    {
        iscafeOpen = false;
        maxCustomersReached = false;
        expectedSandwich = "none";
        customerOrders = new string[] { "none", "none", "none", "none" };
        orderFinshed = false;
    }

    static GameStateManager()
    {
        Instance = new GameStateManager();
    }
/*
    public void ResetSandwich()
{
    emptySandwich.sprite = noFillingSandwich;
    sandwichState = SandwichState.Nothing;
}
*/
}