using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool isDead = false;
    [HideInInspector] public float startTime = 0;

    [Header ("Game over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause Menu")]
    [SerializeField] private GameObject pauseScreen;

    [Header("GameComplete")]
    [SerializeField] private GameObject gameCompleteScreen;
    [SerializeField] private Text currentTime;
    [SerializeField] private Text bestTime;
    [SerializeField] Transform start;   

    [Header("Components to disable")]
    [SerializeField] private Behaviour[] pauseScreenComponents;
    [SerializeField] private Behaviour[] gameOverScreenComponents;
    [SerializeField] private PlayerHealth playerHealth;

    public void GameOver(bool status)
    {
        isDead = status;
        foreach (var component in gameOverScreenComponents)
        {
            component.enabled = !status;
        }

        gameOverScreen.SetActive(status);

        if (status)
            Time.timeScale = 0;
        else
        {
            Time.timeScale = 1;
            playerHealth.Respawn();
        }
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }


    //Options functions

    public void Respawn()
    {
        gameCompleteScreen.SetActive(false);
        Time.timeScale = 1;
        foreach (var component in pauseScreenComponents)
        {
            component.enabled = true;
        }
        FindObjectOfType<PlayerController>().transform.position = start.position;
        startTime = Time.time;
    }


    public void PauseGame(bool status)
    {
        if (isDead)
            return;
        foreach(var component in pauseScreenComponents)
        {
            component.enabled = !status;
        }

        pauseScreen.SetActive(status);

        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void GameComplete()
    {
        
        foreach (var component in pauseScreenComponents)
        {
            component.enabled = false;
        }


        int minutes = Mathf.FloorToInt(Time.time - startTime / 60);
        int seconds = Mathf.FloorToInt(Time.time - startTime % 60);
        currentTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        float record;
        if (PlayerPrefs.HasKey("record"))
            record = PlayerPrefs.GetFloat("record");
        else
            record = Time.time - startTime;
        record = Mathf.Min(Time.time - startTime, record);


        PlayerPrefs.SetFloat("record", record);
        minutes = Mathf.FloorToInt(record / 60);
        seconds = Mathf.FloorToInt(record % 60);
        bestTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        gameCompleteScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
