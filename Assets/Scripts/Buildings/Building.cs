using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle behaviours for all placed buildings
/// </summary>
public class Building : MonoBehaviour
{
    /// Upgrade level of the building
    public int upgradeLevel;
    /// Array of costs to upgrade to the next level, with the index being the upgrade level
    public int[] upgradeCost;
    /// cost to buy this building 
    public int buyCost;

    public int moveCost;

    protected int index;


    public delegate void BuildingChanged();

    public static event BuildingChanged BuildingEvent;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GameManager.instance.TimeEvent += BuildingTurn;
        BuildingEvent += HandleChange;
        index = Array.IndexOf(GameManager.instance.map, this);
        BuildingEvent?.Invoke();
    }


    /// Function that handles building behaviour when upgraded to the next level
    public virtual void Upgrade()
    {
        GameManager.instance.ghould -= upgradeCost[upgradeLevel];
        upgradeLevel++;
        BuildingEvent?.Invoke();
    }

    /// Function that prints relevant info to menus and UIs
    public virtual void PrintInfo(UIManager canvas)
    {
        canvas.title.text = "level " + (upgradeLevel + 1) + " " + GetType();
        canvas.upgradeCostText.text = upgradeLevel < upgradeCost.Length ? upgradeCost[upgradeLevel].ToString() : "Max";
        canvas.moveCost.text = moveCost + " Ghould";
    }

    /// Handles move functionality of buildings
    public virtual void Move(int destination)
    {
        GameManager manager = GameManager.instance;

        manager.ghould -= moveCost;
        manager.map[destination] = this;
        transform.position = new Vector3((int)(destination % 10) * 10 + 5, 0, (int)(destination / 10) * 10 + 5);
        manager.map[index] = null;
        index = destination;
        BuildingEvent?.Invoke();
    }

    /// Handles events that happen when the "turn" moves forward in the game
    public virtual void BuildingTurn()
    {

    }

    ///Handles event that happens when a building is changed
    public virtual void HandleChange()
    {

    }
}
