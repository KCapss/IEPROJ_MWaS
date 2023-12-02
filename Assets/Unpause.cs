using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unpause : MonoBehaviour
{
    public void OnBackClicked()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public void OnAbandonClick()
    {
        Time.timeScale = 1.0f;
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGM(MyStrings.Audio.Level1Theme);
        }
        EnemyLibrary.Instance.ResetCurrentStageNumber();
        PlayerData.Instance.OnReset();
        SceneManager.LoadScene("MainMenu");
    }
}
