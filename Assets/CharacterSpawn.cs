using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class CharacterSpawn : MonoBehaviour
{

    //public GameObject newCustomer;

    public GameObject[] Customers;
    public int customerIndex;
    public GameObject newCustomer;

    public RectTransform orderDisplay;

    private bool customerArrived = false;

    public int positionY;

    private int positionXIndex;
    private int randomPositionX;

    int[] positionX = {5, 7, -3, 2 };

void Update()
{
Debug.Log("customer spawn code" + GameStateManager.Instance.iscafeOpen);
    if (GameStateManager.Instance.iscafeOpen)
        StartCoroutine(SpawnCustomersRoutine());
}

IEnumerator SpawnCustomersRoutine()
{
    while (GameStateManager.Instance.iscafeOpen)
        {
            if (customerArrived == false)
            {
                SpawnNewCustomer();
                customerArrived = true;
            }
        Debug.Log("THE CAFE IS OPEN");
        yield return new WaitForSeconds(5f); // Wait 5 seconds before next spawn
    }
}
    void SpawnNewCustomer()
    {
        Debug.Log("hello");
        Debug.Log(GameStateManager.Instance.iscafeOpen);
            customerIndex = Random.Range(0, Customers.Length);
            newCustomer = Customers[customerIndex];
            Debug.Log(newCustomer);
            positionXIndex = Random.Range(0, positionX.Length);
            randomPositionX = positionX[positionXIndex];
            Debug.Log(randomPositionX);

            for (var i = 0; i < 1; i++)
            {
                Instantiate(newCustomer, new Vector2(randomPositionX, positionY), Quaternion.identity);
                Debug.Log("character has been placed ");

            }
        }
    }


    //while cafe open characters spawning
