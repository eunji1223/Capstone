using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class InfoOptionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionSet;

    void Awake()
    {
        optionSet.SetActive(false);
    }

    public void ClickBackButton(){
        SceneManager.LoadScene("LobbyScene");
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

}
