using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionInGB : MonoBehaviour
{
    public GameObject Optionwindow;
    public void OnClickOptionButton()
    {
        Optionwindow.SetActive(true);
    }
}
