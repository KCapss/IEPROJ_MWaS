using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownIndicator : MonoBehaviour
{
    [SerializeField] public Image _indicatorImage;

    private void Start()
    {
        _indicatorImage.fillAmount = 0;
    }

    public void SetFillAmount(float fillAmount)
    {
        _indicatorImage.fillAmount = fillAmount;
    }

    //public void StartCooldownIndicator(float cooldownTime)
    //{
    //    _indicatorImage.fillAmount = 1.0f;
    //    StartCoroutine(CoolingDown(cooldownTime));
    //    _indicatorImage.fillAmount = 0;
    //}

    //IEnumerator CoolingDown(float cooldownTime)
    //{
    //    float startTime = Time.time;
    //    float elapsedTime = 0.0f;

    //    while (elapsedTime < cooldownTime) 
    //    {
    //        elapsedTime = Time.time - startTime;
    //        float completionRate = 1.0f - Mathf.Clamp01(elapsedTime / cooldownTime);
    //        _indicatorImage.fillAmount = completionRate;
    //        yield return null;
    //    }

    //    _indicatorImage.fillAmount = 0.0f;
    //}
}
