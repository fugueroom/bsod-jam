using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChummyManager : MonoBehaviour
{
    [SerializeField]
    private Volume GlobalVolume;

    [SerializeField]
    private Chummy ChummyPrefab;

    [SerializeField]
    private Transform ChummySpawnPoint;

    [SerializeField]
    private RectTransform ChummyNoResponsePrefab;

    [SerializeField]
    private Canvas RootCanvas;

    [SerializeField]
    private GameObject BSOD;

    private Chummy chummyInstance;

    public static ChummyManager Instance;
    public bool IsSpawned { get; private set; }
    bool trashAlertedLevel1, trashAlertedLevel2, trashAlertedLevel3;

    private CancellationTokenSource chummyTalkCT;

    public enum TrashAlertLevel
    {
        Low,
        Medium,
        High
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    private void OnEnable()
    {
        chummyTalkCT = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        chummyTalkCT.Dispose();
    }

    public void SpawnChummy()
    {
        IsSpawned = true;
        chummyInstance = Instantiate(ChummyPrefab);
        chummyInstance.transform.position = ChummySpawnPoint.position;
        chummyInstance.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        chummyInstance.transform.DOMoveX(5f, 3f).OnComplete(() =>
        {
            chummyInstance.transform.DORotate(Vector3.zero, 3f).OnComplete(() =>
            {
                // chummy talketh
                ChummyIntro().Forget();
            });
        });
    }

    public void ChummyTrashAlert(TrashAlertLevel level)
    {
        if (IsSpawned)
        {
            if (level == TrashAlertLevel.Low && !trashAlertedLevel1)
            {
                trashAlertedLevel1 = true;
                ChummySingleTalk("what are you doing?!").Forget();
            }
            else if (level == TrashAlertLevel.Medium && !trashAlertedLevel2)
            {
                trashAlertedLevel2 = true;
                ChummySingleTalk("get away from there!!!!").Forget();
            }
            else if (level == TrashAlertLevel.High && !trashAlertedLevel3)
            {
                trashAlertedLevel3 = true;
                ChummySingleTalk("AGHGHAHGHGGHHGH").Forget();
            }
        }
    }

    public async UniTaskVoid ChummyBoogie()
    {
        if (IsSpawned)
        {
            await chummyInstance.Talk("i love this song", chummyTalkCT.Token);
            await chummyInstance.Talk("makes me wanna dance", chummyTalkCT.Token);

            chummyInstance.transform.DOShakeRotation(15f, 10f, 10, 45, true);
        }
    }

    private bool chummyClickClackReact;

    public async UniTaskVoid ChummyClickClackReact()
    {
        if (IsSpawned && !chummyClickClackReact)
        {
            chummyClickClackReact = true;
            await chummyInstance.Talk("heh heh", chummyTalkCT.Token);
            await chummyInstance.Talk("get ready to type", chummyTalkCT.Token);
            await chummyInstance.Talk("with your human fingers", chummyTalkCT.Token);
            await chummyInstance.Talk("must be nice", chummyTalkCT.Token);
        }
    }

    public void ChummyOneLiner(string text)
    {
        if (IsSpawned)
        {
            ChummySingleTalk(text).Forget();
        }
    }

    private async UniTaskVoid ChummySingleTalk(string text)
    {
        chummyTalkCT.Cancel();
        chummyTalkCT = new CancellationTokenSource();

        await chummyInstance.Talk(text, chummyTalkCT.Token);
    }

    private async UniTaskVoid ChummyIntro()
    {
        string playerName = "friend";

        if (GameflowManager.Instance != null)
        {
            playerName = GameflowManager.Instance.PlayerName;
        }
        
        await chummyInstance.Talk("heh heh", chummyTalkCT.Token);
        await chummyInstance.Talk("hey...", chummyTalkCT.Token);
        await chummyInstance.Talk("..." + playerName, chummyTalkCT.Token);
        await chummyInstance.Talk("names CHUMMY", chummyTalkCT.Token);
        await chummyInstance.Talk("need something?", chummyTalkCT.Token);        
    }

    public void TrashChummy()
    {
        if (IsSpawned)
        {
            PitchDownAllAudio();

            chummyInstance.transform.DOScaleX(8f, 30f);
            chummyInstance.transform.DOScaleY(0.3f, 30f);

            AdjustBloomOnChummyTrashed().Forget();
            SpawnNoResponsePopup().Forget();
        }
    }

    private void PitchDownAllAudio()
    {
        var audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource audioSource in audioSources)
        {
            PitchDown(audioSource, gameObject.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    private async UniTask PitchDown(AudioSource source, CancellationToken ct)
    {
        while (source.pitch > 0.1f)
        {
            source.pitch -= 0.01f;

            await UniTask.Delay(150);
            ct.ThrowIfCancellationRequested();
        }
    }

    private async UniTask AdjustBloomOnChummyTrashed()
    {
        if (GlobalVolume.profile.TryGet<ColorAdjustments>(out var colorAdjustments))
        {
            colorAdjustments.colorFilter.value = Color.red;
        }

        if (GlobalVolume.profile.TryGet<Bloom>(out var bloom))
        {
            if (GlobalVolume.profile.TryGet<LensDistortion>(out var lensDistortion))
            {
                float maxBloom = 20f;

                while (bloom.intensity.value < maxBloom)
                {
                    bloom.intensity.value += Time.deltaTime;
                    lensDistortion.intensity.value -= Time.deltaTime;
                    await UniTask.Delay(1);
                }
            }
        }
    }

    private async UniTask SpawnNoResponsePopup()
    {
        for (int i = 0; i < 100; i++)
        {
            RectTransform popup = Instantiate<RectTransform>(ChummyNoResponsePrefab, RootCanvas.transform);
            Vector3 newPos = popup.anchoredPosition;
            newPos.x += (i * 5) - 250f;
            newPos.y += (i * 5) - 250f;
            popup.anchoredPosition = newPos;
            await UniTask.Delay(50);
        }

        await UniTask.Delay(200);

        BSOD.transform.SetAsLastSibling();
        BSOD.SetActive(true);

        while (!Input.anyKeyDown) { await UniTask.Delay(1); }

        if (GameflowManager.Instance != null)
        {
            GameflowManager.Instance.LoadChummyStartScreen();
        }
    }
}
