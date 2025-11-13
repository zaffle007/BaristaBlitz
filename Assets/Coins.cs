using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Coins : MonoBehaviour, IPointerClickHandler
{
    public GameObject[] coinsCollected;
    private GameStateManager gsm;

    public int coinIndex;

    void Start()
    {
        gsm= GameObject.Find("GameState").GetComponent<GameStateManager>();   
    }
    public void OnPointerClick(PointerEventData data)
    {
        coinsCollected[coinIndex].SetActive(false);
        Debug.Log("coins have been collected + " + coinIndex);

        gsm.adjustScore(4);

        GameStateManager.Instance.customerArrivedPositions[coinIndex] = false;

        if (GameStateManager.Instance.lastCustomerArrived)
        {
            Debug.Log("COIN CODE");
            GameStateManager.Instance.lastCustomerServed = true;

            
            gsm.lastCustomer(GameStateManager.Instance.lastCustomerArrived, GameStateManager.Instance.lastCustomerServed);
        }
    }
}
