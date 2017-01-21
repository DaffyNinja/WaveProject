using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    [FMODUnity.EventRefAttribute]
    FMOD.Studio.EventInstance uiSFX;

    void Awake()
    {
     
        mainMenuPanel.SetActive(true);

        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);

    }

    public void PlayButton()
    {


        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/sfx_UI_Play", transform.position);

        SceneManager.LoadScene(1);

    }

    public void OptionsButton()
    {
        optionsPanel.SetActive(true);

        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(false);

        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/sfx_UI_Play",transform.position);

    }

    public void CreditsButton()
    {


        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/sfx_UI_Play", transform.position);

        creditsPanel.SetActive(true);

        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(false);


       


    }

    public void QuitButton()
    {

        FMODUnity.RuntimeManager.PlayOneShot("event:/sfx/sfx_UI_Play", transform.position);

        Application.Quit();
    }

    public void BackButton()
    {
        mainMenuPanel.SetActive(true);

        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
}
