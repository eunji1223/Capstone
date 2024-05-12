using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� ���

public class CountDownGB : MonoBehaviour
{
    float setTime = 10.0f;
    public TextMeshProUGUI CountText; // Text ��� TextMeshProUGUI ���

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
