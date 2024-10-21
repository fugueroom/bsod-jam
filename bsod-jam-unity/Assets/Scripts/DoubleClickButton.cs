using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class DoubleClickButton : MonoBehaviour, IPointerClickHandler
{
    protected Action OnDoubleClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            OnDoubleClick?.Invoke();
        }
    }
}
