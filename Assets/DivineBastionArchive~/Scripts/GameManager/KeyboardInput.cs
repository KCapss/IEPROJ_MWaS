using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{ 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventBroadcaster.Instance.PostEvent(EventNames.Hotkeys.DESELECT_CHARACTER);
        }
    }
}
