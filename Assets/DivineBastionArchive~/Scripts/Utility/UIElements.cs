using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIElements : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.EndCondition.ON_WINN, this.PlayerWin);
        EventBroadcaster.Instance.AddObserver(EventNames.EndCondition.ON_LOSE, this.PlayerLoss);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.EndCondition.ON_WINN);
        EventBroadcaster.Instance.RemoveObserver(EventNames.EndCondition.ON_LOSE);
    }

    private void PlayerLoss()
    {
        losePanel.SetActive(true);
    }

    private void PlayerWin()
    {
        winPanel.SetActive(true);
    }

    public void OnAttackPressed()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.UI.CHARACTER_ATTK);
    }

    public void OnWaitPressed()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.UI.CHARACTER_WAIT);
    }

    public void OnItemsPressed()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.UI.CHARACTER_ITEM);
    }

    public void OnTurnEndPressed()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.UI.END_TURN);
    }

    public void OnContinuePressed()
    {
        SceneManager.LoadScene(2);
    }
}
