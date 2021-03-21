using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Main building in the game. Contains zombies and living
public class Town : ZombieHousing
{
    /// Number of living in a town
    public int living;
    /// The total capacity for living at the town's current level
    public int[] livingCapacity;

    /// The amount by which the living population contributes to the overall decay of zombie populations
    public float livingDecayBonus;

    /// Multiplier for living effect provided by nearby armories
    public float livingDecayMultiplier;

    /// The amount by which the living population grows per turn
    public float livingGrowthRate;

    public float growthMultiplier;

    /// How many living are being converted
    public int toConvert = 0;

    /// How far along the conversion timer is on a scale of 0 to 1;
    public float convertTimer;

    public new int zombieDecay
    {
        get { return Mathf.CeilToInt(zombies * GameManager.instance.decayRate + living * livingDecayBonus * livingDecayMultiplier); }
    }
    /// The change in living population by the next turn
    public int livingGrowth
    {
        get { return Mathf.Clamp(Mathf.CeilToInt(living * livingGrowthRate * growthMultiplier), 0, livingCapacity[upgradeLevel] - living); }
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        if (toConvert > 0)
        {
            convertTimer += Time.deltaTime / toConvert * 5;
            if (convertTimer >= 1)
            {
                if (toConvert + zombies <= zombieCapacity[upgradeLevel])
                {
                    int maxConvert = zombieCapacity[upgradeLevel] - zombies;
                    zombies += Mathf.Clamp(toConvert,0, maxConvert);
                    living -= Mathf.Clamp(toConvert, 0, maxConvert);
                    toConvert = 0;
                    convertTimer = 0;
                    GameManager.instance.menu.UpdateUI();
                }
            }
        }
    }

    public override void PrintInfo(UIManager canvas)
    {

        base.PrintInfo(canvas);
        canvas.livingCount.text = "Living:" + living + "/" + livingCapacity[upgradeLevel] + " (+" + livingGrowth + ")";
    }

    //public override void UpdatePopulation()
    //{
    //    base.UpdatePopulation();
    //    living += livingGrowth;
    //}

    public override void BuildingTurn()
    {
        base.BuildingTurn();
        living += livingGrowth;
    }

    public override void HandleChange()
    {
        base.HandleChange();

        livingDecayMultiplier = 1;
        growthMultiplier = 1;
    }
}
