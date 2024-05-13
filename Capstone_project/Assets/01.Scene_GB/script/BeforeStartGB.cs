using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeforeStartGB : MonoBehaviour
{
    public TMP_Text beforeStartText;
    private float startTime;

    private void Start()
    {
        if (beforeStartText != null)
        {
            Time.timeScale = 0f; //���� ����
            StartCoroutine(StartGame());
        }
    }
    private IEnumerator StartGame()
    {
        beforeStartText.text = "준비";
        startTime = Time.realtimeSinceStartup;
        yield return new WaitForSecondsRealtime(1);
        beforeStartText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        beforeStartText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        beforeStartText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        beforeStartText.text = "시작!";
        yield return new WaitForSecondsRealtime(1);
        beforeStartText.gameObject.SetActive(false);
        Time.timeScale = 1f; // ���� ����
    }
}