using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource;

    public void SetMusicVolume(float volume){
        musicsource.volume = volume;
    }
}
