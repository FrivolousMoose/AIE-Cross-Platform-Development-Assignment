using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// Class that determines the behaviour of a slider that controls the amount of zombies to sell
public class SellZombiesSlider : MonoBehaviour
{

    private ZombieHousing selectedBuilding;
    private Slider slider;
    private Text textDisplay;


    void Awake()
    {
        slider = GetComponent<Slider>();
        textDisplay = GetComponentInChildren<Text>();
    }

    void OnEnable()
    {
        selectedBuilding = GameManager.instance?.selectedItem.GetComponent<ZombieHousing>();
        slider.value = 0;
    }

    void OnGUI()
    {
        slider.maxValue = selectedBuilding?.zombies ?? 0;
        textDisplay.text = slider.value.ToString();
    }

    public void OnButtonPress()
    {
        int sellAmount = (int)slider.value;
        {
            GameManager manager = GameManager.instance;

            selectedBuilding.zombies -= sellAmount;
            manager.ghould += (int)(sellAmount * manager.sellPrice);
            manager.menu.UpdateUI();
            slider.value = 0;
        }
    }
}
