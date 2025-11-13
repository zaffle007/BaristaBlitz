using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework.Constraints;
using System.Threading;



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
                Debug.Log("LAST CUSTOMER");
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
        newCustomer = Customers[customerIndex];

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

            for (int i = 0; i < spawnedCustomers.Length; i++)
            {
                Debug.Log("ZOEOOEOEOE + " + spawnedCustomers[i] + positionXIndex);
            }
        }

        
/*
        //checks what position has been chosen and makes sure a customer isnt already in that position
        if (positionXIndex == 0)
        {
            if (customerArrivedPosiiton0)
            {
                //Debug.Log("Customer is already there");
            }
            else
            {
                //spawns in a new customer
                Instantiate(newCustomer, new Vector2(randomPositionX, positionY), Quaternion.identity);
                customersSpawned++; //adds 1 to the counter to track the number of customers
                customerArrivedPosiiton0 = true; //ensures no other customers can spawn in that position
                //orderSlot1.SetActive(true); //makes their order slot visible
                orderSlots[positionXIndex].SetActive(true);
                PositionDrinks = drinkPositions[positionXIndex]; //access the slots for the drink and food order to go
                PositionFood = foodPositions[positionXIndex];
                Order(); //activates the order fuction
            }
        }
        if (positionXIndex == 1)
        {
            if (customerArrivedPosiiton1)
            {
                Debug.Log("Customer is already there");
            }
            else
            {
                //spawns in a new customer
                Instantiate(newCustomer, new Vector2(randomPositionX, positionY), Quaternion.identity);
                customersSpawned++; //adds 1 to the counter to track the number of customers
                customerArrivedPosiiton1 = true; //ensures no other customers can spawn in that position
                orderSlots[positionXIndex].SetActive(true); //makes their order slot visible
                PositionDrinks = drinkPositions[positionXIndex]; //access the slots for the drink and food order to go
                PositionFood = foodPositions[positionXIndex];
                Order(); //activates the order fuction
            }
        }
        if (positionXIndex == 2)
        {
            if (customerArrivedPosiiton2)
            {
                //Debug.Log("Customer is already there");
            }
            else
            {
                //spawns in a new customer
                Instantiate(newCustomer, new Vector2(randomPositionX, positionY), Quaternion.identity);
                customersSpawned++; //adds 1 to the counter to track the number of customers
                customerArrivedPosiiton2 = true; //ensures no other customers can spawn in that position
                orderSlots[positionXIndex].SetActive(true); //makes their order slot visible
                PositionDrinks = drinkPositions[positionXIndex]; //access the slots for the drink and food order to go
                PositionFood = foodPositions[positionXIndex];
                Order(); //activates the order fuction
            }
        }
        if (positionXIndex == 3)
        {
            if (customerArrivedPosiiton3)
            {
                //Debug.Log("Customer is already there");
            }
            else
            {
                //spawns in a new customer
                Instantiate(newCustomer, new Vector2(randomPositionX, positionY), Quaternion.identity);
                customersSpawned++; //adds 1 to the counter to track the number of customers
                customerArrivedPosiiton3 = true; //ensures no other customers can spawn in that position
                orderSlots[positionXIndex].SetActive(true); //makes their order slot visible
                PositionDrinks = drinkPositions[positionXIndex]; //access the slots for the drink and food order to go
                PositionFood = foodPositions[positionXIndex];
                Order(); //activates the order fuction
            }
        }*/
        
    }


    Image Order()
    {
        //checks what the level difficulty is
        if (GameStateManager.level == 0) //just coffees and simple sandwiches (only picks the first 3 option of the drink and food arrray)
        {
            int MyIndexDrinks = Random.Range(0, Mathf.Min(Drinks.Length, 3)); //https://discussions.unity.com/t/picking-a-random-object-from-an-array/398804
            MyIndexFood = Random.Range(0, Mathf.Min(Food.Length, 3));
            //changes the empty box to the item ordered
            PositionDrinks.sprite = ImagesDrinks[MyIndexDrinks];
            PositionFood.sprite = ImagesFood[MyIndexFood];
            orderDrinkChoice = ImagesDrinks[MyIndexDrinks].name;
            orderFoodChoice = ImagesFood[MyIndexFood].name;
            GameStateManager.Instance.customerDrinkOrders[positionXIndex] = orderDrinkChoice; //updates the order array in the GameStateManager to what has been ordered in that position
            GameStateManager.Instance.customerFoodOrders[positionXIndex] = orderFoodChoice; //updates the order array in the GameStateManager to what has been ordered in that position

        }
        else if (GameStateManager.level == 1)//all drinks and sandwiches
        {
            int MyIndexDrinks = Random.Range(0, Drinks.Length); //https://discussions.unity.com/t/picking-a-random-object-from-an-array/398804
            int MyIndexFood = Random.Range(0, Food.Length);
            //changes the empty box to the item ordered
            PositionDrinks.sprite = ImagesDrinks[MyIndexDrinks];
            PositionFood.sprite = ImagesFood[MyIndexFood];
            orderDrinkChoice = ImagesDrinks[MyIndexDrinks].name;
            orderFoodChoice = ImagesFood[MyIndexFood].name;
            GameStateManager.Instance.customerDrinkOrders[positionXIndex] = orderDrinkChoice; //updates the order array in the GameStateManager to what has been ordered in that position
            GameStateManager.Instance.customerFoodOrders[positionXIndex] = orderFoodChoice; //updates the order array in the GameStateManager to what has been ordered in that position




        }
        else if (GameStateManager.level == 2)//multiple orders of drinks and food
        {
            int MyIndexDrinks = Random.Range(0, 2); //https://discussions.unity.com/t/picking-a-random-object-from-an-array/398804
            int MyIndexFood = Random.Range(0, 2);
            //changes the empty box to the item ordered
            PositionDrinks.sprite = ImagesDrinks[MyIndexDrinks];
            PositionFood.sprite = ImagesFood[MyIndexFood];
            orderDrinkChoice = ImagesDrinks[MyIndexDrinks].name;
            orderFoodChoice = ImagesFood[MyIndexFood].name;
            GameStateManager.Instance.customerDrinkOrders[positionXIndex] = orderDrinkChoice; //updates the order array in the GameStateManager to what has been ordered in that position
            GameStateManager.Instance.customerFoodOrders[positionXIndex] = orderFoodChoice; //updates the order array in the GameStateManager to what has been ordered in that position

        }
        return PositionFood;
        
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

            if (sandwichToBeGiven == GameStateManager.Instance.customerFoodOrders[0])
            {
                Debug.Log("Correct sandwich dropped: " + sandwichToBeGiven);
                dropped.SetActive(false); //makes the dropped item invisible
                //bool orderFinshed = true;
                foodTicks[0].SetActive(true);
                GameStateManager.Instance.customerFoodOrders[0] = null;

            }
            else
            {
                Debug.Log("Incorrect sandwich! You gave a " + sandwichToBeGiven + " They would like a " + GameStateManager.Instance.customerFoodOrders[0]);

            }
            if (drinkToBeGiven == GameStateManager.Instance.customerDrinkOrders[0])
            {
                Debug.Log("Correct drink dropped: " + drinkToBeGiven);
                dropped.SetActive(false); //makes the dropped item invisible
                //bool orderFinshed = true;
                drinkTicks[0].SetActive(true);
                GameStateManager.Instance.customerFoodOrders[0] = null;
            }
            else
            {
                Debug.Log("Incorrect drink! You gave a " + drinkToBeGiven + " They would like a " + GameStateManager.Instance.customerDrinkOrders[0]);
            }
            int coinsX = 0;
            StartCoroutine(OrderCompleted(coinsX));

        }

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        else if (orderLocationID == "OrderSlot2" || orderLocationID == "position2Drink" || orderLocationID == "position2Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerFoodOrders[1])
            {
                Debug.Log("Correct sandwich dropped: " + sandwichToBeGiven);
                dropped.SetActive(false);//makes the dropped item invisible
                //Position1Food.sprite = originalOrderSlot2;
                //Destroy(gameObject);
                //bool orderFinshed = true;
                foodTicks[1].SetActive(true);
                GameStateManager.Instance.customerFoodOrders[1] = null;

            }
            else
            {
                Debug.Log("Incorrect sandwich! You gave a " + sandwichToBeGiven + " They would like a " + GameStateManager.Instance.customerFoodOrders[1]);
            }

            if (drinkToBeGiven == GameStateManager.Instance.customerDrinkOrders[1])
            {
                Debug.Log("Correct drink dropped: " + drinkToBeGiven);
                dropped.SetActive(false); //makes the dropped item invisible
                //bool orderFinshed = true;
                drinkTicks[1].SetActive(true);
                //OrderCompleted();
                GameStateManager.Instance.customerFoodOrders[1] = null;
            }
            else
            {
                Debug.Log("Incorrect drink! You gave a " + drinkToBeGiven + " They would like a " + GameStateManager.Instance.customerDrinkOrders[1]);
            }
            int coinsX = 1;
            StartCoroutine(OrderCompleted(coinsX));

        }

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        else if (orderLocationID == "OrderSlot3" || orderLocationID == "position3Drink" || orderLocationID == "position3Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerFoodOrders[2])
            {
                Debug.Log("Correct sandwich dropped: " + sandwichToBeGiven);
                dropped.SetActive(false);//makes the dropped item invisible

                //bool orderFinshed = true;
                foodTicks[2].SetActive(true);
                GameStateManager.Instance.customerFoodOrders[2] = null;

            }
            else
            {
                Debug.Log("Incorrect sandwich! You gave a " + sandwichToBeGiven + " They would like a " + GameStateManager.Instance.customerFoodOrders[2]);
            }

            if (drinkToBeGiven == GameStateManager.Instance.customerDrinkOrders[2])
            {
                Debug.Log("Correct drink dropped: " + drinkToBeGiven);
                dropped.SetActive(false); //makes the dropped item invisible
                //bool orderFinshed = true;
                drinkTicks[2].SetActive(true);
                GameStateManager.Instance.customerFoodOrders[2] = null;
            }
            else
            {
                Debug.Log("Incorrect drink! You gave a " + drinkToBeGiven + " They would like a " + GameStateManager.Instance.customerDrinkOrders[2]);
            }
            int coinsX = 2;
            StartCoroutine(OrderCompleted(coinsX));
        }

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        else if (orderLocationID == "OrderSlot4" || orderLocationID == "positsion4Drink" || orderLocationID == "position4Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerFoodOrders[3])
            {
                Debug.Log("Correct sandwich dropped: " + sandwichToBeGiven);
                dropped.SetActive(false);//makes the dropped item invisible

                //bool orderFinshed = true;
                foodTicks[3].SetActive(true);
                GameStateManager.Instance.customerFoodOrders[3] = null;


            }
            else
            {
                Debug.Log("Incorrect sandwich! You gave a " + sandwichToBeGiven + " They would like a " + GameStateManager.Instance.customerFoodOrders[3]);
            }

            if (drinkToBeGiven == GameStateManager.Instance.customerDrinkOrders[3])
            {
                Debug.Log("Correct drink dropped: " + drinkToBeGiven);
                dropped.SetActive(false); //makes the dropped item invisible
                //bool orderFinshed = true;
                drinkTicks[3].SetActive(true);
                GameStateManager.Instance.customerFoodOrders[3] = null;
            }
            else
            {
                Debug.Log("Incorrect drink! You gave a " + drinkToBeGiven + " They would like a " + GameStateManager.Instance.customerDrinkOrders[3]);
            }
            int coinsX = 3;
            StartCoroutine(OrderCompleted(coinsX));
            
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


        
    
