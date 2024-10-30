using UnityEngine;

public class BFTypeableText : TypeableText
{
    private void OnEnable()
    {
        OnTextPastBottomThreshold += Cleanup;
        OnTextTyped += AddToAttackBar;
    }

    private void AddToAttackBar()
    {
        ChummyBossManager.Instance.AddToAttackBar();
        Destroy(gameObject);
    }

    private void Cleanup()
    {
        Destroy(gameObject);
    }
}
