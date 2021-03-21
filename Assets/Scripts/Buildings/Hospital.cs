using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : Building
{
    public float[] decayRate;

    protected override void Start()
    {
        base.Start();
        GameManager.instance.decayRate = decayRate[upgradeLevel];
    }

    public override void Upgrade()
    {
        base.Upgrade();
        GameManager.instance.decayRate = decayRate[upgradeLevel];
    }
}
