using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClickSoundGB : MonoBehaviour
{
    public AudioSource clickSound;
    public GameObject SoundOnButton;
    public GameObject SoundOffButton;
    private bool isMuted = false;

    private void Awake()
    {
        var soundManagers = FindObjectsOfType<SoundManagerGB>();
        if (soundManagers.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadSettings();
    }

    void Start()
    {
        UpdateButtonIcons();
    }

    public void ToggleSoundEffect()
    {
        isMuted = !isMuted;
        clickSound.mute = isMuted;
        SaveSettings();
        UpdateButtonIcons();
    }

    public void PlayClickSound()
    {
        if (!isMuted)
        {
            clickSound.Play();
        }
    }

    private void UpdateButtonIcons()
    {
        // SoundOnButton�� SoundOffButton�� null�� �ƴ��� Ȯ��
        if (SoundOnButton != null && SoundOffButton != null)
        {
            if (!isMuted)
            {
                SoundOnButton.SetActive(true);
                SoundOffButton.SetActive(false);
            }
            else
            {
                SoundOnButton.SetActive(false);
                SoundOffButton.SetActive(true);
            }
        }
    }


    private void LoadSettings()
    {
        isMuted = PlayerPrefs.GetInt("soundMuted", 0) == 1;
        clickSound.mute = isMuted;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("soundMuted", isMuted ? 1 : 0);
    }
}
