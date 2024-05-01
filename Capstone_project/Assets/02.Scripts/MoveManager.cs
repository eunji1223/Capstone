using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveManager : MonoBehaviour
{
    public void MoveToLoadingScene(){
        LoadingSceneController.Instance.LoadScene
        ("StatsScene-1");
    }
}
