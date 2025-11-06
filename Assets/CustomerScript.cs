using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;



public class CustomerScript : MonoBehaviour, IDropHandler
{
    //checks what difficulty the game is
    public string difficultyLevel;

    //gets the order slots and the food/drink order places
    public GameObject orderSlot1;
    public GameObject orderSlot2;
    public GameObject orderSlot3;
    public GameObject orderSlot4;

    public Image PositionDrinks;
    public Image PositionFood;

    public Image Position1Drinks;
    public Image Position1Food;

    public Image Position2Drinks;
    public Image Position2Food;

    public Image Position3Drinks;
    public Image Position3Food;

    public Image Position4Drinks;
    public RectTransform Position4Food;

    [SerializeField] GameObject completedOrderIcon;


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

    //bools to check if a customer is in one of the locations as only 1 customer can be in one slot at one time
    private bool customerArrivedPosiiton0 = false;
    private bool customerArrivedPosiiton1 = false;
    private bool customerArrivedPosiiton2 = false;
    private bool customerArrivedPosiiton3 = false;

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
    private int maxCustomersSpawned = 4;

    //counter to keep track of how many customers have been spawned in
    private int customersSpawned = 0;

    //checks which order is in what position
    public string orderLocationID;

    //stores the order choice
    string orderChoice = "none";

    


    public int MyIndexFood;

    //the positions for the order
    public Image[] foodPositions;
    public Image[] drinkPositions;

    public RectTransform rectTransformTarget;

    public static float xCoordinate;
    public static float yCoordinate;

    [SerializeField] public static Transform canvasParent; // Reference to the Canvas or a child panel

    [SerializeField] public GameObject [] tickLocation;

    public static bool foodCompleted;
    


