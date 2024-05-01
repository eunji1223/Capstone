using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSet : MonoBehaviour
{
    public Slider slider;
    public Text text;
    private float value = 0;
    private int p=0;

    private void sliderPlus(){
        if(value<1f){
            
        }
    }
    public void setVolume(float volume)
    {
        Debug.Log(volume);    
    }
}
