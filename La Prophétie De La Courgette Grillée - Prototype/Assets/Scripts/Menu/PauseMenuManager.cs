using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource musicManager;
    public Text text;
    private bool isOpen;


    private void Start()
    {
        isOpen = false;
    }

    private void Update()
    {
        double volume = System.Math.Round((double)musicManager.volume, 2);
        text.text = volume.ToString();
        if (Input.GetButtonDown("Pause"))
        {
            if(!isOpen)
            {
                Time.timeScale = 0;
                isOpen = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                isOpen = false;
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }

    public void QuitterMenu()
    {
        isOpen = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
