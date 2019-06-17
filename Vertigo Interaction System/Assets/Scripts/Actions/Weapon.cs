using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : EquipmentFunction
{
    public ControllerObject magazineSlot;
    public GameObject bulletSpawnPosition;
    public GameObject muzzleFlash;

    [Header("Bullet")]
    public GameObject bullet;
    public GameObject jacked;


    [Header("Automatic fire Settings")]
    private bool singleFire = true;
    private bool coroutineInProgress = false;
    public float fireRate;

    #region overrides
    public override void ActivateEquipmentTouchFunction(bool down) {
        if (down) {
            singleFire = !singleFire;
        }
    }

    public override void ActivateEquipmenPinchtFunction(bool down) {
        if (!coroutineInProgress && down) {
            if (singleFire) {
                Fire();
            }
            else {
                StartCoroutine(AutomaticFire(fireRate));
            }
        }else if(coroutineInProgress && !down) {
            coroutineInProgress = false;
        }
    }
    #endregion

    #region newFunctions
    private void Fire() {
        if (magazineSlot.equipmentSlot && magazineSlot.equipmentSlot.GetData<int>(0) > 0) {
            magazineSlot.equipmentSlot.SetData<int>(magazineSlot.equipmentSlot.GetData<int>(0) - 1);
            StartCoroutine(MuzzleFlash(muzzleFlash));

            GameObject x = Instantiate(bullet,
                bulletSpawnPosition.transform.position,
                bulletSpawnPosition.transform.rotation,
                null);
            x.GetComponent<Rigidbody>().AddForce(x.transform.right * 75, ForceMode.Impulse);

            RaycastHit hit;
            if (Physics.Raycast(bulletSpawnPosition.transform.position, bulletSpawnPosition.transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity)) {
                if (hit.collider.CompareTag("Can")) {
                    Debug.Log("hit");
                }
            }
        }
    }

    private IEnumerator AutomaticFire(float fireRate) {
        coroutineInProgress = true;
        float time = fireRate;
        while (coroutineInProgress) {
            time += Time.deltaTime;
            if(time >= fireRate) {
                Fire();
                time = 0;
            }
            yield return null;
        }
    }

    private IEnumerator MuzzleFlash(GameObject x) {
        x.transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), 0, 0);
        x.SetActive(true);
        float time = .05f;
        while (time > 0) {
            time -= Time.deltaTime;
            yield return null;
        }
        x.SetActive(false);
    }
    #endregion
}
