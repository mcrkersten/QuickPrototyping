using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movePos = new Vector3(this.transform.position.x, player.transform.position.y + 1.5f, this.gameObject.transform.position.z);
        transform.position = Vector3.Lerp(this.transform.position, movePos, .05f);
    }
}
