using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Child class for all buildings that contain zombies
public class ZombieHousing : Building
{
    ///Number of zombies in the building
    public int zombies;

    ///Zombie capacity at each upgrade level
    public int[] zombieCapacity;

    public int toMove;

    ///How many zombies will be lost by the next turn
    public int zombieDecay
    {
        get { return Mathf.CeilToInt(zombies * GameManager.instance.decayRate); }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameManager.instance.menu.ZombieSumEvent += ZombieSum;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void PrintInfo(UIManager canvas)
    {
        base.PrintInfo(canvas);
        canvas.buildingZombiesCount.text = "Zombies: " + zombies + "/" + zombieCapacity[upgradeLevel] + " (-" + zombieDecay + ")";
    }

    public virtual void UpdatePopulation()
    {
        zombies -= zombieDecay;
    }

    public override void BuildingTurn()
    {
        base.BuildingTurn();
        if (zombies > 0) zombies -= zombieDecay;
    }

    public void ZombieSum(ref int startValue)
    {
        startValue += zombies;
    }


}
