using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private bool ParentIsRoot;

    private bool pointerDown;
    private Vector3 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;

        if (ParentIsRoot)
        {
            offset = transform.parent.position - Input.mousePosition;
            transform.parent.SetAsLastSibling();
        }
        else
        {
            offset = transform.position - Input.mousePosition;
            transform.SetAsLastSibling();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }

    private void Update()
    {
        if (pointerDown)
        {
            if (ParentIsRoot)
            {
                transform.parent.position = Input.mousePosition + offset;
            }
            else
            {
                transform.position = Input.mousePosition + offset;
            }
        }
    }
}
