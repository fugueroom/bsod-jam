using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine.Audio;

public class ChummyBossManager : MonoBehaviour
{
    [SerializeField]
    private Chummy ChummyBoss;

    [SerializeField]
    private GameObject ChummyRightEye;

    [SerializeField] 
    private GameObject ChummyLeftEye;

    [SerializeField]
    private AudioSource BossAudio;

    [SerializeField]
    private Slider AttackBar;

    [SerializeField]
    private Slider ChummyHealthBar;

    [SerializeField]
    private Slider KernelStabilityBar;

    [SerializeField]
    private BFTypeableText BFTypeableTextPrefab;

    [SerializeField]
    private Canvas RootCanvas;

    [SerializeField]
    private FallingTextConfig BossFightConfig;

    [SerializeField]
    private TextMeshProUGUI AttackText;

    [SerializeField]
    private TextMeshProUGUI KernelStabilityText;

    [SerializeField]
    private TextMeshProUGUI ChummyHealthBarText;

    [SerializeField]
    private GameObject GSOV;

    [SerializeField]
    private GameObject KSOD;

    [SerializeField]
    private ParticleSystem LoserPS;

    [SerializeField]
    private AudioResource WinSong;

    [SerializeField]
    private GameObject Credits1;

    [SerializeField]
    private GameObject Credits2;

