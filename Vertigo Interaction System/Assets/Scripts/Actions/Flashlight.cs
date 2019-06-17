using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : EquipmentFunction {
    public GameObject spotlight;

    public override void ActivateEquipmentTouchFunction(bool down) {
        if (down) {
            ActivateLight();
        }
    }

    public void ActivateLight() {
        if (spotlight.activeSelf) {
            spotlight.SetActive(false);
        }
        else {
            spotlight.SetActive(true);
        }
    }
}
