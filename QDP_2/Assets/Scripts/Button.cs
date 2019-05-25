using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public delegate void NextRound();
    public static event NextRound OnNextRound;

    private void OnMouseOver() {
        if (Input.GetMouseButton(0)) {
            OnNextRound?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
