using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SandwichCode : MonoBehaviour, IDropHandler
{

    
    public Image emptySandwich;
    public Image bin;
    public RectTransform rectTransformTarget;


    //the sprites being used for the sandwiches
    public Sprite bread;
    public Sprite ham;
    public Sprite cheese;
    public Sprite lettuce;
    public Sprite tomato;

    public Sprite noFillingSandwich;
    public Sprite hamSandwich;
    public Sprite hamLettuceSandwich;
    public Sprite cheeseSandwich;
    public Sprite cheeseLettuceSandwich;
    public Sprite cheeseTomatoSandwich;
    public Sprite cheeseHamSandwich;
    public Sprite cheeseHamLettuceSandwich;
    public Sprite lettuceSandwich;
    public Sprite tomatoSandwich;



    //accesses the gameStateManager
    private GameStateManager gsm;

    //variable to identify the item slot
    public string SandwichItemID;



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

        //SandwichCode sandwichCode = dropped.GetComponent<SandwichCode>();


        //snaps the dropped items to the item slot
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        //Destroy(GameObject.FindWithTag("")); used from //https://discussions.unity.com/t/destroy-gameobject-with-tag/426549

        if (SandwichItemID == "EmptySandwich")
        {
            Debug.Log("Item dropped on sandwich");

            if (dropped.CompareTag("Ham"))
            {
                if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.CheeseSandwich_0)
                {
                    emptySandwich.sprite = cheeseHamSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseHamSandwich_0;
                    Debug.Log("ham and cheese sandwich");
                    GameObject.FindWithTag("Ham").SetActive(false);
                }
                else if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.LettuceSandwich_0)
                {
                    emptySandwich.sprite = hamLettuceSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.HamLettuceSandwich_0;
                    Debug.Log("ham and lettuce sandwich");
                    GameObject.FindWithTag("Ham").SetActive(false);
                }
                else if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.CheeseLettuceSandwich_0)
                {
                    emptySandwich.sprite = cheeseHamLettuceSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseHamLettuceSandwich_0;
                    Debug.Log("cheese, ham and lettuce sandwich");
                    GameObject.FindWithTag("Ham").SetActive(false);
                }
                else
                {
                    emptySandwich.sprite = hamSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.HamSandwich_0;
                    Debug.Log("ham sandwich");
                    GameObject.FindWithTag("Ham").SetActive(false);
                }
            }
            else if (dropped.CompareTag("Cheese"))
            {
                if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.HamSandwich_0)
                {
                    emptySandwich.sprite = cheeseHamSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseHamSandwich_0;
                    Debug.Log("ham and cheese sandwich");
                    GameObject.FindWithTag("Cheese").SetActive(false);
                }
                else if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.TomatoSandwich_0)
                {
                    emptySandwich.sprite = cheeseTomatoSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseTomatoSandwich_0;
                    Debug.Log("cheese and tomato sandwich");
                    GameObject.FindWithTag("Cheese").SetActive(false);
                }
                else if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.LettuceSandwich_0)
                {
                    emptySandwich.sprite = cheeseLettuceSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseLettuceSandwich_0;
                    Debug.Log("cheese and lettuce sandwich");
                    GameObject.FindWithTag("Cheese").SetActive(false);
                }
                else if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.HamLettuceSandwich_0)
                {
                    emptySandwich.sprite = cheeseHamLettuceSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseHamLettuceSandwich_0;
                    Debug.Log("cheese, ham and lettuce sandwich");
                    GameObject.FindWithTag("Cheese").SetActive(false);
                }
                else
                {
                    emptySandwich.sprite = cheeseSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseSandwich_0;
                    Debug.Log("cheese sandwich");
                    GameObject.FindWithTag("Cheese").SetActive(false);
                }
            }
            else if (dropped.CompareTag("Lettuce"))
            {
                if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.HamSandwich_0)
                {
                    emptySandwich.sprite = hamLettuceSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.HamLettuceSandwich_0;
                    Debug.Log("ham and lettuce sandwich");
                    GameObject.FindWithTag("Lettuce").SetActive(false);
                }
                else if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.CheeseSandwich_0)
                {
                    emptySandwich.sprite = cheeseLettuceSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseLettuceSandwich_0;
                    Debug.Log("cheese and lettuce sandwich");
                    GameObject.FindWithTag("Lettuce").SetActive(false);
                }
                else if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.CheeseHamSandwich_0)
                {
                    emptySandwich.sprite = cheeseHamLettuceSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseHamLettuceSandwich_0;
                    Debug.Log("cheese, ham and lettuce sandwich");
                    GameObject.FindWithTag("Lettuce").SetActive(false);
                }
                else
                {
                    emptySandwich.sprite = lettuceSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.LettuceSandwich_0;
                    Debug.Log("lettuce sandwich");
                    GameObject.FindWithTag("Lettuce").SetActive(false);
                }
            }
            else if (dropped.CompareTag("Tomato"))
            {
                if (GameStateManager.Instance.sandwichState == GameStateManager.SandwichState.CheeseSandwich_0)
                {
                    emptySandwich.sprite = cheeseTomatoSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.CheeseTomatoSandwich_0;
                    Debug.Log("cheese and tomato sandwich");
                    GameObject.FindWithTag("Tomato").SetActive(false);
                   
                }
                else
                {
                    emptySandwich.sprite = tomatoSandwich;
                    GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.TomatoSandwich_0;
                    Debug.Log("tomato sandwich");
                    GameObject.FindWithTag("Tomato").SetActive(false);
                }
            }

        }
        else if (SandwichItemID == "Bin")
        {
            Debug.Log("Item dropped onto the bin");
            if (dropped.CompareTag("Ham") || dropped.CompareTag("Plate"))
            {
                Debug.Log(dropped.tag + " cannot be dropped onto the bin");
            }
            else
            {
                dropped.SetActive(false);
                emptySandwich.sprite = noFillingSandwich;
                GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.Nothing;
                //Destroy(dropped); //Destroy function was used from https://docs.unity3d.com/ScriptReference/Object.Destroy.html
            }

        }
        else if (SandwichItemID == "Plate")
        {
            if (dropped.CompareTag("Empty"))
            {
                Debug.Log("sandwich has been places onto the plate");
            Debug.Log("Item has been placed onto the plate");
            rectTransformTarget.anchoredPosition = new Vector2(118, -80); //code taken from https://discussions.unity.com/t/how-do-i-change-a-ui-elements-y-position-in-code/740452/3
            }else
            {
                Debug.Log("Item cannot be placed onto the plate");
            }
        }
    }
}
        //checking if the item slot being used is the coffee machine
        /*
        if (SandwichItemID == "Bread")
        {
            Debug.Log("Item has been placed ontop of the bread");

            //checking if a cup has been placed onto the coffee machine
            if (dropped.CompareTag("Ham")) //.Compare Tag is used from https://community.gamedev.tv/t/the-difference-between-other-gameobject-tag-and-other-gameobject-comparetag/214190
            {
                sandwichCode.fillingState = FillingState.Ham;          
                Debug.Log("Filling state: " + sandwichCode.fillingState);
            }
            //checking if the item dropped is the milk jug
            else if (dropped.CompareTag("Lettuce"))
            {
                Debug.Log("lettuce has been placed on the bread");
                Debug.Log("Filling state: " + sandwichCode.fillingState);

            }

            else if (dropped.CompareTag("Tomato"))
            {
                Debug.Log("tomato has been placed on the bread");
                // Print the current state before updating
    Debug.Log("Current fillingState before update: " + sandwichCode.fillingState);

    sandwichCode.fillingState = FillingState.Tomato;  // Update filling state

    // Log the new state after update
    Debug.Log("Filling state after update: " + sandwichCode.fillingState);


            }
            else if (dropped.CompareTag("Cheese"))
            {
                Debug.Log("cheese has been placed on the bread");
                Debug.Log("Filling state: " + fillingState);

            }
            else
            {
                Debug.Log("Item cannot be dropped on the bread");
                Debug.Log("Filling state: " + fillingState);
            }
        }
        else if (SandwichItemID == "Ham")
        {
            if (dropped.CompareTag("Bread"))
            {
                Debug.Log("Ham Sandwich");
                sandwichState = SandwichState.HamSandwich;
                Debug.Log(sandwichState);
            }

            if (dropped.CompareTag("Lettuce"))
            {
                Debug.Log("Lettuce and Ham on Bread");
                sandwichCode.fillingState = FillingState.HamLettuce;
            }

        }else if (SandwichItemID == "Lettuce")
        {
            if (dropped.CompareTag("Bread"))
            {
                Debug.Log("B"+sandwichCode.fillingState);
            }
        }
    } 
 }

*/


