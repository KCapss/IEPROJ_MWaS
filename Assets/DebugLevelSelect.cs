using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLevelSelect : MonoBehaviour
{
    [SerializeField] private LevelTracker levelTracker;
    [SerializeField] private Dropdown dropdown;

    private void Start()
    {
        Levels currentLevel = levelTracker.GetCurrentLevel();

        switch (currentLevel)
        {
            case Levels.LEVEL_1:
                dropdown.value = 0;
                break;

            case Levels.LEVEL_2:
                dropdown.value = 1;
                break;

            case Levels.LEVEL_3:
                dropdown.value = 2;
                break;

            case Levels.LEVEL_4:
                dropdown.value = 3;
                break;
        }
    }

    public void OnLevelSelect(Int32 value)
    {
        switch(value)
        {
            case 3:
                dropdown.value = 3;
                levelTracker.SelectLevel(Levels.LEVEL_4);
                break;

            case 2:
                dropdown.value = 2;
                levelTracker.SelectLevel(Levels.LEVEL_3);
                break;

            case 1:
                dropdown.value = 1;
                levelTracker.SelectLevel(Levels.LEVEL_2);
                break;

            case 0:
                dropdown.value = 0;
                levelTracker.SelectLevel(Levels.LEVEL_1);
                break;
        }
    }
}
