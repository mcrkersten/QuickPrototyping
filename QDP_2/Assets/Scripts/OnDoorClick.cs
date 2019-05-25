using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDoorClick : MonoBehaviour
{
    public int doorNumber;

    public delegate void ChooseDoor(int numberOfDoor);
    public static event ChooseDoor OnDoorClickEvent;

    public delegate void MoveCamera(Vector3 position);
    public static event MoveCamera OnMoveCamera;

    public GameObject Door;
    public GameObject Wall;
    public GameObject pickDoorText;

    public Material selectedMaterial;
    private Material oldMaterial;
    private Material[] materialsOnDoor;

    private void Start() {
        materialsOnDoor = Door.GetComponent<Renderer>().materials;
        oldMaterial = materialsOnDoor[0];

        GameManager.OnRemoveDoor += RemoveDoor;
        GameManager.OnDoorSelection += SelectDoor;
    }

    private void OnDestroy() {
        GameManager.OnRemoveDoor -= RemoveDoor;
        GameManager.OnDoorSelection -= SelectDoor;
    }

    private void OnMouseOver() {
        if (Input.GetMouseButton(0)) {
            OnDoorClickEvent?.Invoke(doorNumber);
        }
    }

    private void SelectDoor(int number) {
        if(number == doorNumber) {
            pickDoorText.SetActive(true);
            materialsOnDoor[0] = selectedMaterial;
            Door.GetComponent<Renderer>().materials = materialsOnDoor;
        }
        else {
            materialsOnDoor[0] = oldMaterial;
            Door.GetComponent<Renderer>().materials = materialsOnDoor;
        }
    }

    private void RemoveDoor(int number, int round) {
        if(number == doorNumber && round == 0) {
            Door.gameObject.SetActive(false);
            this.gameObject.GetComponent<Collider>().enabled = false;
            Instantiate(Wall, this.gameObject.transform);
        }
        else if(number == doorNumber && round == 1) {
            OnMoveCamera?.Invoke(this.transform.position);
            Door.gameObject.SetActive(false);
            Destroy(this);
        }else if(round == 1) {
            Destroy(this);
        }
    }
}
