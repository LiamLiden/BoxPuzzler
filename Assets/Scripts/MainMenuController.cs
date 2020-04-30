using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject levelSelectPanel;

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void ToggleLevelSelect()
    {
        levelSelectPanel.SetActive(!levelSelectPanel.activeSelf);
    }
}
