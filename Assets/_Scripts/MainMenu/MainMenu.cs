using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private List<Button> mainButtons;
    [SerializeField] private Toggle debugToggle;
    [SerializeField] private GameObject LoadingScreen;
    //public Image LoadingBarFill;

    private void Start()
    {
        AudioManager.Instance.PlayBGM(MyStrings.Audio.MainMenuBGM);
    }

    public void Play()
    {
        SceneManager.LoadScene(MyStrings.Play);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SettingsOpen()
    {
        settingsMenu.SetActive(true);
        foreach(Button button in mainButtons)
        {
            button.interactable = false;
        }
    }

    public void SettingsClose()
    {
        settingsMenu.SetActive(false);
        foreach(Button button in mainButtons)
        {
            button.interactable = true;
        }
    }

    public void ToggleDebugKill(bool isOn)
    {
        if(isOn == true)
        {
            CheatsManager.Instance.DebugDamage = true;
            Debug.Log("On");
        }
        else if(isOn == false)
        {
            CheatsManager.Instance.DebugDamage = false;
            Debug.Log("Off");
        }
    }

    public void ResetTutorial()
    {
        PlayerPrefs.SetInt("tutorial", 0);
    }

    public void LoadNewGame()
    {
        AudioManager.Instance.StopBGM(MyStrings.Audio.MainMenuBGM);

        string levelPlay = MyStrings.Play;
        StartCoroutine(LoadSceneAsync(levelPlay));
    }

    IEnumerator LoadSceneAsync(string levelPlay)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelPlay);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            //float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            //LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }
}
