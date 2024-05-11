using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneChangerGB : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Gamescene");
    }
}
