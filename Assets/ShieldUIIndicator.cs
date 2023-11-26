using System.Collections;
using UnityEngine;

public class ShieldUIIndicator : MonoBehaviour
{
    [SerializeField] private GameObject shieldIndicator;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.UI.SHIELDS_UP, ShowShieldIndicator);
    }

    private void OnDestroy()
    {
         EventBroadcaster.Instance.RemoveObserver(EventNames.UI.SHIELDS_UP);
    }

    public void ShowShieldIndicator(Parameters param)
    {
        float shieldTimer = param.GetFloatExtra(EventNames.UI.SHIELDS_UP, 0.0f);
        StartCoroutine(ShieldTimer(shieldTimer));
    }

    IEnumerator ShieldTimer(float timer)
    {
        shieldIndicator.SetActive(true);
        yield return new WaitForSeconds(timer);
        shieldIndicator.SetActive(false);
    }

}
