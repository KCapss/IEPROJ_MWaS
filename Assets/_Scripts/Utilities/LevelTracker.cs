using UnityEngine;


[CreateAssetMenuAttribute(menuName = "LevelTracker", fileName = "Save File")]
[System.Serializable]

public class LevelTracker : ScriptableObject
{
    [SerializeField] bool tutorialPassed;
    [SerializeField] bool level1Passed;
    [SerializeField] bool level2Passed;
    [SerializeField] bool level3Passed;
    [SerializeField] bool level4Passed;

    public void PassLevel(Levels levelType)
    {
        switch (levelType)
        {
            case Levels.TUTORIAL: tutorialPassed = true; break;
            case Levels.LEVEL_1: level1Passed = true; break;
            case Levels.LEVEL_2: level2Passed = true; break;
            case Levels.LEVEL_3: level3Passed = true; break;
            case Levels.LEVEL_4: level4Passed = true; break;
        }
    }

    public Levels GetCurrentLevel()
    {
        if(level4Passed) { return Levels.LEVEL_4; }
        else if(level3Passed) { return Levels.LEVEL_4; }
        else if(level2Passed) { return Levels.LEVEL_3; }
        else if(level1Passed) { return Levels.LEVEL_2; }
        else if(tutorialPassed) { return Levels.LEVEL_1; }
        return Levels.TUTORIAL;
    }
}
