using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class CCManager : MonoBehaviour
{
    [SerializeField]
    private CCText FallingTextPrefab;

    [SerializeField]
    private float FallingTextStartingYPos;

    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private TextMeshProUGUI GameOverText;

    private int currentScore;
    private float currentFallingSpeed = 50f;
    private bool gameOver;

    public static CCManager Instance;

    private static readonly string[] wordset = new string[] 
    /** 10 3-letter words**/ { "fun", "win", "dog", "cat", "toy", "art", "zen", "kid", "yay", "sun",
    /** 10 4-letter words**/   "door", "wrap", "roll", "moon", "sway", "play", "tree", "jinx", "open", "cool",
    /** 10 5-letter words**/    "smile", "happy", "quick", "laugh", "zebra", "magic", "frown", "water", "champ", "tower",
    /** 5 6-letter words**/    "friend", "umpire", "handle", "kitten", "orange",
    /** 4 7-letter words**/     "opossum", "syringe", "wallaby", "biscuit",
    /** 3 8-letter words**/     "computer", "kangaroo", "absolute" };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ContinueCreatingWords().Forget();
    }

    private async UniTaskVoid ContinueCreatingWords()
    {
        while (!gameOver)
        {
            CreateFallingText(wordset[Random.Range(0, wordset.Length)]);
            await UniTask.Delay(1000);
        }
    }

    private void CreateFallingText(string word)
    {
        var text = Instantiate(FallingTextPrefab, transform);
        text.Initialize(new Vector3(Random.Range(-100f, 100f), FallingTextStartingYPos, 0f), word, currentFallingSpeed);
    }

    public void GameOver()
    {
        GameOverText.gameObject.SetActive(true);
        gameOver = true;

        var texts = GetComponentsInChildren<CCText>();

        foreach (var text in texts)
        {
            Destroy(text.gameObject);
        }
    }

    public void AddToScore()
    {
        currentScore++;
        ScoreText.text = currentScore.ToString();

        if (currentScore == 20)
        {
            currentFallingSpeed *= 1.5f;
        }
        else if (currentScore == 50)
        {
            currentFallingSpeed *= 1.4f;
        }
        else if (currentScore == 80)
        {
            currentFallingSpeed *= 1.3f;
        }
        else if (currentFallingSpeed == 100)
        {
            currentFallingSpeed *= 1.2f;
        }
    }
}