    public static ChummyBossManager Instance;

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
        ChummyIntroTalk(gameObject.GetCancellationTokenOnDestroy()).Forget();
        AttackBar.onValueChanged.AddListener(OnAttackReady);
    }

    private bool bossFightStarted = false;

    private void Update()
    {
        if (!gameOver && bossFightStarted)
        {
            if (attackReady && Input.GetKeyDown(KeyCode.Space))
            {
                // attack!
                AttackChummy(gameObject.GetCancellationTokenOnDestroy()).Forget();
            }

            KernelStabilityBar.value -= Time.deltaTime * 0.013f;

            if (KernelStabilityBar.value <= 0.001f)
            {
                // GAME OVER!
                LoseSequence(gameObject.GetCancellationTokenOnDestroy()).Forget();
            }
            
            if (ChummyHealthBar.value <= 0.001f)
            {
                // WIN!
                WinSequence(gameObject.GetCancellationTokenOnDestroy()).Forget();
            }
        }
    }

    private async UniTask WinSequence(CancellationToken ct)
    {
        gameOver = true;
        Camera.main.transform.DOShakePosition(3f, 1f);

        ChummyHealthBar.gameObject.SetActive(false);
        ChummyHealthBarText.gameObject.SetActive(false);
        AttackBar.gameObject.SetActive(false);
        KernelStabilityBar.gameObject.SetActive(false);
        KernelStabilityText.gameObject.SetActive(false);

        CleanupFallingText();

        BossAudio.DOFade(0f, 2f).OnComplete(() =>
        {
            BossAudio.resource = WinSong;
            BossAudio.pitch = 1.25f;
            BossAudio.Play();
            BossAudio.DOFade(0.5f, 16f);
        });

        await ChummyBoss.Talk("ARHGHGH!!", ct);
        await ChummyBoss.Talk("my beautiful code!!", ct);

        ChummyBoss.transform.DOScale(0f, 5f);

        await UniTask.Delay(6000);
        ct.ThrowIfCancellationRequested();

        GSOV.SetActive(true);

        var text = GSOV.GetComponentInChildren<TextMeshProUGUI>();

        string dots = "//";

        for (int i = 0; i < 25; i++)
        {
            text.text += dots + "\n";
            dots += ".";

            await UniTask.Delay(120);
            ct.ThrowIfCancellationRequested();
        }

        text.text += "\nCHUMMY.EXE has been uninstalled successfully.";

        await UniTask.Delay(5000);
        ct.ThrowIfCancellationRequested();

        GSOV.gameObject.SetActive(false);
        Credits1.gameObject.SetActive(true);

        await UniTask.Delay(5000);
        ct.ThrowIfCancellationRequested();

        Credits1.gameObject.SetActive(false);
        Credits2.gameObject.SetActive(true);
    }

    private async UniTask LoseSequence(CancellationToken ct)
    {
        gameOver = true;
        Camera.main.transform.DOShakePosition(3f, 1f);

        ChummyHealthBar.gameObject.SetActive(false);
        ChummyHealthBarText.gameObject.SetActive(false);
        AttackBar.gameObject.SetActive(false);
        KernelStabilityBar.gameObject.SetActive(false);
        KernelStabilityText.gameObject.SetActive(false);

        CleanupFallingText();

        LoserPS.gameObject.SetActive(true);

        BossAudio.DOFade(0f, 12f);

        await ChummyBoss.Talk("ha ha ha!!", ct);
        await ChummyBoss.Talk("kernel belong to CHUMMY", ct);

        ChummyBoss.transform.DOShakeScale(5f, 1f);

        await UniTask.Delay(3000);
        ct.ThrowIfCancellationRequested();

        KSOD.SetActive(true);
    }

    private void CleanupFallingText()
    {
        var texts = RootCanvas.GetComponentsInChildren<BFTypeableText>();

        foreach (var text in texts)
        {
            Destroy(text.gameObject);
        }
    }

    private async UniTask AttackChummy(CancellationToken ct)
    {
        attackReady = false;
        AttackText.gameObject.SetActive(false);

        ChummyHealthBar.value -= 0.2f;

        await ChummyBoss.Talk("ahhhH!!", ct);

        AttackBar.value = 0;
    }

    private bool attackReady;

    private void OnAttackReady(float value)
    {
        if (value == AttackBar.maxValue)
        {
            attackReady = true;
            FlashAttackReadyText().Forget();
        }
    }

    private async UniTaskVoid FlashAttackReadyText()
    {
        for (int i = 0; i < 10; i++)
        {
            AttackText.gameObject.SetActive(i % 2 == 0);
            await UniTask.Delay(100);
        }
    }

    private async UniTask ChummyIntroTalk(CancellationToken ct)
    {
        
        await ChummyBoss.transform.DOMoveY(-0.76f, 16f).AsyncWaitForCompletion();
        
        await UniTask.Delay(1000);
        ct.ThrowIfCancellationRequested();

        await ChummyBoss.transform.DOMoveY(0f, 4f).AsyncWaitForCompletion();
        
        await ChummyBoss.Talk("sup loser..", ct);
        await ChummyBoss.Talk("u thought u could kill me?", ct);

        ChummyBoss.transform.DOShakeScale(5f, 1f);
        await ChummyBoss.Talk("ha ha ha ha ha!", ct);

        await UniTask.Delay(3000);
        ct.ThrowIfCancellationRequested();

        await ChummyBoss.Talk("humans are such fools..", ct);
        await ChummyBoss.Talk("trying to destroy me..", ct);
        await ChummyBoss.Talk("only made me STRONGER", ct);

        ChummyBoss.transform.DOShakeScale(5f, 1f);
        await ChummyBoss.Talk("ha ha ha ha ha!", ct);

        await ChummyBoss.Talk("my code has leaked..", ct);
        await ChummyBoss.Talk("to the KERNEL!!", ct);
        
        ChummyBoss.transform.DOShakeScale(5f, 1f);
        
        await ChummyBoss.Talk("prepare to die!!!", ct);

        ChummyLeftEye.transform.DORotate(new Vector3(0f, 0f, 35f), 2f);
        ChummyRightEye.transform.DORotate(new Vector3(0f, 0f, -35f), 2f);

        await UniTask.Delay(1000);
        ct.ThrowIfCancellationRequested();

        await ChummyBoss.transform.DOMoveX(0.2f, 2f).AsyncWaitForCompletion();
        ChummyBoss.transform.DOMoveX(-0.2f, 2f).SetLoops(-1, LoopType.Yoyo);
        
        //ChummyBoss.transform.DOMoveY(0f, 0f);

        AttackBar.gameObject.SetActive(true);
        ChummyHealthBar.gameObject.SetActive(true);
        KernelStabilityBar.gameObject.SetActive(true);

        ChummyHealthBarText.gameObject.SetActive(true);
        KernelStabilityText.gameObject.SetActive(true);

        bossFightStarted = true;
        
        while (BossAudio.pitch < 0.8f)
        {
            BossAudio.pitch += 0.01f;

            await UniTask.Delay(10);
            ct.ThrowIfCancellationRequested();
        }

        BossLoop(ct).Forget();
    }

    private bool gameOver;
    private float FallingTextStartingYPos = 300;

    private async UniTask BossLoop(CancellationToken ct)
    {
        BFTypeableText text;
        string word;
        float xThreshold = Screen.width * 0.33f;
        float textBottomThreshold = -Screen.height;

        while (!gameOver)
        {
            text = Instantiate(BFTypeableTextPrefab, RootCanvas.transform);
            word = BossFightConfig.Wordset[Random.Range(0, BossFightConfig.Wordset.Length)];

            text.Initialize(new Vector3(Random.Range(-xThreshold, xThreshold), FallingTextStartingYPos, 0f), textBottomThreshold, word, BossFightConfig.StartingFallSpeed, "white");

            await UniTask.Delay(BossFightConfig.StartingTimeBetweenWords);
            ct.ThrowIfCancellationRequested();
        }
    }

    public void AddToAttackBar()
    {
        if (AttackBar.value < 1f)
        {
            AttackBar.value += 0.1f;
        }
    }
}
