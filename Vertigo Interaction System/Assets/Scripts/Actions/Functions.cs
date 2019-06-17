using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EquipmentObject))]
public abstract class EquipmentFunction : MonoBehaviour {
    public virtual void ActivateEquipmentTouchFunction(bool down) { }
    public virtual void ActivateEquipmenPinchtFunction(bool down) { }
    public virtual T GetData<T>(T param) { return param; }
    public virtual void SetData<T>(T param ){ }
}

