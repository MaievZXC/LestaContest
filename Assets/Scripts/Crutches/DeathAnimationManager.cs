using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimationManager : MonoBehaviour
{
    UIManager uIManager;
    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    //legacy
    public void Respawn()
    {
        transform.parent.GetComponent<PlayerHealth>().Respawn();

    }

    public void RestartScreen()
    {
        uIManager.GameOver(true);
    }
}
