using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneChangerGB: MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Startscene");
    }
}
