using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using UnityEditor;
using Valve.VR.InteractionSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EquipmentObject : MonoBehaviour
{
    public ObjectGrabTypes grabType;
    public SteamVR_Input_Sources positionPurpose;
    public WeaponType weaponType;
    public List<Collider> colliders = new List<Collider>();


    [Header("Fixed rotation")]
    [Tooltip("Select if you want to use fixed rotation")]
    public bool fixRotation;
    [Tooltip("Rotation in euler angles")]
    public Vector3 eurlerRotation;

    private Rigidbody rb;
    private Vector3 currentTransform;
    private Vector3 lastTransform;

    [HideInInspector]
    public EquipmentFunction function;

    private void Start() {
        gameObject.tag = "EquipmentObject";
        rb = this.GetComponent<Rigidbody>();
        function = this.GetComponent<EquipmentFunction>();
    }

    public void OnAttach() {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.isKinematic = true;

        //Turn intersecting colliders to triggers
        foreach (Collider x in colliders) {
            x.isTrigger = true;
        }
    }

    /// <summary>Remove all RigidbodyConstraints and calculate a throw of the EquipmentObject</summary>
    public void OnRelease() {
        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;
        ThrowEquipment();

        foreach (Collider x in colliders) {
            x.isTrigger = false;
        }
    }

    private void ThrowEquipment() {
        float velocity = ((lastTransform - currentTransform).magnitude) / Time.deltaTime;
        Vector3 throwVector = currentTransform - lastTransform;
        rb.AddForce(throwVector * (velocity * 15), ForceMode.Impulse);
    }

    private void FixedUpdate() {
        lastTransform = currentTransform;
        currentTransform = this.transform.position;
    }

    #region Functions
    public void DoTouchFunction(bool down) {
        if (function) { function.ActivateEquipmentTouchFunction(down); }
    }

    public void DoPinchFunction(bool down) {
        if (function) { function.ActivateEquipmenPinchtFunction(down); }
    }

    public T GetData<T>(T param) {
        return function.GetData<T>(param);
    }

    public void SetData<T>(T param) {
        function.SetData<T>(param);
    }
    #endregion
}
