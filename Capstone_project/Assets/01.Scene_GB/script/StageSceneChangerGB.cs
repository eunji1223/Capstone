using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneChangerGB : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Stagescene");
    }
}
