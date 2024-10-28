using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

[RequireComponent(typeof(Button))]
public class DoubleClickButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private AudioResource OnDoubleClickAudio;

    protected Action OnDoubleClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        UIAudioSource.Instance.PlayClip(OnDoubleClickAudio);

        if (eventData.clickCount == 2)
        {
            OnDoubleClick?.Invoke();
        }
    }
}
