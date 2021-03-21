using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Class that determines the behaviour of a slider that controls the amount of living to convert to zombies
public class ConvertLivingSlider : MonoBehaviour
{

    private Town selectedBuilding;
    public Slider slider;
    public Slider progressBar;
    private Text textDisplay;


    void Awake()
    {
        textDisplay = GetComponentInChildren<Text>();
    }

    void OnEnable()
    {
        selectedBuilding = GameManager.instance?.selectedItem.GetComponent<Town>();
        slider.value = 0;
    }

    void OnGUI()
    {
        slider.maxValue = (selectedBuilding.zombieCapacity[selectedBuilding.upgradeLevel] - selectedBuilding.zombies) < selectedBuilding.living - 1 ? (selectedBuilding.zombieCapacity[selectedBuilding.upgradeLevel] - selectedBuilding.zombies) : selectedBuilding.living;
        textDisplay.text = slider.value.ToString();
        progressBar.value = selectedBuilding.convertTimer;
        
    }

    /// Behaviour for when connected button is pressed
    public void OnButtonPress()
    {
        int convertAmount = (int)slider.value;
        if (convertAmount + selectedBuilding.zombies <= selectedBuilding.zombieCapacity[selectedBuilding.upgradeLevel] && selectedBuilding.toConvert == 0)
        {
            selectedBuilding.toConvert = convertAmount;
            slider.value = 0;
        }
    }
}
