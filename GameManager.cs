using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Object mainMenuScene;

    private int level = 0;
    
    void Awake()
    {
        EnforceSingletonGameManager();
        DontDestroyOnLoad(gameObject);
        InitGame();
    }
     
    void EnforceSingletonGameManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if ( instance != this)
        {
            Destroy(gameObject);
        }
    }

    void InitGame()
    {
        Debug.Log("Game manager: InitGame called.");
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        Debug.Log("Game manager: LoadMainMenu called");
        gameObject.GetComponent<changeScene>().loadScene(mainMenuScene.name);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Pause button pressed");
        }
    }

    void PauseGame()
    {

    }
}
