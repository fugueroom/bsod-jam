using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "BoomboxSong", menuName = "Scriptable Objects/BoomboxSong")]
public class BoomboxSong : ScriptableObject
{
    public string SongTitle;
    public string ArtistTitle;
    public AudioResource SongClip;
    public Color SongBackgroundColor;
    public Material SongTitleFontVariant;
    public Material ArtistTitleFontVariant;
}
