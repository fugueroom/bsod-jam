using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.Audio;

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

    [SerializeField]
    private TextMeshProUGUI TitleText;

    [SerializeField]
    private TextMeshProUGUI ExplainerText;

    [SerializeField]
    private TextMeshProUGUI HighScoreText;

    [SerializeField]
    private TextMeshProUGUI AnyKeyToContinueText;

    [SerializeField]
    private AudioResource PointScoredSFX;

    private int currentScore;
    private float currentFallingSpeed;
    private int currentTimeBetweenWords;
    private bool gameOver;
    private bool gameStarted;
    private int highScore;

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

    private void Start()
    {
        ChummyManager.Instance.ChummyClickClackReact().Forget();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        TitleText.gameObject.SetActive(false);
        ExplainerText.gameObject.SetActive(false);

        GameOverText.gameObject.SetActive(false);
        HighScoreText.gameObject.SetActive(false);
        AnyKeyToContinueText.gameObject.SetActive(false);

        gameStarted = true;
        gameOver = false;
        currentScore = 0;
        currentFallingSpeed = 50;
        currentTimeBetweenWords = 1000;

        ScoreText.text = currentScore.ToString();

        ContinueCreatingWords(gameObject.GetCancellationTokenOnDestroy()).Forget();
    }

    private async UniTask ContinueCreatingWords(CancellationToken ct)
    {
        while (!gameOver)
        {
            CreateFallingText(wordset[Random.Range(0, wordset.Length)]);

            await UniTask.Delay(currentTimeBetweenWords);
            ct.ThrowIfCancellationRequested();
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

        var texts = GetComponentsInChildren<CCText>();

        foreach (var text in texts)
        {
            Destroy(text.gameObject);
        }

        HighScoreText.gameObject.SetActive(true);
        AnyKeyToContinueText.gameObject.SetActive(true);

        if (currentScore > highScore)
        {
            // set new high score text
            HighScoreText.text = "new high score!!!: " + currentScore.ToString();
            highScore = currentScore;
        }
        else
        {
            HighScoreText.text = "high score: " + highScore.ToString();
        }

        gameOver = true;
        gameStarted = false;

        ChummyManager.Instance.ChummyOneLiner("too bad...");
    }

    public void AddToScore()
    {
        currentScore++;
        ScoreText.text = currentScore.ToString();

        // Adjust difficulty!
        if (currentScore == 15)
        {
            currentFallingSpeed *= 1.5f;
            ChummyManager.Instance.ChummyOneLiner("pretty good...");
        }
        else if (currentScore == 40)
        {
            currentFallingSpeed *= 1.4f;
            currentTimeBetweenWords = 800;
            ChummyManager.Instance.ChummyOneLiner("gotta type faster..");
        }
        else if (currentScore == 75)
        {
            currentFallingSpeed *= 1.3f;
            ChummyManager.Instance.ChummyOneLiner("FASTER!!");
        }
        else if (currentScore == 100)
        {
            currentFallingSpeed *= 1.2f;
            currentTimeBetweenWords = 500;
            ChummyManager.Instance.ChummyOneLiner("more! more! more!");
        }
        else if (currentScore == 125)
        {
            currentFallingSpeed *= 1.1f;
            ChummyManager.Instance.ChummyOneLiner("such fast fingers!");
        }

        UIAudioSource.Instance.PlayClip(PointScoredSFX);
    }
}
