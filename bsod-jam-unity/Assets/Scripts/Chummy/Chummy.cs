using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
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

    private AudioSource speechAudio;

    private void Awake()
    {
        speechAudio = GetComponent<AudioSource>();
    }

    public async UniTask Talk(string text, CancellationToken ct)
    {
        speechText.text = string.Empty;
        speechBubble.SetActive(true);
        speechAudio.Play();

        for (int i = 0; i < text.Length; i++)
        {
            speechText.text += text[i];

            await UniTask.Delay(talkDelay, cancellationToken : ct);
            ct.ThrowIfCancellationRequested();

            chummyMouth.sprite = i % 2 == 0 ? mouthOpenSprite : mouthCloseSprite;
            speechAudio.pitch = Random.Range(0.5f, 1.5f);
        }

        speechAudio.Pause();

        await UniTask.Delay(1000, cancellationToken : ct);
        ct.ThrowIfCancellationRequested();

        speechText.text = string.Empty;
        speechBubble.SetActive(false);
    }
}
