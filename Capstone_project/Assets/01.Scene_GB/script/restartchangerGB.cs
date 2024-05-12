using UnityEngine;
using UnityEngine.SceneManagement;

public class restartchangerGB: MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Gamescene");
    }
}
