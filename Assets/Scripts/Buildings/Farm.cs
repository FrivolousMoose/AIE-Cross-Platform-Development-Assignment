using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : RadialBuilding
{

    public float[] growthMultiplier;
    protected override void GiveEffect(Building building)
    {
        Town town = building?.GetComponent<Town>();
        if (town)
        {
            town.growthMultiplier = growthMultiplier[upgradeLevel] > town.growthMultiplier ? growthMultiplier[upgradeLevel] : town.growthMultiplier;
        }

    }
}
