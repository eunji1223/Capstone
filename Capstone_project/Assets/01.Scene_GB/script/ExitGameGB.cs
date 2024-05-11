using UnityEngine;

public class ExitGameGB: MonoBehaviour
{
    public void Exit()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
