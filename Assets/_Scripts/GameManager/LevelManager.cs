using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /* Add Win Condition Panels Here */
    [SerializeField] private GameObject rewardScreenDamageCard;
    [SerializeField] private GameObject rewardScreenWeaponCard;

    [SerializeField] private List<WeaponCardRewards> weaponsPanelList;
    [SerializeField] private List<DamageCardRewards> damagePanelList;
    [SerializeField] private GameObject LoadingScreen;

    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI endText;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.EndCondition.ON_LOSE, PlayerLost);
        EventBroadcaster.Instance.AddObserver(EventNames.EndCondition.ON_WINN, PlayerWin);
        EventBroadcaster.Instance.AddObserver(EventNames.UI.DAMAGE_REWARD_OPEN, OpenDamageRewards);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.EndCondition.ON_LOSE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.EndCondition.ON_WINN);
        EventBroadcaster.Instance.RemoveObserver(EventNames.UI.DAMAGE_REWARD_OPEN);
    }

    public void PlayerLost()
    {
        // Add Lose Screen before loading, or go to loading screen
        EnemyLibrary.Instance.ResetCurrentStageNumber();
        PlayerData.Instance.OnReset();
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGM(MyStrings.Audio.Level1Theme);
        }

        EnemyLibrary.Instance.ResetCurrentStageNumber();
        endScreen.SetActive(true);
        endText.text = "YOU LOST!";
        //SceneManager.LoadScene(MyStrings.MainMenu);
    }

    public void PlayerWin()
    {
        // Add Win Screen before loading, or go to loading screen
        if(EnemyLibrary.Instance.GetRemainingStageCount() > 0) 
        {
            //Disable Enemy & Player if needed
            OpenWeaponRewards();
        }
        else
        {
            // Player Defeats the Boss
            EnemyLibrary.Instance.SaveCurrentProgress();
            EnemyLibrary.Instance.ResetCurrentStageNumber();
            PlayerData.Instance.OnReset();
            endScreen.SetActive(true);

            string extraText;
            switch(EnemyLibrary.Instance.GetCurrentLevel())
            {
                case Levels.LEVEL_4:
                    extraText = "GAME END!";
                    break;

                default:
                    extraText = "NEXT AREA UNLOCKED";
                    break;
            }

            endText.text = "YOU WIN!" + "\n" + extraText;
            //SceneManager.LoadScene(MyStrings.MainMenu);
        }
    }

    public void OpenDamageRewards()
    {
        rewardScreenWeaponCard.SetActive(false);
        rewardScreenDamageCard.SetActive(true);
        foreach(DamageCardRewards panel in damagePanelList)
        {
            panel.SetReward();
        }
    }
    public void OpenWeaponRewards()
    { 
        rewardScreenWeaponCard.SetActive(true);
        
        foreach(WeaponCardRewards panel in weaponsPanelList)
        {
            panel.SetReward();
        }
    }

    //public void LoadNextLevel()
    //{
    //    SceneManager.LoadScene(MyStrings.Play);
    //}

    public void LoadNextLevel()
    {
        StartCoroutine(LoadSceneAsync(MyStrings.Play));
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

    public void ToMainMenuPressed()
    {
        AudioManager.Instance.PlayUI_SFX("Button Press SFX");
        SceneManager.LoadScene(MyStrings.MainMenu);
    }
}
