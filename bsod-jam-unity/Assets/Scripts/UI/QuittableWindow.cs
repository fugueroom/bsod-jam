using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class QuittableWindow : MonoBehaviour
{
    [SerializeField]
    private Button _quitButton;

    [SerializeField]
    private AudioResource quitAudio;

    protected virtual void Start()
    {
        _quitButton.onClick.AddListener(OnQuit);
    }

    protected virtual void OnQuit()
    {
        UIAudioSource.Instance.PlayClip(quitAudio);
        Destroy(gameObject);
    }
}
