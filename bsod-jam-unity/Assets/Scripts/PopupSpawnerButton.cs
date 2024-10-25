using UnityEngine;

public class PopupSpawnerButton : DoubleClickButton
{
    [SerializeField]
    protected RectTransform PopupPrefab;

    private Canvas rootCanvas;
    protected RectTransform popupInstance;

    protected virtual void OnEnable()
    {
        rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
        OnDoubleClick += OnSpawnerButtonSelected;
    }

    protected virtual void OnDisable()
    {
        OnDoubleClick -= OnSpawnerButtonSelected;
    }

    protected virtual void OnSpawnerButtonSelected()
    {
        popupInstance = Instantiate<RectTransform>(PopupPrefab, rootCanvas.transform);
        Vector3 newPos = popupInstance.anchoredPosition;
        newPos.x += Random.Range(-100f, 100f);
        newPos.y += Random.Range(-100f, 100f);
        popupInstance.anchoredPosition = newPos;
    }
}
