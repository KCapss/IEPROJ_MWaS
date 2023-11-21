using UnityEngine;
using UnityEngine.UI;

public class AnticipationBar : MonoBehaviour
{
    [SerializeField] private Image fillBar;

    // Update is called once per frame
    public void UpdateFillBar(float fillAmount)
    {
        fillBar.fillAmount = fillAmount;
    }
}
