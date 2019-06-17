using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class EquipmentSystem : MonoBehaviour
{
    private static EquipmentSystem instance = null;
    public static EquipmentSystem Instance
    {
        get {
            if (instance == null) {
                // This is where the magic happens.
                instance = FindObjectOfType(typeof(EquipmentSystem)) as EquipmentSystem;
            }

            // If it is still null, create a new instance
            if (instance == null) {
                GameObject i = new GameObject("Body");
                i.AddComponent(typeof(EquipmentSystem));
                instance = i.GetComponent<EquipmentSystem>();
            }
            return instance;
        }
    }

    public EquipmentObject LeftHandItem { private set; get; }
    public EquipmentObject RightHandItem { private set; get; }
    public EquipmentObject HeadItem { private set; get; }


    /// <summary>Attach the equipment to the controller</summary>
    /// <param name="fixRotation">Fix rotation to the eurlerRotation of EquipmentObject </param>
    /// <param name="isAddOn">If the equipment is placed on a other object, eg; Gun</param>
    public void AttachEquitmentOnController(EquipmentObject equipment, ControllerObject theController, bool fixRotation = false, bool isAddOn = false) {
        //Release object from holding controller
        if (equipment.gameObject.transform.parent != null) {
            ControllerObject parentController = equipment.transform.parent.gameObject.GetComponent<ControllerObject>();
            parentController.ReleaseEquipment();
        }

        equipment.transform.parent = theController.transform;
        equipment.transform.position = theController.snapTransform.transform.position;
        theController.equipmentSlot = equipment;

        if (fixRotation) {
            equipment.transform.localEulerAngles = equipment.eurlerRotation;
        }

        //Set controller to invisable
        if (!isAddOn) {
            theController.snapTransform.SetActive(false);
        }

        SetEquipmentInSystem(theController, equipment);
        equipment.OnAttach();
    }

    /// <summary>Release the Equipment on the given controller</summary>
    public void ReleaseEquitmentFromController(EquipmentObject equipment, ControllerObject theController) {
        equipment.transform.parent = null;
        theController.equipmentSlot = null;

        //Set controller to visable
        theController.snapTransform.SetActive(true);

        RemoveEquipmentFromSystem(theController);
        equipment.OnRelease();
    }

    /// <summary>Set the equipment in the System</summary>
    private void SetEquipmentInSystem(ControllerObject controller, EquipmentObject equipmentObject) {
        switch (controller.controllerType) {
            case SteamVR_Input_Sources.LeftHand:
                LeftHandItem = equipmentObject;
                break;
            case SteamVR_Input_Sources.RightHand:
                RightHandItem = equipmentObject;
                break;
            case SteamVR_Input_Sources.Head:
                HeadItem = equipmentObject;
                break;
            default:
                //Nothing
                break;
        }
    }

    /// <summary>Release the equipment from the System</summary>
    private void RemoveEquipmentFromSystem(ControllerObject controller) {
        switch (controller.controllerType) {
            case SteamVR_Input_Sources.LeftHand:
                LeftHandItem = null;
                break;
            case SteamVR_Input_Sources.RightHand:
                RightHandItem = null;
                break;
            case SteamVR_Input_Sources.Head:
                HeadItem = null;
                break;
            default:
                //Nothing
                break;
        }
    }
}
