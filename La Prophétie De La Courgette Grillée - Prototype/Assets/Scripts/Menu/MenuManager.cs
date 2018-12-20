using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject options;
    public AudioSource source;
    public Text text;

    private void Update()
    {
        double volume = System.Math.Round((double)source.volume,2);
        text.text = volume.ToString();
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void AfficherOptions()
    {
        options.SetActive(true);
        menu.SetActive(false);
    }

    public void RetournerMenu()
    {
        menu.SetActive(true);
        options.SetActive(false);
    }

    public void QuitMenu()
    {
        Application.Quit();
    }
}