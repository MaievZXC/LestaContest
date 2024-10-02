using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationManager : MonoBehaviour
{
    UIManager uIManager;
    [SerializeField] GameObject gameOverScreen;
    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    public void Respawn()
    {
        transform.parent.GetComponent<PlayerHealth>().Respawn();

    }

    public void RestartScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
