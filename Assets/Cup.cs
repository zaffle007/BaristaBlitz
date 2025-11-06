using UnityEngine;

//cup code
/*
public enum CupState
{
    Empty,
    Espresso,
    Latte
}

public class Cup : MonoBehaviour
{
    private Collider2D col;

    private Vector3 startDragPosition;
    private bool isDragging = false;
    private Vector3 offset;

    public CupState cupState = CupState.Empty;
    public Sprite espressoSprite;
    public Sprite latteSprite;
    private SpriteRenderer sr;


    private void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        startDragPosition = transform.position;
        transform.position = GetMousePositionInWorldSpace();
        isDragging = true;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, 0);
    }

    private void OnMouseDrag()
    {
                if (!isDragging) return;
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePos.z = 0f;
            //transform.position = mousePos;
            transform.position = new Vector3(mousePos.x, mousePos.y, 0) + offset;
        }
        //transform.position = GetMousePositionInWorldSpace();
        //Debug.Log("Dragging Cup");
    }

    private void OnMouseUp()
    {
        isDragging = false;
        /*
        Debug.Log("Dropped Cup");
        isDragging = false;
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;
        if (hitCollider != null && hitCollider.TryGetComponent(out ICupDropArea cupDropArea))
        {
            cupDropArea.OnCupDrop(this);
        }else
        {
            transform.position = startDragPosition;
        }
        
    }

    public void ChangeStateTo(CupState newState){
        cupState = newState;

        switch (newState)
        {
            case CupState.Espresso:
                sr.sprite = espressoSprite;
                break;
            case CupState.Latte:
                sr.sprite = latteSprite;
                break;
        }
    }
   
    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}


*/

//cup code


public class Cup : MonoBehaviour
{
    private Collider2D col;

    private Vector3 startDragPosition;



    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        startDragPosition = transform.position;
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorldSpace();
        Debug.Log("Dragging Cup");
    }

    private void OnMouseUp()
    {

        Debug.Log("Dropped Cup");

        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;
        if (hitCollider != null && hitCollider.TryGetComponent(out ICupDropArea cupDropArea))
        {
            cupDropArea.OnCupDrop(this);
        }else
        {
            transform.position = startDragPosition;
        }
    }

   
    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}






