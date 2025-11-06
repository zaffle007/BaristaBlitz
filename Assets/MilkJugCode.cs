
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MilkJugCode : MonoBehaviour, IDropHandler
{
    public Image targetImage; // Reference to this image's component
    public Image changedImage;

    public string ItemSlotID;
    
    private void Awake()
    {
        Debug.Log("Awake2");
        if (targetImage == null)
            targetImage = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (dropped == null) return;

        DraggableImage item = dropped.GetComponent<DraggableImage>();

        if (item == null) return;

        Debug.Log("Dropped item2");
        Debug.Log("OnDrop2");
        Image droppedImage = eventData.pointerDrag?.GetComponent<Image>();

            if (droppedImage != null && targetImage != null)
            {
                targetImage.sprite = changedImage.sprite;
            }

            dropped.GetComponent<RectTransform>().anchoredPosition =
                GetComponent<RectTransform>().anchoredPosition;
        

    }
    

}
