using UnityEngine;

public class DisablePlayerInput : MonoBehaviour
{
    [SerializeField] private GameObject blackOutPanel;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.EndCondition.ON_COMBAT_END, DisableInput);       
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.EndCondition.ON_COMBAT_END);
    }

    public void DisableInput()
    {
        blackOutPanel.SetActive(true);
    }


}
