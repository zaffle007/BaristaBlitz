using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework.Constraints;
using System.Threading;
using System;
using Random = UnityEngine.Random;



public class CustomerScript : MonoBehaviour, IDropHandler
{

    private GameStateManager gsm;
    //checks what difficulty the game is
    

    //gets the order slots and the food/drink order places
    public GameObject orderSlot1;
    public GameObject orderSlot2;
    public GameObject orderSlot3;
    public GameObject orderSlot4;

    public GameObject[] orderSlots;

    public Image PositionDrinks;
    public Image PositionFood;

    public Image Position1Drinks;
    public Image Position1Food;

    public Image Position2Drinks;
    public Image Position2Food;

    public Image Position3Drinks;
    public Image Position3Food;

    public Image Position4Drinks;
    public Image Position4Food;

    [SerializeField] GameObject tickPrefab;


    //arrays the hold the different order options, when the orders are chosen they are randomised from the arrays

    //https://www.bytehide.com/blog/random-elements-csharp#:~:text=got%20you%20covered.-,rray%20Randomization,Next(animals.
    string[] Drinks = { "Espresso", "Latte", "Cappacino", "Hot Chocolate & Cream", "Hot Chocolate & Cream & Marshmellows", "Hot Chocolate & Cream & Sprinkles" };
    string[] Food = { "HamSandwich", "CheeseHamSandwich", "CheeseSandwich", "CheeseTomatoSandwich", "HamLettuceSandwich", "CheeseHamLettuceSandwich", "CheeseLettuceSandwich" };

    //lists are used to store the sprites of the drinks and food
    //https://discussions.unity.com/t/making-a-list-of-images/220917
    public Sprite[] ImagesDrinks;
    public Sprite[] ImagesFood;

    //list to store the different customers
    public GameObject[] Customers;

    //GameObject variable to store the spawned in customer
    private GameObject newCustomer;


    //sets the y position from the inspector where the characters are spawned at
    public int positionY;

    //array to store the 4 x positions for the customers
    int[] positionX = { -3, 1, 4, 7 };

    //variable stores what position the character has been placed at
    public static int positionXIndex;

    //stores what x value the customer has been spawned at
    private static int randomPositionX;



    //these two variables allow customers to be spawned in at random times
    //https://discussions.unity.com/t/instantiating-a-object-at-a-set-location-x-amount-of-times/890744/3

    //the time range is dictated in the inspector
    public Vector2 randomSpawnTimeRange;
    public float m_remainingSpawnTimer = 0f;

    //the maximum number of customers
    

    //counter to keep track of how many customers have been spawned in
    private int customersSpawned = 0;

    //checks which order is in what position
    public string orderLocationID;

    //stores the order choice
    string orderDrinkChoice = "none";
    string orderFoodChoice = "none";




    public int MyIndexFood;
    public int MyIndexDrinks;

    //the positions for the order
    public Image[] foodPositions;
    public Image[] drinkPositions;

 

    public GameObject foodTick1;
    public GameObject foodTick2;
    public GameObject foodTick3;
    public GameObject foodTick4;

    public GameObject drinkTick1;
    public GameObject drinkTick2;
    public GameObject drinkTick3;
    public GameObject drinkTick4;


    public GameObject[] drinkTicks;
    public GameObject[] foodTicks;

    static GameObject[] spawnedCustomers = {null, null, null, null};


    public GameObject[] coins;

    public CustomerTimer[] timerBars;

    void Start()
    {
        gsm = GameObject.Find("GameState").GetComponent<GameStateManager>();
        Debug.Log("Level = " + GameStateManager.level);

    }

    void Update()
    {
        //checks if the cafe is open, if it is then starts to spawn customers
        if (GameStateManager.Instance.iscafeOpen)
        {
            //makes sure the customers hasnt exceeded the maximum number
            if (customersSpawned < GameStateManager.Instance.maxCustomersSpawned)
            {
                //makes the customers visible on screen 
                //code from https://docs.unity3d.com/560/Documentation/ScriptReference/Renderer-material.html
                foreach (GameObject customer in Customers)
                {
                    Renderer cts = customer.GetComponent<Renderer>();
                    cts.enabled = true;
                }

                //the timer will pick a random number from randomSpawnTimeRange, once the timer is 0 then it will spawn a customer
                //code from https://discussions.unity.com/t/instantiating-a-object-at-a-set-location-x-amount-of-times/890744/3
                m_remainingSpawnTimer -= Time.deltaTime;
                if (m_remainingSpawnTimer <= 0f)
                {
                    SpawnNewCustomer();
                    m_remainingSpawnTimer = Random.Range(randomSpawnTimeRange.x, randomSpawnTimeRange.y);
                }
            }
            else
            {
                //converts the bool to true if the maximum number of customers has been reached
                GameStateManager.Instance.maxCustomersReached = true;
                GameStateManager.Instance.lastCustomerArrived = true;

                if (GameStateManager.Instance.lastCustomerArrived)
                {
                    bool allNull = true;

                        foreach (GameObject customer in spawnedCustomers)
                        {
                            if (customer != null)
                            {
                                allNull = false;
                                break; // stop checking once we find a non-null element
                            }
                        }

                        if (allNull)
                        {
                            Debug.Log("All customers are null.");
                        }
                        else
                        {
                            Debug.Log("At least one customer exists.");
                        }
                }
                return;
            }
        }
        else
        {
            //turns the order slots to invisible when the cafe is closed
            orderSlot1.SetActive(false);
            orderSlot2.SetActive(false);
            orderSlot3.SetActive(false);
            orderSlot4.SetActive(false);

            //makes the customers invisible when the cafe is closed
            foreach (GameObject customer in Customers)
            {
                Renderer cts = customer.GetComponent<Renderer>();
                cts.enabled = false;
            }
        }


    }

