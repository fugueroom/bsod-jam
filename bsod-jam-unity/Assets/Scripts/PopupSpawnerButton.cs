using UnityEngine;

public class PopupSpawnerButton : DoubleClickButton
{
    [SerializeField]
    private RectTransform PopupPrefab;

    [SerializeField]
    private Canvas RootCanvas;

    private void OnEnable()
    {
        OnDoubleClick += OnSpawnerButtonSelected;
    }

    private void OnDisable()
    {
        OnDoubleClick -= OnSpawnerButtonSelected;
    }

    private void OnSpawnerButtonSelected()
    {
        RectTransform popup = Instantiate<RectTransform>(PopupPrefab, RootCanvas.transform);
        Vector3 newPos = popup.anchoredPosition;
        newPos.x += Random.Range(-100f, 100f);
        newPos.y += Random.Range(-100f, 100f);
        popup.anchoredPosition = newPos;
    }
}
