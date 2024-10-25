using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChummyStartScreenManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField playerInputField;

    [SerializeField]
    private Button goButton;

    private void Start()
    {
        if (GameflowManager.Instance != null)
        {
            playerInputField.placeholder.GetComponent<TextMeshProUGUI>().text = GameflowManager.Instance.PlayerName;
            goButton.onClick.AddListener(GameflowManager.Instance.LoadCorruptedOS);
        }
    }
}
