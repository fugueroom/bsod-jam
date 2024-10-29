using TMPro;
using UnityEngine;

public class DiaryPopupWindow : QuittableWindow
{
    [SerializeField]
    private TMP_InputField diaryInputField;

    [SerializeField]
    private TextMeshProUGUI diaryEntryTitle;

    private void Awake()
    {
        if (GameflowManager.Instance != null)
        {
            diaryInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "hey " + GameflowManager.Instance.PlayerName + "!";
        }

        diaryInputField.onValueChanged.AddListener(OnDiaryWrite);
    }

    private void OnDiaryWrite(string value)
    {
        if (value.Contains("chummy"))
        {
            ChummyManager.Instance.ChummyOneLiner("are u talking about me?");
        }
    }

    public void SetDiaryTitle(string text)
    {
        diaryEntryTitle.text = text;
    }

    public void SetDiaryText(string text)
    {
        diaryInputField.text = text;
    }
}
