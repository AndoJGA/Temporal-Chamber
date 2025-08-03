using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float totalTime = 10f; // Total time for the timer
    [SerializeField] private TextMeshProUGUI timerText; // Reference to the UI text component
    private float currentTime;
    [SerializeField] private GameObject UiPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject PausePanel;

    void Start()
    {
        currentTime = 0f; // Initialize the timer
        Time.timeScale = 1; // Ensure the game time is running
        GameOverPanel.SetActive(false); // Ensure the game over panel is hidden at the start
        UiPanel.SetActive(true); // Ensure the UI panel is active at the start
    }

    void Update()
    {
        if (currentTime < totalTime)
        {
            timerText.text = (totalTime - currentTime).ToString("F0");
            currentTime += Time.deltaTime; // Increment the timer
        }
        else
        {
            ShowGameOverPanel();
        }
    }

    private void ShowGameOverPanel()
    {
        Time.timeScale = 0; // Stop the game time
        GameOverPanel.SetActive(true);
        UiPanel.SetActive(false);
    }

    public void Restart()
    {
        GameOverPanel.SetActive(false); // Ensure the game over panel is hidden at the start
        UiPanel.SetActive(true); // Ensure the UI panel is active at the start
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentTime = 0f; // Initialize the timer
        Time.timeScale = 1; // Ensure the game time is running
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        UiPanel.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        UiPanel.SetActive(true);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

}
