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
        SceneManager.LoadScene("MainMenu");
    }
}
