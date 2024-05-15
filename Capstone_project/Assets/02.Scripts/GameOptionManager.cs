using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
/* 게임 씬 버튼 또는 옵션 기능 구현 --> 추후 다른 cs파일의 같은 기능끼리 통합할 예정 */
public class GameOptionManager : MonoBehaviour
{
    [SerializeField]
    public GameObject optionWindow;
    [SerializeField]
    private GameObject optionSet;
    [SerializeField]
    public GameObject helpWindow;

    void Awake()
    {
        optionSet.SetActive(false);
        optionWindow.SetActive(false);
        helpWindow.SetActive(false);
    }
    public void OptionDown()
    {
        Time.timeScale = 0;
        optionWindow.SetActive(true);
    }
    public void OptionToOption()
    {
        optionWindow.SetActive(true);
    }
    public void OptionConfirm()
    {
        optionWindow.SetActive(false);
        Time.timeScale = 1;
    }
    public void OptionToOptionOut()
    {
        optionWindow.SetActive(false);
        optionSet.SetActive(true);
    }
    public void help()
    {
        helpWindow.SetActive(true);
    }
    public void helpOut()
    {
        helpWindow.SetActive(false);
        optionWindow.SetActive(true);
    }
    public void ExitMap()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Startscene");
    }


    public void ClickOptionButton(){
        optionSet.SetActive(true);
    }

    public void ClickCloseButton(){
        optionSet.SetActive(false);
    }

    public void OnMoveSite()
    {
        Application.OpenURL("https://honeymind.co.kr/");
    }

    public void ClickBackButton(){
        SceneManager.LoadScene("LobbyScene");
    }
}
