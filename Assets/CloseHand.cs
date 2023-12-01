using System.Collections;
using UnityEngine;

public class CloseHand : MonoBehaviour
{
    [SerializeField] private GameObject hand;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.EndCondition.ON_COMBAT_END, CloseHandObject);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.EndCondition.ON_COMBAT_END);
    }

    public void CloseHandObject()
    {
        StartCoroutine(DisableInput());
    }

    IEnumerator DisableInput()
    {
        yield return new WaitForEndOfFrame();
        hand.SetActive(false);
    }
}
