using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void OnPauseButtonClick()
    {
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
    }
}
