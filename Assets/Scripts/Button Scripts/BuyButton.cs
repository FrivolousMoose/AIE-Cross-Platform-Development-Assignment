using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// Class to determine the behaviour of the buy button
public class BuyButton : MonoBehaviour
{
    /// Building prefab to be bought
    public Building building;
    /// Text display of the building's price
    public Text costText;
    // Start is called before the first frame update
    void Start()
    {
        costText.text = building.buyCost + " Ghould";
    }

    /// Behaviour when button is clicked and building is bought. Places down appropriate building and deducts cost from the player
    public void BuyBuilding()
    {
        GameManager.instance.PlaceBuilding(building.gameObject);
        GameManager.instance.ghould -= building.buyCost;
        GameManager.instance.menu.UpdateUI();
        GameManager.instance.menu.CloseMenu();
    }
}
