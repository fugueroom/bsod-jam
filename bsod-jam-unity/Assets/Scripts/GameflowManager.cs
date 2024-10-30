using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using TMPro;
using DG.Tweening;

public class GameflowManager : MonoBehaviour
{
    [SerializeField]
    private Image Fader;

    public string PlayerName { get; private set; }

    [HideInInspector]
    public int CurrentCCHighScore;

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
        PlayerName = FindAnyObjectByType<TMP_InputField>().text;

        if (string.IsNullOrEmpty(PlayerName))
        {
            PlayerName = "friend";
        }

        LoadScene(1).Forget();
    }

    public void LoadStartScreen()
    {
        PlayerName = string.Empty;
        LoadScene(0).Forget();
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
        var sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (var source in sources)
        {
            source.DOFade(0f, 1f);
        }

        Fader.gameObject.SetActive(true);
        await Fader.DOFade(1f, 2f).AsyncWaitForCompletion();

        await SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

        await Fader.DOFade(0f, 0.2f).AsyncWaitForCompletion();
        Fader.gameObject.SetActive(false);
    }
}
