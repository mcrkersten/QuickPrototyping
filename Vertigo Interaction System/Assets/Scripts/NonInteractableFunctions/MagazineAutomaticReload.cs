using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagazineAutomaticReload : EquipmentFunction {
    public int bullets;
    private EquipmentObject equipment;
    private Weapon weapon;
    ControllerObject controller;

    public override T GetData<T>(T param) {
        return (T) Convert.ChangeType(bullets, typeof(T));
    }

    public override void SetData<T>(T param) {
        bullets = Convert.ToInt32(param);
    }

    private void Start() {
        equipment = gameObject.GetComponent<EquipmentObject>();
    }

    private void Update() {
        if(this.transform.parent != null && this.transform.parent.GetComponent<ControllerObject>() != null) {
            controller = this.transform.parent.GetComponent<ControllerObject>();

            //Check controller hand and then check if other controller is a weapon and the right type.
            if (controller.controllerType == Valve.VR.SteamVR_Input_Sources.RightHand) {
                if (EquipmentSystem.Instance.LeftHandItem != null && EquipmentSystem.Instance.LeftHandItem.weaponType == equipment.weaponType) {
                    weapon = (Weapon)EquipmentSystem.Instance.LeftHandItem.function;
                      weapon.magazineSlot.ReleaseEquipment();
                    weapon.magazineSlot.AttachEquipmentToController(this.gameObject);
                }
            }

            if(controller.controllerType == Valve.VR.SteamVR_Input_Sources.LeftHand) {
                if (EquipmentSystem.Instance.RightHandItem != null && EquipmentSystem.Instance.RightHandItem.weaponType == equipment.weaponType) {
                    weapon = (Weapon)EquipmentSystem.Instance.RightHandItem.function;
                    weapon.magazineSlot.ReleaseEquipment();
                    weapon.magazineSlot.AttachEquipmentToController(this.gameObject);
                }
            }
        }

        //Disable this magazine
        if(bullets == 0) {
            weapon.magazineSlot.ReleaseEquipment();
            this.equipment.weaponType = WeaponType.NOWEAPON;
            this.enabled = false;
        }
    }
}
