using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private List<Sprite> backgrounds;
    [SerializeField] private SpriteRenderer backgroundRenderer;

    private void OnEnable()
    {
        Levels currentLevel = EnemyLibrary.Instance.GetCurrentLevel();

        switch(currentLevel) 
        {
            case Levels.TUTORIAL: backgroundRenderer.sprite = backgrounds[0]; break;
            case Levels.LEVEL_1: backgroundRenderer.sprite = backgrounds[1]; break;
            case Levels.LEVEL_2: backgroundRenderer.sprite = backgrounds[2]; break;
            case Levels.LEVEL_3: backgroundRenderer.sprite = backgrounds[3]; break;
            case Levels.LEVEL_4: backgroundRenderer.sprite = backgrounds[4]; break;
        }
    }
}
