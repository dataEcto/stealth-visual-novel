using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonsScript : MonoBehaviour
{

    void Start()
    {

    }


    void Update()
    {

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Load Menu");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart Scene");
    }

}
