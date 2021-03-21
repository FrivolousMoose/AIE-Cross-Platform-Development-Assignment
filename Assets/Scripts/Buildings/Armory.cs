using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Building type that reduces living effect on zombies in an area
public class Armory : RadialBuilding
{
    public float[] livingDecayMultiplier;
    protected override void GiveEffect(Building building)
    {
        Town town = building?.GetComponent<Town>();
        if (town)
        {
            town.livingDecayMultiplier = livingDecayMultiplier[upgradeLevel] > town.livingDecayMultiplier ? livingDecayMultiplier[upgradeLevel] : town.livingDecayMultiplier;
        }

    }
}
