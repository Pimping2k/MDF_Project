using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Функция должна быть public
    }

    public void OpenOptions()
    {
        Debug.Log("Options menu opened"); // Добавьте код для вызова окна опций
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exited");
    }
}


