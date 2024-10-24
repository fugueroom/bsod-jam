using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoaderPopupWindow : QuittableWindow
{
    [SerializeField]
    private Slider loadingBar;

    [SerializeField]
    private float loadingSpeed;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(LoadingBar());
    }

    private IEnumerator LoadingBar()
    {
        while (loadingBar.value < 1f)
        {
            yield return new WaitForSeconds(loadingSpeed);
            loadingBar.value += 0.1f;
        }

        ChummyManager.Instance.SpawnChummy();

        Destroy(gameObject);
    }
}