    void SpawnNewCustomer()
    {
        //choses a random customer to spawn in from the list Customers 
        int customerIndex = Random.Range(0, Customers.Length);
        Debug.Log(customerIndex);
        try
        {
            newCustomer = Customers[customerIndex];
        }catch (IndexOutOfRangeException e)
        {
            Debug.Log("Error : " + e.Message);
            Debug.Log("Cusotmer Index : " + customerIndex);
        }

        //choses a random place to spawn them into 
        positionXIndex = Random.Range(0, positionX.Length);
        randomPositionX = positionX[positionXIndex];

        if (GameStateManager.Instance.customerArrivedPositions[positionXIndex])
        {
            Debug.Log("customer is already there");
            return;
        }
        else
        {
            try
            {
                //spawns in a new customer
                GameObject spawnedCustomer = Instantiate(newCustomer, new Vector2(randomPositionX, positionY), Quaternion.identity);
            
                customersSpawned++; //adds 1 to the counter to track the number of customers
                GameStateManager.Instance.customerArrivedPositions[positionXIndex] = true; //ensures no other customers can spawn in that position
                                                                //orderSlot1.SetActive(true); //makes their order slot visible
                orderSlots[positionXIndex].SetActive(true);
                PositionDrinks = drinkPositions[positionXIndex]; //access the slots for the drink and food order to go
                PositionFood = foodPositions[positionXIndex];
                Order(); //activates the order fuction


            
                Debug.Log("Customer + " + newCustomer);
                Debug.Log("positoin + " + positionXIndex);
                spawnedCustomers[positionXIndex] = spawnedCustomer;
            
            }
            catch (ArgumentException e)
            {
                Debug.Log("Spawned customer error : " + e.Message);
            }
        }
        
    }


    void Order()
    {
        //checks what level is to increase the difficulty
        if (GameStateManager.level == 0) //just coffees and simple sandwiches (only picks the first 3 option of the drink and food arrray)
        {
            MyIndexDrinks = Random.Range(0, Mathf.Min(Drinks.Length, 3)); //https://discussions.unity.com/t/picking-a-random-object-from-an-array/398804
            MyIndexFood = Random.Range(0, Mathf.Min(Food.Length, 3));
        }else if (GameStateManager.level == 1)
        {
            MyIndexDrinks = Random.Range(0, Drinks.Length); //https://discussions.unity.com/t/picking-a-random-object-from-an-array/398804
            MyIndexFood = Random.Range(0, Food.Length);
        }

        //changes the empty box to the item ordered
        PositionDrinks.sprite = ImagesDrinks[MyIndexDrinks];
        PositionFood.sprite = ImagesFood[MyIndexFood];
        orderDrinkChoice = ImagesDrinks[MyIndexDrinks].name;
        orderFoodChoice = ImagesFood[MyIndexFood].name;
        GameStateManager.Instance.customerDrinkOrders[positionXIndex] = orderDrinkChoice; //updates the order array in the GameStateManager to what has been ordered in that position
        GameStateManager.Instance.customerFoodOrders[positionXIndex] = orderFoodChoice; //updates the order array in the GameStateManager to what has been ordered in that position

    }
        
    


    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        DraggableImage item = dropped.GetComponent<DraggableImage>();

