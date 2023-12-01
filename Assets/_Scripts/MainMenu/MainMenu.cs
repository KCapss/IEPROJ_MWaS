using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  
    [SerializeField] private List<Button> mainButtons;
    [SerializeField] private Toggle debugToggle;
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private GameObject settingsMenu;
    //public Image LoadingBarFill;

    [Header("Visible Buttons")]
    [SerializeField] private GameObject originalTileScreen;
    [SerializeField] private GameObject playGameObject;
    [SerializeField] private GameObject settingGameObject;
    [SerializeField] private Animator animator;
    


    private void Start()
    {
        AudioManager.Instance.PlayBGM(MyStrings.Audio.MainMenuBGM);
    }

    public void Play()
    {
        AudioManager.Instance.PlayUI_SFX("Button Press SFX");
        SceneManager.LoadScene(MyStrings.Play);
    }

    public void Quit()
    {
        AudioManager.Instance.PlayUI_SFX("Button Press SFX");
        Application.Quit();
    }

    public void SettingsOpen()
    {
        AudioManager.Instance.PlayUI_SFX("Button Press SFX");
        settingsMenu.SetActive(true);
        foreach(Button button in mainButtons)
        {
            button.interactable = false;
        }
    }

    public void SettingsClose()
    {
        AudioManager.Instance.PlayUI_SFX("Button Press SFX");
        settingsMenu.SetActive(false);
        foreach(Button button in mainButtons)
        {
            button.interactable = true;
        }
    }

    public void ToggleDebugKill(bool isOn)
    {
        AudioManager.Instance.PlayUI_SFX("Button Press SFX");
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
        AudioManager.Instance.PlayUI_SFX("Button Press SFX");
        PlayerPrefs.SetInt("tutorial", 0);
    }

    public void PressPlay()
    {
        AudioManager.Instance.StopBGM(MyStrings.Audio.MainMenuBGM);
        originalTileScreen.SetActive(false);
        playGameObject.SetActive(false);
        settingsMenu.SetActive(false);
        animator.SetBool("isPlay", true);
    }

    public void LoadNewGame()
    {
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
