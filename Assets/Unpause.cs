using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unpause : MonoBehaviour
{
    public void OnBackClicked()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
