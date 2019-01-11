using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class quitGame : MonoBehaviour
{
    public void closeApplication()
    {
        Debug.Log("Closing, bye!");
        Application.Quit();
    }
}
