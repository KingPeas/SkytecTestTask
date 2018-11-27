using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [SceneName]
    public string SceneGame = "";
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(SceneGame))
            SceneManager.LoadScene(SceneGame);
    }

    public void ConnectToGame()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
