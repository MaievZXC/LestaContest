using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool isDead = false;

    [Header ("Game over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause Menu")]
    [SerializeField] private GameObject pauseScreen;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(0);
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

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
