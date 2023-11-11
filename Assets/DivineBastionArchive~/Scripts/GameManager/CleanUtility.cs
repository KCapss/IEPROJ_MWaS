using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUtility : MonoBehaviour
{
    [SerializeField] private Pathfinding targetPF;
    [SerializeField] private GridHighlight moveHighlightObject;
    [SerializeField] private GridHighlight attkHighlightObject;

    public void ClearPathFinding()
    {
        targetPF.Clear();
    }

    public void ClearGridHighlightAttack()
    {
        attkHighlightObject.Hide();
    }

    public void ClearGridHighlightMove()
    {
        moveHighlightObject.Hide();
    }

    public void ClearAll()
    {
        targetPF.Clear();
        attkHighlightObject.Hide();
        moveHighlightObject.Hide();
    }


}
