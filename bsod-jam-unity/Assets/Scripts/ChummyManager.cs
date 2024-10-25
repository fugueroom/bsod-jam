using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using System.Threading;

public class ChummyManager : MonoBehaviour
{
    [SerializeField]
    private Chummy ChummyPrefab;

    [SerializeField]
    private Transform ChummySpawnPoint;

    private Chummy chummyInstance;

    public static ChummyManager Instance;
    public bool IsSpawned { get; private set; }

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

    bool working = false;

    private void OnDisable()
    {
        chummyTalkCT.Dispose();
    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {

            chummyTalkCT?.Cancel();
            chummyTalkCT = new CancellationTokenSource();

            Work(chummyTalkCT.Token).Forget();
        }
    }

    private async UniTaskVoid Work(CancellationToken ct)
    {
        working = true;
        Debug.Log("Starting...");
        await UniTask.Delay(2000, cancellationToken: ct);
        ct.ThrowIfCancellationRequested();

        Debug.Log("Working...");
        await UniTask.Delay(2000, cancellationToken: ct);
        ct.ThrowIfCancellationRequested();

        Debug.Log("Done!");
        working = false;
    }
    */

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

    bool trashAlertedLevel1, trashAlertedLevel2, trashAlertedLevel3;

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
        chummyInstance.transform.DOShakePosition(3f, 1f);
    }
}
