using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Transform Inventory;
    public Image image;
    public Transform ParentAfterDrag;
    public Transform ParentBeforeDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        ParentAfterDrag = transform.parent;
        ParentBeforeDrag = transform.parent;
        transform.SetParent(Inventory);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ParentAfterDrag.childCount > 0)
        {
            transform.SetParent(ParentBeforeDrag);
            image.raycastTarget = true;
        }
        else
        {
            transform.SetParent(ParentAfterDrag.transform);
            image.raycastTarget = true;
        }
    }

    void Awake()
    {
        Inventory = GameObject.Find("Canvas").transform;
    }
}
