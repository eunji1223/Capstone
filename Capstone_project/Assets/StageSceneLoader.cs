using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string Stagescene)
    {
        SceneManager.LoadScene(Stagescene); // ������ �̸��� ���� �ε�
    }
}