    void Update()
    {
        //checks if the cafe is open, if it is then starts to spawn customers
        if (GameStateManager.Instance.iscafeOpen)
        {
            //makes sure the customers hasnt exceeded the maximum number
            if (customersSpawned < maxCustomersSpawned)
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
                orderSlot1.SetActive(true); //makes their order slot visible
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
                orderSlot2.SetActive(true); //makes their order slot visible
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
                orderSlot3.SetActive(true); //makes their order slot visible
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
                orderSlot4.SetActive(true); //makes their order slot visible
                PositionDrinks = drinkPositions[positionXIndex]; //access the slots for the drink and food order to go
                PositionFood = foodPositions[positionXIndex];
                Order(); //activates the order fuction
            }
        }

    }


    Image Order()
    {
        //checks what the level difficulty is
        if (difficultyLevel == "Easy") //just coffees and simple sandwiches (only picks the first 3 option of the drink and food arrray)
        {
            int MyIndexDrinks = Random.Range(0, Mathf.Min(Drinks.Length, 3)); //https://discussions.unity.com/t/picking-a-random-object-from-an-array/398804
            MyIndexFood = Random.Range(0, Mathf.Min(Food.Length, 3));
            //changes the empty box to the item ordered
            PositionDrinks.sprite = ImagesDrinks[MyIndexDrinks];
            PositionFood.sprite = ImagesFood[MyIndexFood];
            orderChoice = ImagesFood[MyIndexFood].name;
            GameStateManager.Instance.customerOrders[positionXIndex] = orderChoice; //updates the order array in the GameStateManager to what has been ordered in that position
            
        }
        else if (difficultyLevel == "Medium")//all drinks and sandwiches
        {
            int MyIndexDrinks = Random.Range(0, Drinks.Length); //https://discussions.unity.com/t/picking-a-random-object-from-an-array/398804
            int MyIndexFood = Random.Range(0, Food.Length);
            //changes the empty box to the item ordered
            PositionDrinks.sprite = ImagesDrinks[MyIndexDrinks];
            PositionFood.sprite = ImagesFood[MyIndexFood];
            orderChoice = ImagesFood[MyIndexFood].name;
            GameStateManager.Instance.customerOrders[positionXIndex] = orderChoice; //updates the order array in the GameStateManager to what has been ordered in that position
            
            


        }
        else if (difficultyLevel == "Hard")//multiple orders of drinks and food
        {
            int MyIndexDrinks = Random.Range(0, 2); //https://discussions.unity.com/t/picking-a-random-object-from-an-array/398804
            int MyIndexFood = Random.Range(0, 2);
            //changes the empty box to the item ordered
            PositionDrinks.sprite = ImagesDrinks[MyIndexDrinks];
            PositionFood.sprite = ImagesFood[MyIndexFood];
            orderChoice = ImagesFood[MyIndexFood].name;
            GameStateManager.Instance.customerOrders[positionXIndex] = orderChoice; //updates the order array in the GameStateManager to what has been ordered in that position
            
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


        Vector2 dropPosition = eventData.position;

        Debug.Log("Item dropped at Screen X: " + dropPosition.x + ", Screen Y: " + dropPosition.y);

        // You can access X and Y individually:
        xCoordinate = dropPosition.x;
        yCoordinate = dropPosition.y;


        //checks where the item has been dropped and checks the item dropped matches the ordered item
        if (orderLocationID == "OrderSlot1" || orderLocationID == "position1Drink" || orderLocationID == "position1Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerOrders[0])
            {
                Debug.Log("Correct sandwich dropped: " + sandwichToBeGiven);
                dropped.SetActive(false); //makes the dropped item invisible
                bool orderFinshed = true;
                OrderCompleted(orderFinshed);
                GameStateManager.Instance.customerOrders[0] = null;
            }
            else
            {
                Debug.Log("Incorrect sandwich! You gave a " + sandwichToBeGiven + " They would like a " + GameStateManager.Instance.customerOrders[0]);
            }
        }

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        else if (orderLocationID == "OrderSlot2" || orderLocationID == "position2Drink" || orderLocationID == "position2Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerOrders[1])
            {
                Debug.Log("Correct sandwich dropped: " + sandwichToBeGiven);
                dropped.SetActive(false);//makes the dropped item invisible
                //Position1Food.sprite = originalOrderSlot2;
                //Destroy(gameObject);
                bool orderFinshed = true;
                OrderCompleted(orderFinshed);
                GameStateManager.Instance.customerOrders[1] = null;
            }
            else
            {
                Debug.Log("Incorrect sandwich! You gave a " + sandwichToBeGiven + " They would like a " + GameStateManager.Instance.customerOrders[1]);
            }
        }

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        else if (orderLocationID == "OrderSlot3" || orderLocationID == "position3Drink" || orderLocationID == "position3Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerOrders[2])
            {
                Debug.Log("Correct sandwich dropped: " + sandwichToBeGiven);
                dropped.SetActive(false);//makes the dropped item invisible

                bool orderFinshed = true;
                OrderCompleted(orderFinshed);
                GameStateManager.Instance.customerOrders[2] = null;
            }
            else
            {
                Debug.Log("Incorrect sandwich! You gave a " + sandwichToBeGiven + " They would like a " + GameStateManager.Instance.customerOrders[2]);
            }
        }

        //checks where the item has been dropped and checks the item dropped matches the ordered item
        else if (orderLocationID == "OrderSlot4" || orderLocationID == "positsion4Drink" || orderLocationID == "position4Food")
        {

            if (sandwichToBeGiven == GameStateManager.Instance.customerOrders[3])
            {
                Debug.Log("Correct sandwich dropped: " + sandwichToBeGiven);
                dropped.SetActive(false);//makes the dropped item invisible

                bool orderFinshed = true;
                foodCompleted = true;
                StartCoroutine(Border(orderFinshed));
                GameStateManager.Instance.customerOrders[3] = null;

            }
            else
            {
                Debug.Log("Incorrect sandwich! You gave a " + sandwichToBeGiven + " They would like a " + GameStateManager.Instance.customerOrders[3]);
            }
        }
    }

    void OrderCompleted(bool orderFinshed)
    {
        Debug.Log("order completed + " + orderFinshed);

        if (orderFinshed)
        {
            //Destroy(gameObject);
            completedOrderIcon.SetActive(true);
        }
    }
    
    //https://discussions.unity.com/t/make-an-object-sprite-appear-and-disappear/771739/5
    IEnumerator Border(bool orderFinished)
    {
        if (orderFinished)
        {
            if(foodCompleted == true)
            {
                Debug.Log("food completed ");
                Debug.Log("x position + " + positionXIndex);
                Debug.Log("hi + " + tickLocation[3].name);
                tickLocation[3].SetActive(true);
                yield return new WaitForSeconds(20f);
            }
            /*
            Debug.Log("x position + " + randomPositionX + positionY);
            
            //completedOrderIcon.SetActive(true);


            Vector3 currentPosition = Position4Food.transform.position;
            float xPosition = transform.position.x;
            float yPosition = transform.position.y;
            Debug.Log("POSITION 4 FOOD POSITION: " + xCoordinate + " " + yCoordinate);
            Instantiate(completedOrderIcon, new Vector2(0, 0), Quaternion.identity);
            completedOrderIcon.transform.SetParent(canvasParent, false);

            yield return new WaitForSeconds(20f);
            //completedOrderIcon.SetActive(false);*/
        }
    }
}