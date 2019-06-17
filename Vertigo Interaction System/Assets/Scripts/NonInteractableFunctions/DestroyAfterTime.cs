using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time;
    public GameObject impactFlash;
    public GameObject dustImpact;
    private float currentTime;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > time) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(currentTime > .05f) {
            Instantiate(impactFlash, this.transform.position, this.transform.rotation, null);
            Instantiate(dustImpact, this.transform.position, this.transform.rotation, null);
            if(collision.gameObject.GetComponent<Rigidbody>() != null) {
                ContactPoint x = collision.GetContact(0);
                float vel = collision.relativeVelocity.magnitude;

                collision.gameObject.GetComponent<Rigidbody>().AddForce(x.point - new Vector3(x.normal.x, 1, x.normal.z) * vel/2);
            }
            Destroy(this.gameObject);
        }
    }
}
