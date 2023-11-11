using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Hotkeys : MonoBehaviour
{
    SelectCharacter select;
    CleanUtility cleanUtility;
    CommandMenu commandMenu;

    private void Awake()
    {
       select = GetComponent<SelectCharacter>();
       cleanUtility = GetComponent<CleanUtility>();
       commandMenu = GetComponent<CommandMenu>();

       EventBroadcaster.Instance.AddObserver(EventNames.Hotkeys.DESELECT_CHARACTER, this.DeselectCharacter);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.Hotkeys.DESELECT_CHARACTER);
    }

    private void DeselectCharacter()
    {
        commandMenu.CancelCommandSelected();
        cleanUtility.ClearAll();
    }
}
