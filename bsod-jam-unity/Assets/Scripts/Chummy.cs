using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Chummy : MonoBehaviour
{
    [SerializeField]
    private int talkDelay;

    [SerializeField]
    private SpriteRenderer chummyMouth;

    [SerializeField]
    private Sprite mouthOpenSprite;

    [SerializeField]
    private Sprite mouthCloseSprite;

    [SerializeField]
    private GameObject speechBubble;

    [SerializeField]
    private TextMeshPro speechText;

    public async UniTask Talk(string text, CancellationToken ct)
    {
        speechText.text = string.Empty;
        speechBubble.SetActive(true);

        for (int i = 0; i < text.Length; i++)
        {
            speechText.text += text[i];
            await UniTask.Delay(talkDelay, cancellationToken : ct);
            ct.ThrowIfCancellationRequested();

            chummyMouth.sprite = i % 2 == 0 ? mouthOpenSprite : mouthCloseSprite;
        }

        await UniTask.Delay(1000, cancellationToken : ct);
        ct.ThrowIfCancellationRequested();

        speechText.text = string.Empty;
        speechBubble.SetActive(false);
    }
}
