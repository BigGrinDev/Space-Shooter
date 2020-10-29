using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool gameOver;
    

    


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && gameOver)
        {

            Scene _currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(_currentScene.buildIndex);
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();
        }
    }




    public void GameOver()
    {
        gameOver = true;
    }
}
