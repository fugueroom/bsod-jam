using UnityEngine;

[CreateAssetMenu(fileName = "FallingTextConfig", menuName = "Scriptable Objects/FallingTextConfig")]
public class FallingTextConfig : ScriptableObject
{
    public float StartingFallSpeed;
    public int StartingTimeBetweenWords;
    public string[] Wordset;
}
