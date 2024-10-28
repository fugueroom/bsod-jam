using Cysharp.Threading.Tasks;
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

    [SerializeField]
    private Button NextSongButton;

    [SerializeField]
    private Button PrevSongButton;

    [SerializeField]
    private Slider VolumeSlider;

    AudioSource audioSource;

    private bool isPlaying;
    private int currentSongIndex;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        VolumeSlider.value = audioSource.volume;

        PlayPauseButton.onClick.AddListener(TogglePlayPause);
        NextSongButton.onClick.AddListener(PlayNextSong);
        PrevSongButton.onClick.AddListener(PlayPrevSong);
        VolumeSlider.onValueChanged.AddListener(SetVolume);

        SetCurrentSong().Forget();
    }

    private void PlayNextSong()
    {
        currentSongIndex++;

        // loop back to beginning if at end of songs
        if (currentSongIndex >= Songs.Count) { currentSongIndex = 0; }

        SetCurrentSong().Forget();
    }

    private void PlayPrevSong()
    {
        currentSongIndex--;

        // loop to end if at beginning
        if (currentSongIndex < 0) { currentSongIndex = Songs.Count - 1; }

        SetCurrentSong().Forget();
    }

    private async UniTaskVoid SetCurrentSong()
    {
        PlayPauseButton.image.sprite = PauseSprite;

        CurrentSongTitle.text = Songs[currentSongIndex].SongTitle;
        CurrentSongTitle.fontMaterial = Songs[currentSongIndex].SongTitleFontVariant;

        CurrentArtistTitle.text = Songs[currentSongIndex].ArtistTitle;
        CurrentArtistTitle.fontMaterial = Songs[currentSongIndex].ArtistTitleFontVariant;

        CurrentSongBackgroundImage.color = Songs[currentSongIndex].SongBackgroundColor;

        audioSource.resource = Songs[currentSongIndex].SongClip;

        await UniTask.Delay(100);

        audioSource.Play();
        isPlaying = true;
    }

    private void SetVolume(float volume)
    {
        audioSource.volume = volume;
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
