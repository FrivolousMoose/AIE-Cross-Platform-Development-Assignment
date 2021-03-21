using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : Building
{
    public float[] sellPrice;

    protected override void Start()
    {
        base.Start();
        GameManager.instance.sellPrice = sellPrice[upgradeLevel];
    }

    public override void Upgrade()
    {
        base.Upgrade();
        GameManager.instance.sellPrice = sellPrice[upgradeLevel];
    }
}
