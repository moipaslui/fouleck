using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitMenu()
    {
        Application.Quit();
    }
}