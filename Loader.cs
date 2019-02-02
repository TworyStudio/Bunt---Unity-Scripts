using System.Collections;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {
        InstantiateManagers(); 
    }

    void InstantiateManagers()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
