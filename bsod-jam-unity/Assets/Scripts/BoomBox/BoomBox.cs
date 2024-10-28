using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BoomBox : MonoBehaviour
{
    [SerializeField]
    private List<BoomboxSong> Songs;

    [SerializeField]
    private TextMeshProUGUI CurrentSongTitle;

    [SerializeField]
    private TextMeshProUGUI CurrentArtistTitle;

    [SerializeField]
    private Image CurrentSongBackgroundImage;

    [SerializeField]
    private Sprite PauseSprite;

    [SerializeField]
    private Sprite PlaySprite;

    [SerializeField]
    private Button PlayPauseButton;

    AudioSource audioSource;

    private bool isPlaying;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.resource = Songs[0].SongClip;
        audioSource.Play();

        isPlaying = true;
        PlayPauseButton.image.sprite = PauseSprite;

        CurrentSongTitle.text = Songs[0].SongTitle;
        CurrentSongTitle.fontMaterial = Songs[0].SongTitleFontVariant;

        CurrentArtistTitle.text = Songs[0].ArtistTitle;
        CurrentArtistTitle.fontMaterial = Songs[0].ArtistTitleFontVariant;

        CurrentSongBackgroundImage.color = Songs[0].SongBackgroundColor;

        PlayPauseButton.onClick.AddListener(TogglePlayPause);
    }

    private void TogglePlayPause()
    {
        if (isPlaying)
        {
            PlayPauseButton.image.sprite = PlaySprite;
            audioSource.Pause();
            isPlaying = false;
        }
        else
        {
            PlayPauseButton.image.sprite = PauseSprite;
            audioSource.Play();
            isPlaying = true;
        }
    }
}
