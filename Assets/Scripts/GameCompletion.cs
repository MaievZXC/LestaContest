using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCompletion : MonoBehaviour
{
    UIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            manager.GameComplete();
        }
    }
}
