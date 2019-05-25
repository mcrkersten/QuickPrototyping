using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToPosition : MonoBehaviour
{
    public Vector3 endPosition;
    // Update is called once per frame

    private void Start() {
        endPosition = this.transform.position;
        OnDoorClick.OnMoveCamera += Next;
    }
    private void OnDestroy() {
        OnDoorClick.OnMoveCamera -= Next;
    }

    void Next(Vector3 newPosition) {
        endPosition = new Vector3(0, -1.38f, this.transform.position.z + 20);
        StartCoroutine(Move1(newPosition, 2));
    }

    private IEnumerator Move1(Vector3 Finish, float time) {
        float elapsedTime = 0;

        while (elapsedTime < time) {
            this.transform.position = Vector3.Lerp(this.transform.position, Finish, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            if(Vector3.Distance(this.transform.position, Finish) < .15f) {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(Move2(endPosition, Vector3.Distance(this.transform.position,endPosition)));
    }

    private IEnumerator Move2(Vector3 Finish, float time) {
        float elapsedTime = 0;

        while (elapsedTime < time) {
            this.transform.position = Vector3.Lerp(this.transform.position, Finish, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            if (Vector3.Distance(this.transform.position, Finish) < .1f) {
                this.transform.position = Finish;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
