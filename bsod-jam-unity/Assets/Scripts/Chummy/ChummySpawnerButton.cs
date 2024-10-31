using UnityEngine;
using UnityEngine.EventSystems;

public class ChummySpawnerButton : PopupSpawnerButton, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private RectTransform AlertPopup;

    [SerializeField]
    private Trashcan TrashcanIcon;

    private float currentTrashDistance;
    private bool isSelected;

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSelected = false;

        if (currentTrashDistance < 120f)
        {
            // set icon image
            TrashcanIcon.SetTrashFull();

            // trash chummy
            ChummyManager.Instance.TrashChummy();

            // trash chummy button
            Destroy(gameObject);
        }
    }

    protected override void OnSpawnerButtonSelected()
    {
        if (ChummyManager.Instance.IsSpawned)
        {
            PopupPrefab = AlertPopup;
        }
        else
        {
            // make the icon draggable only once chummy has been spawned
            GetComponent<UIDraggable>().enabled = true;
        }

        base.OnSpawnerButtonSelected();
    }

    private void Update()
    {
        if (isSelected)
        {
            CheckTrashDistance();
        }
    }

    private void CheckTrashDistance()
    {
        // check distance from trash icon
        currentTrashDistance = (TrashcanIcon.transform.position - transform.position).magnitude;

        if (currentTrashDistance < 1500f && currentTrashDistance > 900f)
        {
            ChummyManager.Instance.ChummyTrashAlert(ChummyManager.TrashAlertLevel.Low);
        }
        else if (currentTrashDistance > 300f && currentTrashDistance < 900f)
        {
            ChummyManager.Instance.ChummyTrashAlert(ChummyManager.TrashAlertLevel.Medium);
        }
        else if (currentTrashDistance < 300f)
        {
            ChummyManager.Instance.ChummyTrashAlert(ChummyManager.TrashAlertLevel.High);
        }
    }
}
