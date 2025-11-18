using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDisplay : MonoBehaviour
{

    public TextMeshProUGUI scoreDisplay;

    public TextMeshProUGUI customerCount;

    public TextMeshProUGUI levelNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("HIIIIIIIIIIIIIIIIIIIII" + GameStateManager.level + " " + GameStateManager.numCustomerServed);
        levelNumber.text = " " + GameStateManager.level;
        customerCount.text = " " + GameStateManager.numCustomerServed + " / " + GameStateManager.Instance.maxCustomersSpawned;
        scoreDisplay.text = " " + GameStateManager.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
