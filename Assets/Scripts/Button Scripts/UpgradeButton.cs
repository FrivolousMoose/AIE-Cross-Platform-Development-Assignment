using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Class to determine upgrade button behaviour
public class UpgradeButton : MonoBehaviour
{
    public void OnPress()
    {
        GameManager manager = GameManager.instance;
        Building selectedBuilding = manager.selectedItem.GetComponent<Building>();


        if (selectedBuilding.upgradeLevel < selectedBuilding.upgradeCost.Length && selectedBuilding.upgradeCost[selectedBuilding.upgradeLevel] <= manager.ghould)
        {
            selectedBuilding.Upgrade();
            manager.menu.UpdateUI();
        }
    }
}
