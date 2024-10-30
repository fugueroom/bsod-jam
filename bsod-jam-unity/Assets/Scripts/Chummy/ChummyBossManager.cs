using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

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

    private void Start()
    {
        ChummyIntroTalk(gameObject.GetCancellationTokenOnDestroy()).Forget();
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

        await ChummyBoss.transform.DOMoveX(0.5f, 2f).AsyncWaitForCompletion();
        ChummyBoss.transform.DOMoveX(-0.5f, 2f).SetLoops(-1, LoopType.Yoyo);

        while (BossAudio.pitch < 1.5f)
        {
            BossAudio.pitch += 0.01f;

            await UniTask.Delay(100);
            ct.ThrowIfCancellationRequested();
        }
    }
}
