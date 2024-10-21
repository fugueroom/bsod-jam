using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindowBorder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private Vector3 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        offset = transform.parent.position - Input.mousePosition;
        transform.parent.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }

    private void Update()
    {
        if (pointerDown)
        {
            transform.parent.position = Input.mousePosition + offset;
        }
    }
}
