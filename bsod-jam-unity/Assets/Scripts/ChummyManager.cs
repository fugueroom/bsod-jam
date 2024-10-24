using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class ChummyManager : MonoBehaviour
{
    [SerializeField]
    private Chummy ChummyPrefab;

    [SerializeField]
    private Transform ChummySpawnPoint;

    private Chummy chummyInstance;

    public static ChummyManager Instance;
    public bool IsSpawned { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
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

    public async UniTaskVoid ChummyIntro()
    {
        await chummyInstance.Talk("heh heh");
        await chummyInstance.Talk("hey...");
        await chummyInstance.Talk("names CHUMMY");
        await chummyInstance.Talk("need something?");
    }
}