        if (item == null) return;

        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        //variable stores what sandwich has been made by checking the sandwich state 
        string sandwichToBeGiven = GameStateManager.Instance.sandwichState.ToString();
        string drinkToBeGiven = GameStateManager.Instance.cupstate.ToString();

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        if (orderLocationID == "OrderSlot1" || orderLocationID == "position1Drink" || orderLocationID == "position1Food")
        {
            if (sandwichToBeGiven == GameStateManager.Instance.customerFoodOrders[0] || drinkToBeGiven == GameStateManager.Instance.customerDrinkOrders[0])
            {
                Debug.Log("Correct order item dropped: " + dropped.name);
                dropped.SetActive(false); //makes the dropped item invisible

                //checks if a sandwich or drink has been dropped and makes the appropraite tick appear to confirm to the user they have given the correct item
                if(dropped.name == "EmptySandwich"){
                    foodTicks[0].SetActive(true);
                    GameStateManager.Instance.customerFoodOrders[0] = null;
                }else if (dropped.name == "Empty"){
                    drinkTicks[0].SetActive(true);
                    GameStateManager.Instance.customerDrinkOrders[0] = null;
                }
                else
                {
                    Debug.Log("Item cannot be given to the customer");
                }

                int coinsX = 0;
                StartCoroutine(OrderCompleted(coinsX));
            }
            else
            {
                Debug.Log("Incorrect item given");

            }
        }

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        else if (orderLocationID == "OrderSlot2" || orderLocationID == "position2Drink" || orderLocationID == "position2Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerFoodOrders[1] || drinkToBeGiven == GameStateManager.Instance.customerDrinkOrders[1])
            {
                Debug.Log("Correct order item dropped: " + dropped.name);
                dropped.SetActive(false); //makes the dropped item invisible

                //checks if a sandwich or drink has been dropped and makes the appropraite tick appear to confirm to the user they have given the correct item
                if(dropped.name == "EmptySandwich"){
                    foodTicks[1].SetActive(true);
                    GameStateManager.Instance.customerFoodOrders[1] = null;
                }else if (dropped.name == "Empty"){
                    drinkTicks[1].SetActive(true);
                    GameStateManager.Instance.customerDrinkOrders[1] = null;
                }
                else
                {
                    Debug.Log("Item cannot be given to the customer");
                }

                int coinsX = 1;
                StartCoroutine(OrderCompleted(coinsX));
            }
            else
            {
                Debug.Log("Incorrect item given");

            }
        }

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        else if (orderLocationID == "OrderSlot3" || orderLocationID == "position3Drink" || orderLocationID == "position3Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerFoodOrders[2] || drinkToBeGiven == GameStateManager.Instance.customerDrinkOrders[2])
            {
                Debug.Log("Correct order item dropped: " + dropped.name);
                dropped.SetActive(false); //makes the dropped item invisible

                //checks if a sandwich or drink has been dropped and makes the appropraite tick appear to confirm to the user they have given the correct item
                if(dropped.name == "EmptySandwich"){
                    foodTicks[2].SetActive(true);
                    GameStateManager.Instance.customerFoodOrders[2] = null;
                }else if (dropped.name == "Empty"){
                    drinkTicks[2].SetActive(true);
                    GameStateManager.Instance.customerDrinkOrders[2] = null;
                }
                else
                {
                    Debug.Log("Item cannot be given to the customer");
                }

                int coinsX = 2;
                StartCoroutine(OrderCompleted(coinsX));
            }
            else
            {
                Debug.Log("Incorrect item given");

            }
        }

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        else if (orderLocationID == "OrderSlot4" || orderLocationID == "positsion4Drink" || orderLocationID == "position4Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerFoodOrders[3] || drinkToBeGiven == GameStateManager.Instance.customerDrinkOrders[3])
            {
                Debug.Log("Correct order item dropped: " + dropped.name);
                dropped.SetActive(false); //makes the dropped item invisible

                //checks if a sandwich or drink has been dropped and makes the appropraite tick appear to confirm to the user they have given the correct item
                if(dropped.name == "EmptySandwich"){
                    foodTicks[3].SetActive(true);
                    GameStateManager.Instance.customerFoodOrders[3] = null;
                }else if (dropped.name == "Empty"){
                    drinkTicks[3].SetActive(true);
                    GameStateManager.Instance.customerDrinkOrders[3] = null;
                }
                else
                {
                    Debug.Log("Item cannot be given to the customer");
                }

                int coinsX = 3;
                StartCoroutine(OrderCompleted(coinsX));
            }
            else
            {
                Debug.Log("Incorrect item given");

            }
            
        }
    }

    IEnumerator OrderCompleted(int coinsX)
    {
        //code is taken from https://docs.unity3d.com/ScriptReference/GameObject-activeSelf.html
        if(foodTicks[coinsX].activeSelf && drinkTicks[coinsX].activeSelf)
            {
                bool order4completed = true;
            Debug.Log("Order 4 is completed + " + order4completed + positionXIndex);
            gsm.customerServed(1);
            yield return new WaitForSeconds(1);
            Debug.Log("YOOOOOOOOOO + " + coins[coinsX]);
            coins[coinsX].SetActive(true);
            gameObject.SetActive(false);
            foodTicks[coinsX].SetActive(false);
            drinkTicks[coinsX].SetActive(false);
            Debug.Log("CAKKEEEE + " + spawnedCustomers[coinsX]);
            Destroy(spawnedCustomers[coinsX]);
            //Debug.Log("YOOOOOOOOOO" + spawnedCustomers[coinsX]);
            Debug.Log("CUSTOMER WAS DESTROYED AT POSITION + " + coinsX);
            

            }
    

        }
    }


        
    
