using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField] private Canvas MainMenuCanvas;
    [SerializeField] private Canvas SettingsCanvas;

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        MainMenuCanvas.gameObject.SetActive(false); 
        SettingsCanvas.gameObject.SetActive(true);
    }

    public void ReturnFromSettingsButton()
    {
        MainMenuCanvas.gameObject.SetActive(true);
        SettingsCanvas.gameObject.SetActive(false);
    }

    public void StartGameButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}
