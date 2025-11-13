using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AppearItem : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] public GameObject spawnableItem;
    public int x;
    public int y;

    public Sprite noFillingSandwich;

    public Sprite emptyCup;

    public void OnPointerClick(PointerEventData data) //https://docs.unity3d.com/2018.2/Documentation/ScriptReference/EventSystems.EventTrigger.html
    {
        Debug.Log("OnPointerClick called.");

        spawnableItem.SetActive(true); //https://discussions.unity.com/t/how-can-i-make-an-image-appear/686229/7
        spawnableItem.transform.position = new Vector2(x, y);


        if (spawnableItem.name.Contains("Empty") || spawnableItem.name.Contains("Bread"))
        {

            //resets sandwich state to being empty
            GameStateManager.Instance.sandwichState = GameStateManager.SandwichState.Nothing;
            GameStateManager.Instance.cupstate = GameStateManager.CupState.Empty;

            //checks what is being spawned is a sandwich and will set it to be empty
            Image sandwichImage = spawnableItem.GetComponent<Image>();
            Image cupImage = spawnableItem.GetComponent<Image>();
            if (sandwichImage != null && noFillingSandwich != null)
            {
                sandwichImage.sprite = noFillingSandwich;
            }
            if (cupImage != null && emptyCup != null)
            {
                cupImage.sprite = emptyCup;
            }
        }
    }


}
