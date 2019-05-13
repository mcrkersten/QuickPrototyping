using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlacer : MonoBehaviour
{
    public GameObject platform;
    public GameObject player;
    private float placeOnNewHeight;
    public float newHeight;
    // Start is called before the first frame update

    private void Start() {
        placeOnNewHeight = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y > placeOnNewHeight) {
            PlaceNewPlatform();
        }
    }

    void PlaceNewPlatform() {
        placeOnNewHeight += newHeight;
        float yPositionOffset = Random.Range(-.5f, .5f);
        float xPosition = Random.Range(-3.25f, 3.80f);
        Vector3 PlacePosition = new Vector3(xPosition, placeOnNewHeight + yPositionOffset, 0);
        Instantiate(platform, PlacePosition, this.transform.rotation);
    }
}
