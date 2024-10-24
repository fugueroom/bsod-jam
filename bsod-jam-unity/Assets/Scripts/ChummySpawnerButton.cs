using UnityEngine;

public class ChummySpawnerButton : PopupSpawnerButton
{
    [SerializeField]
    private RectTransform AlertPopup;

    protected override void OnSpawnerButtonSelected()
    {
        if (ChummyManager.Instance.IsSpawned)
        {
            PopupPrefab = AlertPopup;
        }

        base.OnSpawnerButtonSelected();
    }
}
