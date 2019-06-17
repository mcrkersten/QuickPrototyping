using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagazineManualReload : EquipmentFunction {
    public int bullets;

    public override T GetData<T>(T param) {
        return (T) Convert.ChangeType(bullets, typeof(T));
    }

    public override void SetData<T>(T param) {
        bullets = Convert.ToInt32(param);
    }
}
