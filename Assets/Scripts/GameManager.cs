using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject pauseMenu;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePauseMenu();
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ClosePauseMenu()
    {
        var cam = camera.GetComponent<CameraController>();
        cam.enabled = !cam.enabled;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
