using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControllerObject : MonoBehaviour
{
    public SteamVR_Input_Sources controllerType;
    public WeaponType weaponType = WeaponType.NOWEAPON;
    public EquipmentObject equipmentSlot;
    public GameObject snapTransform;

    private EquipmentObject equipmentInRange;

    //For Controller-input
    private SteamVR_Action_Boolean grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    private SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    private SteamVR_Action_Boolean touchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Action");

    //For Wearables to prevent resnapping
    private bool locked = false;
    private float minDistance = .15f;
    private Transform lastTransform;

    private void Start() {
        //To be sure that correct controller is set.
        if(this.gameObject.GetComponent<SteamVR_Behaviour_Pose>() != null)
        controllerType = this.gameObject.GetComponent<SteamVR_Behaviour_Pose>().inputSource;
        else {
            //this is not a real controller, we do not need to use the Update to catch button-input
            enabled = false;
        }
    }

    private void Update() {
        //For wearables | Re-snap prevention
        if (locked) {
            if(Vector3.Distance(this.transform.position, lastTransform.position) > minDistance) {
                locked = false;
                enabled = true; //Shut-down re-snap prevention
            }
        }

        //If Grip is pressed
        if (grabGripAction.GetStateDown(controllerType)) {
            if (equipmentInRange != null && equipmentSlot == null) {
                AttachEquipment();
                return;
            }
            if (equipmentSlot != null && equipmentSlot.grabType == ObjectGrabTypes.GRABLOCK) {
                ReleaseEquipment();
                return;
            }
        }

        //If Grip is released
        if (grabGripAction.GetStateUp(controllerType)) {
            if (equipmentSlot != null && equipmentSlot.grabType != ObjectGrabTypes.GRABLOCK) {
                ReleaseEquipment();
            }
        }

        if(equipmentSlot != null) {
            //If Touch is pressed
            if (touchAction.GetStateDown(controllerType)) {
                equipmentSlot.DoTouchFunction(true);
            }
            //If Touch is released
            if (touchAction.GetStateUp(controllerType)) {
                equipmentSlot.DoTouchFunction(false);
            }

            //If Pinched the trigger
            if (grabPinchAction.GetStateDown(controllerType)) {
                equipmentSlot.DoPinchFunction(true);
            }

            //If Released the trigger
            if (grabPinchAction.GetStateUp(controllerType)) {
                equipmentSlot.DoPinchFunction(false);
            }
        }
    }

    private void AttachEquipment() {
        EquipmentSystem.Instance.AttachEquitmentOnController(equipmentInRange, this, equipmentInRange.fixRotation);
    }

    /// <summary> Release object from controller </summary>
    public void ReleaseEquipment() {
        if(equipmentSlot != null) {
            enabled = true; //Activate update loop for re-snap prevention
            EquipmentSystem.Instance.ReleaseEquitmentFromController(equipmentSlot, this);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("EquipmentObject")) {

            //If this controller is a handController
            if (controllerType == SteamVR_Input_Sources.LeftHand || controllerType == SteamVR_Input_Sources.RightHand) {
                //if the equipment is interactable
                if (other.GetComponent<EquipmentObject>().grabType == ObjectGrabTypes.INTERACT && equipmentSlot == null) {
                    equipmentSlot = other.gameObject.GetComponent<EquipmentObject>();
                }
                else {
                    equipmentInRange = other.GetComponent<EquipmentObject>();
                }
            }

            else if (other.GetComponent<EquipmentObject>().grabType == ObjectGrabTypes.GRABHOLD && !locked) {
                //If it is the right type of controller and check if the object is not just laying around.
                if (controllerType == other.GetComponent<EquipmentObject>().positionPurpose && other.transform.parent != null) {
                    if(other.GetComponent<EquipmentObject>().weaponType == WeaponType.NOWEAPON) {
                        AttachEquipmentToController(other.gameObject);
                    } else if(weaponType == other.gameObject.GetComponent<EquipmentObject>().weaponType){
                        AttachEquipmentToController(other.gameObject);
                    }
                }
            }
        }
    }

    public void AttachEquipmentToController(GameObject equipment) {
        equipmentInRange = equipment.GetComponent<EquipmentObject>();
        EquipmentSystem.Instance.AttachEquitmentOnController(equipmentInRange, this, true, true);
        lastTransform = equipment.transform;
        locked = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("EquipmentObject")) {
            equipmentInRange = null;
        }
        if(other.GetComponent<EquipmentObject>() != null && equipmentSlot != null) {
            if(other.GetComponent<EquipmentObject>().grabType == ObjectGrabTypes.INTERACT) {
                equipmentSlot.DoTouchFunction(false); // < make sure function = off
                equipmentSlot = null;
            }
        }
    }
}
