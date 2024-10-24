using System.Threading.Tasks;
using UnityEngine;
using TMPro;

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

    public async Task Talk(string text)
    {
        speechText.text = string.Empty;
        speechBubble.SetActive(true);

        for (int i = 0; i < text.Length; i++)
        {
            speechText.text += text[i];
            await Task.Delay(talkDelay);

            chummyMouth.sprite = i % 2 == 0 ? mouthOpenSprite : mouthCloseSprite;
        }

        await Task.Delay(1000);

        speechText.text = string.Empty;
        speechBubble.SetActive(false);
    }
}
