using UnityEngine;

public class ChummySpawnerButton : PopupSpawnerButton
{
    [SerializeField]
    private RectTransform AlertPopup;

    [SerializeField]
    private GameObject TrashIcon;

    private float currentTrashDistance;

    protected override void OnSpawnerButtonSelected()
    {
        if (ChummyManager.Instance.IsSpawned)
        {
            PopupPrefab = AlertPopup;
        }

        base.OnSpawnerButtonSelected();
    }

    private void Update()
    {
        // check distance from trash icon
        currentTrashDistance = (TrashIcon.transform.position - transform.position).magnitude;

        if (currentTrashDistance < 1000f && currentTrashDistance > 500f)
        {
            ChummyManager.Instance.ChummyTrashAlert(ChummyManager.TrashAlertLevel.Low);
        }
        else if (currentTrashDistance > 250f && currentTrashDistance < 500f)
        {
            ChummyManager.Instance.ChummyTrashAlert(ChummyManager.TrashAlertLevel.Medium);
        }
        else if (currentTrashDistance < 250f)
        {
            ChummyManager.Instance.ChummyTrashAlert(ChummyManager.TrashAlertLevel.High);
        }
    }
}
