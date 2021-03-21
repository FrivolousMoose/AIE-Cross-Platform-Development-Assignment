using UnityEngine;
/// A type of building that provides an effect in a radius
public abstract class RadialBuilding : Building
{
    /// The distance of the building's effect per level
    public int[] distance;


    protected override void Start()
    {
        base.Start();
        ProvideRadial();
    }
    // Start is called before the first frame update
    void ProvideRadial()
    {
        //Finds the number of squares distance * 2 + 1
        int diameter = distance[upgradeLevel] * 2 + 1;
        int size = diameter * diameter;
        for (int i = 0; i < size; ++i)
        {
            int currentColumn = (index % 10 - distance[upgradeLevel]) + (i % diameter);
            int currentRow = (index / 10 - distance[upgradeLevel]) + (i / diameter);
            if (currentRow > 0 && currentRow < 10 && currentColumn > 0 && currentColumn < 10)
            {
                int currentIndex = currentRow * 10 + currentColumn;

                GiveEffect(GameManager.instance.map[currentIndex]);
            }

        }
    }

    protected abstract void GiveEffect(Building building);

    public override void HandleChange()
    {
        base.HandleChange();
        ProvideRadial();
    }
}
