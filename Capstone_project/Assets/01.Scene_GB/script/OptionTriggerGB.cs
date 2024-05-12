using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionTriggerGB : MonoBehaviour
{
    public GameObject Optionwindow;
    public GameObject Optionwindows;
    public GameObject Helpwindow;

    public void OptionDown()
    {
        Time.timeScale = 0;
        Optionwindow.SetActive(true);
    }
    public void OptionToOption()
    {
        Optionwindows.SetActive(true);
    }
    public void OptionConfirm()
    {
        Optionwindow.SetActive(false);
        Time.timeScale = 1;
    }
    public void OptionToOptionOut()
    {
        Optionwindows.SetActive(false);
        Optionwindow.SetActive(true);
    }
    public void help()
    {
        Helpwindow.SetActive(true);
    }
    public void helpOut()
    {
        Helpwindow.SetActive(false);
        Optionwindow.SetActive(true);
    }
    public void ExitMap()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Startscene");
    }

}
