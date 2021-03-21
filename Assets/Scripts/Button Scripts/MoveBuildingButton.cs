using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Class to determine move building button behaviour
public class MoveBuildingButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnPress()
    {
        //GameManager.instance.menu.CloseMenu();
        GameManager.instance.selectionState = 1;
    }
}
