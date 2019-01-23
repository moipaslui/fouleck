using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public Text text;
    private bool isOpen;

    private void Start()
    {
        isOpen = false;
    }

    private void Update()
    {
        double volume = System.Math.Round(GameManager.musicManager.source.volume, 2) * 100;
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
                QuitterMenu();
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
