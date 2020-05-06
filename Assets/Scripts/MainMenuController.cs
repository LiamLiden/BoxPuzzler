using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject levelSelectPanel;

    /// <summary>
    /// Loads level with same name as input string.
    /// </summary>
    /// <param name="level"></param>
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// Toggles the level select panel.
    /// </summary>
    public void ToggleLevelSelect()
    {
        levelSelectPanel.SetActive(!levelSelectPanel.activeSelf);
    }

    /// <summary>
    /// Quits the exe.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
