using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutInGB : MonoBehaviour
{
    public GameObject Outwindow;
    public void OnClickBackButton()
    {
        Outwindow.SetActive(true);
    }
}
