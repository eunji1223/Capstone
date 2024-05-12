using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 사용

public class CountDownGB : MonoBehaviour
{
    float setTime = 10.0f;
    public TextMeshProUGUI CountText; // Text 대신 TextMeshProUGUI 사용

    void Start()
    {
        CountText.text = setTime.ToString();
    }

    void Update() 
    {
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;
        }
        else if (setTime <= 0)
        {
            Time.timeScale = 0.0f;
        }
        CountText.text = Mathf.Round(setTime).ToString();
    }
}
