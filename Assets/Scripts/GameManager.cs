using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject pauseMenu;
    public GameObject camera;
    public Text timeText;
    public Text moveText;
    public Text bestText;
    public GameObject tutorial;
    public float defaultTime;
    public int defaultMoves;

    private float time;
    private int moves;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        time = 0;
        // Reset physics and timescale
        Physics.gravity = new Vector3(0, -9.81f, 0);
        Time.timeScale = 1;

        // Set best time based on prefs
        float bestTime = PlayerPrefs.GetFloat("time" + SceneManager.GetActiveScene().name, defaultTime);
        int bestMoves = PlayerPrefs.GetInt("moves" + SceneManager.GetActiveScene().name, defaultMoves);
        bestText.text = "Best Time and Moves\n Time: " + TimeSpan.FromSeconds(bestTime).ToString(@"mm\:ss\:ff") + " Moves: " + bestMoves;

        // Lock cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Update moves and time text
        time += Time.deltaTime;
        timeText.text = "Time: " + TimeSpan.FromSeconds(time).ToString(@"mm\:ss\:ff");
        moveText.text = "Moves: " + moves;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CloseTutorial();
        }
    }

    /// <summary>
    /// Loads next level.
    /// </summary>
    public void NextLevel()
    {
        // Save data if new record
        if (time < PlayerPrefs.GetFloat("time" + SceneManager.GetActiveScene().name, defaultTime))
        {
            PlayerPrefs.SetFloat("time" + SceneManager.GetActiveScene().name, time);
            PlayerPrefs.SetInt("moves" + SceneManager.GetActiveScene().name, moves);
        }
        
        // Load new level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Restarts current level.
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Loads main menu.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Toggles the pause menu.
    /// </summary>
    public void TogglePauseMenu()
    {
        // Freeze camera
        var cam = camera.GetComponent<CameraController>();
        cam.enabled = !cam.enabled;
        // Toggle cursor
        Cursor.visible = !Cursor.visible;
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        // Toggle menu
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        // Pause time
        if (Time.timeScale == 1)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    private void CloseTutorial()
    {
        if (tutorial != null)
            tutorial.SetActive(false);
    }

    public void IncrementMoves()
    {
        moves++;
    }

    public void MuteButton()
    {
        AudioController.instance.ToggleMute();
    }
}
