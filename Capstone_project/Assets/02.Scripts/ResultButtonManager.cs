using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

/*결과 창의 버튼 관리, 게임을 다시하거나 로비로 이동할 때 활용*/
public class ResultButtonManager : MonoBehaviour
{
    public void OnMoveGameScene(){
        SceneManager.LoadScene("GameScene");
    }

    public void OnMoveLobbyScene(){
        SceneManager.LoadScene("LobbyScene");
    }
}
