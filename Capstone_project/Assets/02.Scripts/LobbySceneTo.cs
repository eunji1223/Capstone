using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneTo : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public void LobbyToAnotherScene(){
        SceneManager.LoadScene(sceneName);
    }
}
