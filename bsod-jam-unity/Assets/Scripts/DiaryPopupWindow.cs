using UnityEngine;
using UnityEngine.UI;

public class DiaryPopupWindow : MonoBehaviour
{
    [SerializeField]
    private Button _quitButton;

    private void Start()
    {
        _quitButton.onClick.AddListener(() => Destroy(gameObject));
    }
}
