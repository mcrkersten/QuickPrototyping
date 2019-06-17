using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFenceCloser : EquipmentFunction {
    public GameObject fence;
    private bool move = false;
    // Start is called before the first frame update
    public void Update() {
        if (move) {
            fence.transform.position = new Vector3(fence.transform.position.x + .1f,
                fence.transform.position.y,
                fence.transform.position.z);
        }
    }

    public override void ActivateEquipmentTouchFunction(bool down) {
        move = down;
        Debug.Log("press");
    }
}
