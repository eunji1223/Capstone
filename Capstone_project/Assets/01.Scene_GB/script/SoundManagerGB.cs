using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerGB : MonoBehaviour
{
    public AudioSource bgm;
    public GameObject MusicOnButton;
    public GameObject MusicOffButton;
    private bool muted = false;
    private void Awake()
    {
        var soundManagers = FindObjectsOfType<SoundManagerGB>();
        if(soundManagers.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateBtnImg();
        AudioListener.pause = muted;
        bgm.Play();
        
    }

    public void MusicButtonClick()
    {
        if(muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateBtnImg();
    }
    private void UpdateBtnImg()
    {
        // MusicOnButton�� MusicOffButton�� null�� �ƴ� ���� �۵��ϵ��� Ȯ��
        if (MusicOnButton != null && MusicOffButton != null)
        {
            if (muted == false)
            {
                MusicOnButton.SetActive(true);
                MusicOffButton.SetActive(false);
            }
            else
            {
                MusicOnButton.SetActive(false);
                MusicOffButton.SetActive(true);
            }
        }
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted",muted ? 1 : 0);
    }
    void Update()
    {

    }
}
