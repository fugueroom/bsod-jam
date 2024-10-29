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

    private static readonly string[] chummyReactions = new string[] { "whats that..", "what r u reading?", "what does that say?", "can i see that?" }; 

    protected override void OnSpawnerButtonSelected()
    {
        base.OnSpawnerButtonSelected();

        if ( popupInstance != null )
        {
            var diaryPopup = popupInstance.GetComponent<DiaryPopupWindow>();

            diaryPopup.SetDiaryText(EntryText);
            diaryPopup.SetDiaryTitle(EntryTitle);

            ChummyManager.Instance.ChummyOneLiner(chummyReactions[Random.Range(0, chummyReactions.Length - 1)]);
        }
    }
}
