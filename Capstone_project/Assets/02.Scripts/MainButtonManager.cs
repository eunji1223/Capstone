using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* main(start scene)의 버튼 기능 구현 */
public class MainButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionSet;
    [SerializeField]
    private GameObject outWindow;
    void Awake()
    {
        optionSet.SetActive(false);
        outWindow.SetActive(false);
    }
    public void ChangeSceneToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnClickOptionButton(){
        optionSet.SetActive(true);
    }

    public void OnClickCloseButton(){
        optionSet.SetActive(false);
    }

    public void OnClickBackButton()
    {
        outWindow.SetActive(true);
    }
    public void OnClicknotButton()
    {
        outWindow.SetActive(false);
    }
    public void OnClickExitButton()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnMoveSite()
    {
        Application.OpenURL("https://honeymind.co.kr/");
    }
}
