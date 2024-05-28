using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*로딩 씬 호출*/
public class MoveManager : MonoBehaviour
{
    public void MoveToLoadingScene(){
        ButtonManager.Instance.SaveGameData();
        LoadingSceneController.Instance.LoadScene("GameScene");
    }

    public void MoveToInfoScene(){
        SceneManager.LoadScene("StatsScene-1");
    }
}
