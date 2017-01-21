using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    void Awake()
    {
        mainMenuPanel.SetActive(true);

        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsButton()
    {
        optionsPanel.SetActive(true);

        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(false);

    }

    public void CreditsButton()
    {
        creditsPanel.SetActive(true);

        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(false);


    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        mainMenuPanel.SetActive(true);

        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
}
