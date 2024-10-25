using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using TMPro;

public class GameflowManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField currentUserInputField;

    public UIDraggable CurrentDraggable;

    public string PlayerName { get; private set; }

    public static GameflowManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void LoadOS()
    {
        PlayerName = currentUserInputField.text;

        if (string.IsNullOrEmpty(PlayerName))
        {
            PlayerName = "friend";
        }

        LoadMainScene().Forget();
    }

    public async UniTaskVoid LoadMainScene()
    {
        await SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

}
