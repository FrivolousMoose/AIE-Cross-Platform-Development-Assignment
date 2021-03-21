using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Class that determines the behaviour of a slider that determines the amount of zombies to move to another location
public class MoveZombiesSlider : MonoBehaviour
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

    private void OnGUI()
    {
        slider.maxValue = selectedBuilding?.zombies ?? 0;
        textDisplay.text = slider.value.ToString();
    }

    /// Behaviour for when connected button is pressed
    public void OnButtonPress()
    {
        GameManager.instance.selectedItem.GetComponent<ZombieHousing>().toMove = (int)slider.value;
        GameManager.instance.selectionState = 2;
        GameManager.instance.menu.UpdateUI();
    }
}
