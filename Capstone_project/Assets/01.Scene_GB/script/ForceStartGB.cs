using UnityEngine;

using UnityEngine.SceneManagement;



public class ForceStartGB : MonoBehaviour
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    static void FirstLoad()

    {

        if (SceneManager.GetActiveScene().name.CompareTo("Startscene") != 0)

        {

            SceneManager.LoadScene("Startscene");

        }

    }

}