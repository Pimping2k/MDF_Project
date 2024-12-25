using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Make sure the scene index or name is correct in the Build Settings
    }

    public void OpenOptions()
    {
        Debug.Log("Options menu opened"); // Add code here to show your options menu
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game exited");
    }

    // Add functions to open external links
    public void OpenDiscord()
    {
        Application.OpenURL("https://discord.com/invite/example"); // Replace with your Discord invite link
        Debug.Log("Discord link opened");
    }

    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/example"); // Replace with your Twitter profile link
        Debug.Log("Twitter link opened");
    }
}
