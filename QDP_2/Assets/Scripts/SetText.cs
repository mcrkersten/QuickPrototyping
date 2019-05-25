using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject yes;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.text = text;
        GameManager.Instance.resetButton = yes;
    }
}
