using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEvents : MonoBehaviour
{
    //Organize the events into different Section
    [Header("Main Building Sprite")]
    [SerializeField] private GameObject B1_defUprage_1;
    [SerializeField] private GameObject B1_EvUprage_1;
    [SerializeField] private GameObject B1_EvUprage_2;

    [Header("Building 2")]
    [SerializeField] private GameObject B2_defUprage_1;
    [SerializeField] private GameObject B2_EvUprage_1;
    [SerializeField] private GameObject B2_EvUprage_2;

    [Header("Building 3")]
    [SerializeField] private GameObject B3_defUprage_1;
    [SerializeField] private GameObject B3_EvUprage_1;
    [SerializeField] private GameObject B3_EvUprage_2;

    [Header("Building 4")]
    [SerializeField] private GameObject B4_defUprage_1;
    [SerializeField] private GameObject B4_EvUprage_1;
    [SerializeField] private GameObject B4_EvUprage_2;

    [Header("Building 5")]
    [SerializeField] private GameObject B5_defUprage_1;
    [SerializeField] private GameObject B5_EvUprage_1;
    [SerializeField] private GameObject B5_EvUprage_2;

    [Header("Building LimitSize")]
    [SerializeField] private int Building_Limit1;
    [SerializeField] private int Building_Limit2;
    [SerializeField] private int Building_Limit3;
    [SerializeField] private int Building_Limit4;
    [SerializeField] private int Building_Limit5;

    [Header("Extra")]
    [SerializeField] private GameObject endTab;

    /// <summary>
    /// Store per each set as a list and iterate until you reach final evolution
    /// </summary>
    /// 

    private List<GameObject> B1_Evolution;
    private List<GameObject> B2_Evolution;
    private List<GameObject> B3_Evolution;
    private List<GameObject> B4_Evolution;
    private List<GameObject> B5_Evolution;

    private int b1Index = 0;
    private int b2Index = 0;
    private int b3Index = 0;
    private int b4Index = 0;
    private int b5Index = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitUpgradeData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SubtractMoneyAndMaterials(int MoneySub, int OrbSub, int StickSub)
    {
        int MoneySubtract = PlayerPrefs.GetInt(PlayerValues.MONEY) - MoneySub;
        int OrbSubtract = PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB) - OrbSub;
        int StickSubtract = PlayerPrefs.GetInt(PlayerValues.STICK) - StickSub;
        PlayerPrefs.SetInt(PlayerValues.MONEY, MoneySubtract);
        PlayerPrefs.SetInt(PlayerValues.CELESTIAL_ORB, OrbSubtract);
        PlayerPrefs.SetInt(PlayerValues.STICK, StickSubtract);
    }

    public void OnPressUpgrade_B1()
    {
        if (PlayerPrefs.GetInt(PlayerValues.MONEY) == 2 && PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB) == 1 && PlayerPrefs.GetInt(PlayerValues.STICK) == 1)
        {
            SubtractMoneyAndMaterials(2, 1, 1);
            if (B1_Evolution.Count - 1 > b1Index)
            {
                if (Building_Limit1 - 1 > b1Index)
                {
                    B1_Evolution[b1Index].SetActive(false);
                    b1Index++;
                    B1_Evolution[b1Index].SetActive(true);
                }
            }
        }
        CheckAllUpgrade();
    }

    public void OnPressUpgrade_B2()
    {
        if (PlayerPrefs.GetInt(PlayerValues.MONEY) == 2 && PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB) == 1 && PlayerPrefs.GetInt(PlayerValues.STICK) == 1)
        {
            SubtractMoneyAndMaterials(2, 1, 1);
            if (B2_Evolution.Count - 1 > b2Index)
            {
                if (Building_Limit2 - 1 > b2Index)
                {
                    B2_Evolution[b2Index].SetActive(false);
                    b2Index++;
                    B2_Evolution[b2Index].SetActive(true);
                }
            }
        }

        CheckAllUpgrade();
    }

    public void OnPressUpgrade_B3()
    {
        if (PlayerPrefs.GetInt(PlayerValues.MONEY) == 2 && PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB) == 1 && PlayerPrefs.GetInt(PlayerValues.STICK) == 1)
        {
            SubtractMoneyAndMaterials(2, 1, 1);
            if (B3_Evolution.Count - 1 > b3Index)
            {
                if (Building_Limit3 - 1 > b3Index)
                {
                    B3_Evolution[b3Index].SetActive(false);
                    b3Index++;
                    B3_Evolution[b3Index].SetActive(true);
                }
            }
        }

        CheckAllUpgrade();
    }

    public void OnPressUpgrade_B4()
    {
        if (PlayerPrefs.GetInt(PlayerValues.MONEY) == 2 && PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB) == 1 && PlayerPrefs.GetInt(PlayerValues.STICK) == 1)
        {
            SubtractMoneyAndMaterials(2, 1, 1);
            if (B4_Evolution.Count - 1 > b4Index)
            {
                if (Building_Limit4 - 1 > b4Index)
                {
                    B4_Evolution[b4Index].SetActive(false);
                    b4Index++;
                    B4_Evolution[b4Index].SetActive(true);
                }
            }
        }
        CheckAllUpgrade();
    }

    public void OnPressUpgrade_B5()
    {
        if (PlayerPrefs.GetInt(PlayerValues.MONEY) == 2 && PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB) == 1 && PlayerPrefs.GetInt(PlayerValues.STICK) == 1)
        {
            SubtractMoneyAndMaterials(2, 1, 1);
            if (B5_Evolution.Count - 1 > b5Index)
            {
                if (Building_Limit5 - 1 > b5Index)
                {
                    B5_Evolution[b5Index].SetActive(false);
                    b5Index++;
                    B5_Evolution[b5Index].SetActive(true);
                }
            }
        }

        CheckAllUpgrade();
    }



    private void InitUpgradeData()
    {
        B1_Evolution = new List<GameObject>();
        B1_Evolution.Add(B1_defUprage_1);
        B1_Evolution.Add(B1_EvUprage_1);
        B1_Evolution.Add(B1_EvUprage_2);

        B2_Evolution = new List<GameObject>();
        B2_Evolution.Add(B2_defUprage_1);
        B2_Evolution.Add(B2_EvUprage_1);
        B2_Evolution.Add(B2_EvUprage_2);

        B3_Evolution = new List<GameObject>();
        B3_Evolution.Add(B3_defUprage_1);
        B3_Evolution.Add(B3_EvUprage_1);
        B3_Evolution.Add(B3_EvUprage_2);

        B4_Evolution = new List<GameObject>();
        B4_Evolution.Add(B4_defUprage_1);
        B4_Evolution.Add(B4_EvUprage_1);
        B4_Evolution.Add(B4_EvUprage_2);

        B5_Evolution = new List<GameObject>();
        B5_Evolution.Add(B5_defUprage_1);
        B5_Evolution.Add(B5_EvUprage_1);
        B5_Evolution.Add(B5_EvUprage_2);

        //Activate Only the current active bldg

        B1_Evolution[b1Index].SetActive(true);
        B2_Evolution[b2Index].SetActive(true);
        B3_Evolution[b3Index].SetActive(true);
        B4_Evolution[b4Index].SetActive(true);
        B5_Evolution[b5Index].SetActive(true);
    }

    private void CheckAllUpgrade()
    {
        int maxUpgradeScore = Building_Limit1 + Building_Limit2 + Building_Limit3 +
            Building_Limit4 + Building_Limit5;


        int currUpgradeScore = b1Index + b2Index + b3Index + b4Index + b5Index + 5;

        if (maxUpgradeScore == currUpgradeScore)
            endTab.SetActive(true);
    }
}
