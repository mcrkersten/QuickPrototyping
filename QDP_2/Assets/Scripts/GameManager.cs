using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get {
            if (instance == null) {
                // This is where the magic happens.
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            // If it is still null, create a new instance
            if (instance == null) {
                GameObject i = new GameObject("Ship");
                i.AddComponent(typeof(GameManager));
                instance = i.GetComponent<GameManager>();
            }
            return instance;
        }
    }

    public delegate void RemoveDoor(int numberOfDoor, int round);
    public static event RemoveDoor OnRemoveDoor;

    public delegate void SelectDoor(int numberOfDoor);
    public static event SelectDoor OnDoorSelection;

    public GameObject wall;
    private List<GameObject> wallList = new List<GameObject>();
    public int wallDistance;
    private Vector3 newWallosition;

    private int correctDoor;
    private int selectedDoor;
    private int round;

    public TextMeshPro text;

    public GameObject resetButton;

    private void Start() {
        CreateNewWall();
        OnDoorClick.OnDoorClickEvent += DoorSelect;
        Button.OnNextRound += NextRound;
    }

    private void OnDestroy() {
        OnDoorClick.OnDoorClickEvent -= DoorSelect;
    }

    private void NextRound() {
        if(round == 0) {
            text.text = "Want to switch doors?";
            int doorToDestroy = Random.Range(1, 4);
            while (doorToDestroy == selectedDoor || doorToDestroy == correctDoor) {
                doorToDestroy = Random.Range(1, 4);
            }
            OnRemoveDoor?.Invoke(doorToDestroy, round);
            round++;
        }
        else if(round == 1) {
            if(correctDoor == selectedDoor) {
                OnRemoveDoor?.Invoke(correctDoor, round);
                text.text = "Correct!";
                CreateNewWall();
                round = 0;
            }
            else {
                if (!resetButton.activeSelf) {
                    //resetButton.SetActive(true);
                }
                text.text = "You Foool!";
                return;
            }
        }
    }

    //Creates new "Puzzle" 
    private void CreateNewWall() {
        correctDoor = Random.Range(1, 4);
        newWallosition = new Vector3(newWallosition.x, newWallosition.y, newWallosition.z + wallDistance);
        GameObject newWall = Instantiate(wall, newWallosition, this.transform.rotation);
    }

    //Recieves number of selected door;
    private void DoorSelect(int number) {
        selectedDoor = number;
        OnDoorSelection?.Invoke(selectedDoor);
    }
}
