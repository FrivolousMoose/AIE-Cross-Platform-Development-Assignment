using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// Game Manager which controls universal functions and variables
public class GameManager : MonoBehaviour
{

    /// The player's total ghould, the main currency of the game
    public int ghould = 20000;
    /// The amount of time that has passed since the last turn
    public float time = 0;

    /// The game's menu
    public UIManager menu;
    /// The selection square that appears when a tile is selected
    public GameObject selectionHighlight;
    public GameObject radialHighlight;

    /// Variable that determines the behaviour when clicking/tapping on a tile 
    public int selectionState;

    /// Town prefab
    public GameObject town;
    /// Crypt prefab
    public GameObject crypt;

    public Building[] map = new Building[100];
    private int selectedSquare;

    ///The sell price of zombies
    public float sellPrice = 100;

    ///The base decay rate for all zombies
    public float decayRate;

    /// Returns the building on the currently selected tile
    public Building selectedItem
    {
        get { return map[selectedSquare]; }
    }

    private static GameManager manager = null;
    /// Instance of GameManager singleton
    public static GameManager instance
    {
        get { return manager; }
    }

    // Start is called before the first frame update
    void Awake()
    {

        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Debug.LogError("There is another manager already" + gameObject.name);
            Destroy(this);
        }

        PlaceBuilding(town, Random.Range(0, 33));
        PlaceBuilding(town, Random.Range(34, 66));
        PlaceBuilding(town, Random.Range(67, 99));
        PlaceBuilding(crypt, Random.Range(34, 66));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                int selectionIndex = ((int)(hit.point.x / 10) + (int)(hit.point.z / 10) * 10);

                switch (selectionState)
                {
                    case 1:
                        if (!map[selectionIndex])
                        {
                            manager.selectedItem.GetComponent<Building>().Move(selectionIndex);

                            menu.CloseMenu();
                            selectionHighlight.SetActive(false);
                            selectionState = 0;
                        }
                        break;
                    case 2:
                        if (map[selectionIndex])
                        {
                            ZombieHousing moveTo = map[selectionIndex].GetComponent<ZombieHousing>();
                            ZombieHousing moveFrom = selectedItem.GetComponent<ZombieHousing>(); ;
                            moveTo.zombies += moveFrom.toMove;
                            moveFrom.zombies -= moveFrom.toMove;
                            selectionState = 0;
                        }
                        break;
                    default:
                        selectedSquare = selectionIndex;
                        selectionHighlight.SetActive(true);
                        selectionHighlight.transform.position = new Vector3((int)(selectedSquare % 10) * 10 + 5, 0.1f, (int)(selectedSquare / 10) * 10 + 5);
                        menu.SelectMenu(map[selectedSquare]);
                        break;

                }
                menu.UpdateUI();
            }

        }

        time += Time.deltaTime;

        if (time > 10)
        {
            TimeEvent?.Invoke();
            menu.UpdateUI();
            time = 0;
        }

    }

    /// Function that places a building on the currently selected square
    public void PlaceBuilding(GameObject building)
    {
        if (map[selectedSquare] == null)
        {
            map[selectedSquare] = Instantiate(building, new Vector3((int)(selectedSquare % 10) * 10 + 5, 0, (int)(selectedSquare / 10) * 10 + 5), Quaternion.identity).GetComponent<Building>();
        }
    }

    ///Override of the PlaceBuilding function that places a building on a square specified by the index
    public void PlaceBuilding(GameObject building, int index)
    {

        if (map[index] == null)
        {
            map[index] = Instantiate(building, new Vector3((int)(index % 10) * 10 + 5, 0, (int)(index / 10) * 10 + 5), Quaternion.identity).GetComponent<Building>();
        }
    }

    public void GetRadius()
    {
        RadialBuilding radialBuilding = GameManager.instance.selectedItem?.GetComponent<RadialBuilding>();
        if (radialBuilding)
        {
            int diameter = radialBuilding.distance[radialBuilding.upgradeLevel] * 2 + 1;
            radialHighlight.SetActive(true);
            radialHighlight.transform.position = new Vector3(radialBuilding.transform.position.x, 0.1f, radialBuilding.transform.position.z);
            radialHighlight.transform.localScale = new Vector3(diameter, 1, diameter);
        }
        else radialHighlight.SetActive(false);
    }

    public delegate void TimeForward();

    public event TimeForward TimeEvent;


    public delegate void BuildingChanged();

    public event BuildingChanged BuildingEvent;

}
