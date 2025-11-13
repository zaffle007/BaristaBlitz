using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class GameStateManager : MonoBehaviour
{
    //accessing variables in the GameStateManager
    public static GameStateManager Instance { get; private set; }

    //checking if the cafe is open so the level can begin
    public bool iscafeOpen;

    //if the maximum customers for that level has been reached
    public bool maxCustomersReached;

    //the number of customers to be spawned
    public int maxCustomersSpawned;

    //checks what the sandwich in an order is 
    public string expectedSandwich;

    //array to store what drinks the customers have ordered and in what position
    public string[] customerDrinkOrders;

    //array to store what food the customers have ordered and in what position
    public string[] customerFoodOrders;

    //checks if a customers order is completed
    public bool orderFinshed;

    //the coins collected
    private static int score;

    //tracks how many customers have been served
    public static int numCustomerServed;

    //text to display the coins, customers served and the level
    public TextMeshProUGUI scoreDisplay;

    public TextMeshProUGUI customerCount;

    public TextMeshProUGUI levelNumber;

    public bool[] customerArrivedPositions;

    //used to check if the last customer has spawned in and if they have been served
    public bool lastCustomerArrived;
    public bool lastCustomerServed = false;

    //states if the level has ended
    public static bool endLevel;

    //the level of the game
    public static int level;

    public GameObject juniorBarsta;


    //used from https://www.w3schools.com/cs/cs_enums.php
    //enum to track the state of the cup
    public enum CupState { Empty, espresso_0, latte_0, cappacino_0, HotChoco, cream_0, marshmellowsChoco_0, chocoSprinkles_0 }
    public CupState cupstate = CupState.Empty;

    //checks what state the sandwich is at
    public enum SandwichState { Nothing, HamSandwich_0, HamLettuceSandwich_0, CheeseSandwich_0, CheeseTomatoSandwich_0, CheeseHamSandwich_0, CheeseHamLettuceSandwich_0, CheeseLettuceSandwich_0, LettuceSandwich_0, TomatoSandwich_0 }

    public SandwichState sandwichState = SandwichState.Nothing;

    public bool tomatoUnlocked = false;
    public bool lettuceUnlocked = false;

    public GameObject tomatoPadlock;
    public GameObject lettucePadlock;

    
    //sets values to some of the variables 
    public GameStateManager()
    {
        iscafeOpen = false;
        maxCustomersReached = false;
        expectedSandwich = "none";
        customerDrinkOrders = new string[] { "none", "none", "none", "none" };
        customerFoodOrders = new string[] { "none", "none", "none", "none" };
        orderFinshed = false;
        maxCustomersSpawned = 2;
        customerArrivedPositions = new bool[] { false, false, false, false };
        lastCustomerArrived = false;
        lastCustomerServed = false;
        level = 1;
    }

    static GameStateManager()
    {
        Instance = new GameStateManager();
    }

    public int getScore()
    {
        return score;
    }

    public void setScore(int s)
    {
        score = s;
    }

    //adds to the coins and updates the display during the game
    public void adjustScore(int s)
    {
        score += s;
        Debug.Log("Score is " + score);
        scoreDisplay.text = " " + score;
    }

    //adds how many customers have been served and updates the diaply during the game
    public void customerServed(int served)
    {
        numCustomerServed += served;
        Debug.Log("numbers of customers served " + numCustomerServed);
        customerCount.text = numCustomerServed + "/" + maxCustomersSpawned;
    }

    //checks if the level has been finsihed
    public void lastCustomer(bool lastCustomerArrived, bool lastCustomerServed)
    {
        //checks the last customer has been spawned in
        if (lastCustomerArrived)
        {
            //checks if the all customers have been served
            if (numCustomerServed == maxCustomersSpawned)
            {
                if (lastCustomerServed)
                {
                    StartCoroutine(levelFinished());
                }
            }
            else
            {
                Debug.Log("Last Customer Has Arrived");
            }
        }
    }

    IEnumerator levelFinished()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Level is Completed");
        //levelNumber.text = "" + level; //updates the level display
        level += 1;
        endLevel = true;

    }

    public void UnlockTomatoLettuce()
    {
        if (tomatoUnlocked && lettucePadlock)
        {
            tomatoPadlock.SetActive(true);
            lettucePadlock.SetActive(true);
        }
        else
        {
            tomatoPadlock.SetActive(false);
            lettucePadlock.SetActive(false);
        }
    }
    
    public void upgradeRank()
    {
        if (numCustomerServed >= 2)
        {
            juniorBarsta.SetActive(true);
            Debug.Log("SERVING");
        }
        else
        {
            juniorBarsta.SetActive(false);
            Debug.Log("BOOOOO FAILLLL");
        }
    }
}