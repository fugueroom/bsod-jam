using UnityEngine;
using UnityEngine.UI;

public class QuittableWindow : MonoBehaviour
{
    [SerializeField]
    private Button _quitButton;

    protected virtual void Start()
    {
        _quitButton.onClick.AddListener(() => Destroy(gameObject));
    }
}
