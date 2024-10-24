using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryPopupWindow : QuittableWindow
{
    [SerializeField]
    private TMP_InputField diaryInputField;

    private void Awake()
    {
        if (GameflowManager.Instance != null) 
        {
            diaryInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "hey " + GameflowManager.Instance.PlayerName + "!";
        }
    }
}
