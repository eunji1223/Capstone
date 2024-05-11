using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string Stagescene)
    {
        SceneManager.LoadScene(Stagescene); // 지정된 이름의 씬을 로드
    }
}
