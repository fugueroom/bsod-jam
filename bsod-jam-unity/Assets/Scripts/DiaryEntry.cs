using UnityEngine;
using TMPro;

public class DiaryEntry : PopupSpawnerButton
{
    [SerializeField]
    private string EntryText;

    [SerializeField]
    private string EntryTitle;

    protected override void OnEnable()
    {
        base.OnEnable();

        var title = GetComponentInChildren<TextMeshProUGUI>();
        title.text = EntryTitle;
    }

    protected override void OnSpawnerButtonSelected()
    {
        base.OnSpawnerButtonSelected();

        if ( popupInstance != null )
        {
            var diaryPopup = popupInstance.GetComponent<DiaryPopupWindow>();

            diaryPopup.SetDiaryText(EntryText);
            diaryPopup.SetDiaryTitle(EntryTitle);
        }
    }
}
