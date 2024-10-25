using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using TMPro;

public class GameflowManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField currentUserInputField;

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

        LoadScene(1).Forget();
    }

    public void LoadChummyStartScreen()
    {
        LoadScene(2).Forget();
    }

    public void LoadCorruptedOS()
    {
        LoadScene(3).Forget();
    }

    public async UniTaskVoid LoadScene(int scene)
    {
        await SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }
}
