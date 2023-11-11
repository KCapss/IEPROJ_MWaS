using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [Header("Tutorial Steps Components")]
    [SerializeField] private List<GameObject> steps;
    [SerializeField] private bool isTutorialFinished = false;
    [SerializeField] private int incrementMeter = 0;

    private int stepsCount = 0;
    //private int maxIncrement = 1;



    // Start is called before the first frame update
    void Start()
    {
        
        int tutorialCount = PlayerPrefs.GetInt("tutorial");

        if (tutorialCount == 2)
            isTutorialFinished = true;

        else
            isTutorialFinished = false;

        CheckValidTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckValidTutorial()
    {
        if (!isTutorialFinished && steps.Count > stepsCount)
        {
            if (stepsCount == 0)
                steps[stepsCount].SetActive(true);

            else
            {
                steps[stepsCount - 1].SetActive(false);
                steps[stepsCount].SetActive(true);
            }

            
        }

        else if (!isTutorialFinished && steps.Count >= stepsCount)
        {
            steps[stepsCount - 1].SetActive(false);
            PlayerPrefs.SetInt("tutorial", 2);
        }

        else
        {
            PlayerPrefs.SetInt("tutorial", 2);
        }
    }

    public void IncrementSteps()
    {
        Debug.LogWarning($"Click Before: {stepsCount}");
        if (stepsCount == 0)
            steps[stepsCount].SetActive(false);

        else
            steps[stepsCount - 1].SetActive(false);

        stepsCount++;

        CheckValidTutorial();
        Debug.LogWarning($"Click: {stepsCount}");
        
    }

    public void IncrementClick()
    {
        if (!isTutorialFinished)
        {
            stepsCount++;
            CheckValidTutorial();
            StartCoroutine(NextDialogue());
        }

    }

    public void IncrementMeter()
    {
        incrementMeter++;
        if(incrementMeter == 4)
        {
            stepsCount++;
            CheckValidTutorial();
           
        }

        else if( incrementMeter == 1)
        {
            stepsCount++;
            CheckValidTutorial();
           
        }
    }

    public bool  CheckTutorialStatus()
    {
        int tutorialCount = PlayerPrefs.GetInt("tutorial");

        if (tutorialCount == 1)
            isTutorialFinished = true;

        return isTutorialFinished;
    }

    IEnumerator NextDialogue()
    {
        yield return new WaitForSeconds(5.0f);
        IncrementSteps();
    }
}
