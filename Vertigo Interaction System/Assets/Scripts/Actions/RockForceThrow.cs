using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockForceThrow : EquipmentFunction
{
    public override void ActivateEquipmentTouchFunction(bool down) {
        ControllerObject x = this.transform.parent.GetComponent<ControllerObject>();
        x.ReleaseEquipment();
        this.GetComponent<Rigidbody>().AddForce(x.transform.forward * 10, ForceMode.Impulse);
    }

    public override void ActivateEquipmenPinchtFunction(bool down) {
        ControllerObject x = this.transform.parent.GetComponent<ControllerObject>();
        x.ReleaseEquipment();
        this.GetComponent<Rigidbody>().AddForce(x.transform.forward * 10, ForceMode.Impulse);
    }
}
