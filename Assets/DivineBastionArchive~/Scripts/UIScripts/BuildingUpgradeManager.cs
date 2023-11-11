using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject moneyText;
    [SerializeField] private GameObject orbsText;
    [SerializeField] private GameObject stickText;
    int Money;
    int Orbs;
    int Sticks;

    // Start is called before the first frame update
    void Start()
    {
        MoneyGrab();
        OrbsGrab();
        SticksGrab();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoneyGrab()
    {
        TextMeshProUGUI moneyNumber = moneyText.GetComponent<TextMeshProUGUI>();
        Money = PlayerPrefs.GetInt(PlayerValues.MONEY);
        moneyNumber.text = Money.ToString();
    }

    void OrbsGrab()
    {
        TextMeshProUGUI orbsNumber = orbsText.GetComponent<TextMeshProUGUI>();
        Orbs = PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB);
        orbsNumber.text = Orbs.ToString();
    }
    void SticksGrab()
    {
        TextMeshProUGUI sticksNumber = stickText.GetComponent<TextMeshProUGUI>();
        Sticks = PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB);
        sticksNumber.text = Sticks.ToString();
    }
}
