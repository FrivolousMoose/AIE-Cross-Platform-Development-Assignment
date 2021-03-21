using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// Class for controlling UI elements in the game, such as text displays and menus
public class UIManager : MonoBehaviour
{

    public Building target;

    /// game object that contains the menu for general buildings
    public GameObject buildingMenu;
    /// game object that contains the menu for buildings that hold zombies
    public GameObject zombieStorageMenu;
    /// game object that contains the menu for buildings that hold zombies and living
    public GameObject townMenu;
    /// game object that contains the menu for buying buildings on empty squares
    public GameObject buyMenu;

    /// Title of the active menu
    public Text title;
    /// Text display of cost to upgrade a building
    public Text upgradeCostText;
    /// Text display of current ghould value
    public Text ghouldDisplay;
    /// Text display of total zombies held within all buildings
    public Text zombiesDisplay;
    /// Text display of zombies in selected building
    public Text buildingZombiesCount;
    /// Text display of living in selected building
    public Text livingCount;
    /// Text display of cost to move selected building
    public Text moveCost;
    /// Stat text for buildings which don't contain zombies
    public Text statText;
    /// Button to move zombies in selected building
    public Button moveZombiesButton;
    /// Button to sell zombies in selected building
    public Button sellZombiesButton;
    /// Button to convert living into zombies in selected building
    public Button convertLivingButton;

    public delegate void FindZombies(ref int start);

    public event FindZombies ZombieSumEvent;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
        CloseMenu();
    }

    /// Function that's called whenever the user interface needs to be updated
    public void UpdateUI()
    {
        title.text = "Buy";

        target?.PrintInfo(this);
        int totalZombies = 0;
        ZombieSumEvent?.Invoke(ref totalZombies);
        zombiesDisplay.text = "Zombies: " + totalZombies;
        ghouldDisplay.text = "Ghould: " + GameManager.instance.ghould;
        //zombiesDisplay.text = GameManager.

        GameManager.instance.GetRadius();
    }

    /// Function to select which menu should be activated
    public void SelectMenu(Building selected)
    {
        target = selected;
        GameManager.instance.selectionHighlight.SetActive(true);

        UpdateUI();

        gameObject.SetActive(true);

        buildingMenu.SetActive(false);
        buyMenu.SetActive(false);

        if (!target)
        {
            buyMenu.SetActive(true);
            return;
        }


        switch (target)
        {
            case Town town:
                buildingMenu.SetActive(true);
                zombieStorageMenu.SetActive(true);
                townMenu.SetActive(true);
                break;
            case ZombieHousing housing:
                buildingMenu.SetActive(true);
                zombieStorageMenu.SetActive(true);
                townMenu.SetActive(false);
                break;

            /* you can have any number of case statements */
            default: /* Optional */
                buildingMenu.SetActive(true);
                zombieStorageMenu.SetActive(false);
                townMenu.SetActive(false);
                break;
        }
    }


    /// Function to close all menus
    public void CloseMenu()
    {
        GameManager.instance.selectionHighlight.SetActive(false);
        buildingMenu.SetActive(false);
        buyMenu.SetActive(false);
        gameObject.SetActive(false);
    }
}
