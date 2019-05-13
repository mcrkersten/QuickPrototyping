using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour {
    public float currentforce;
    public int maxForce;
    public int powerDoubler;
    public Rigidbody rb;
    public GameObject powerGage;

    public TextMeshPro textMesh;
    public TextMeshPro textMesh2;
    public float maxHeight =  -10;
    private float currentHeight = 0;

    public int height;

    public float maxAngle;
    public float minAngle;
    public GameObject directionArrow;
    public bool rotationBool; //False = clockwise rotation
    public GameObject walls;

    public float rotationAmount;

    private void Start() {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        Controller();
        RotationHandler();
        WallHandler();
        currentHeight = this.transform.position.y;
        if(currentHeight > maxHeight) {
            maxHeight = currentHeight;
            textMesh.text = (Mathf.Round(maxHeight * 100f) / 100f).ToString();
        }
        textMesh2.text = (Mathf.Round(currentHeight * 100f) / 100f).ToString();
    }

    //private void OnCollisionEnter(Collision collision) {
    //    if (collision.gameObject.CompareTag("Wall")) {
    //        rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, rb.velocity.z);
    //    }
    //}

    private void WallHandler() {
        walls.gameObject.transform.position = new Vector3(0, this.transform.position.y, 0);
    }

    private void RotationHandler() {
        if (rotationBool) {
            if(directionArrow.transform.eulerAngles.z > maxAngle) {
                rotationBool = false;
            }
            else {
                directionArrow.transform.eulerAngles = new Vector3(0, 0, directionArrow.transform.eulerAngles.z + rotationAmount);
            }
        }
        else {
            if (directionArrow.transform.eulerAngles.z < minAngle) {
                rotationBool = true;
            }
            else {
                directionArrow.transform.eulerAngles = new Vector3(0, 0, directionArrow.transform.eulerAngles.z - rotationAmount);
            }
        }
    }

    //The ButtonHandler
    private void Controller() {
        if (Input.GetKey(KeyCode.Space)) {
            if (currentforce < maxForce) {
                currentforce += Time.deltaTime * powerDoubler;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            if (currentforce > 0) {
                rb.AddRelativeForce(directionArrow.transform.right * currentforce, ForceMode.Impulse);
                print(currentforce);
            }
            currentforce = 0;
        }
        powerGage.transform.localScale = new Vector3(powerGage.transform.localScale.x, currentforce/2.8f , powerGage.transform.localScale.z);
    }
}
