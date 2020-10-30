using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodHoles : MonoBehaviour
{
    public GameObject cam;
    public GameObject player;
    public Rigidbody playerBody;

    float timeForLerp = Time.time;
    void Start() {
        cam = GameObject.Find("Main Camera");
        player = GameObject.FindWithTag("Player");
        playerBody = player.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Player") {

            //IMPORTANT: these need to be reset to true when the player respawns
            //fall through floor:
            coll.GetComponent<Collider>().enabled = false;
            //stop follow cam:
            cam.GetComponent<CameraBehaviour>().enabled = false;

            //move player to center of hole:
            player.transform.position = this.transform.position;

            //using lerp would make the fall-in motion much smoother, but requires time calculation:
            //player.transform.position = Vector3.Lerp(transform.position, this.gameObject.transform.position, .05f);


            //stop motion in all horizontal axes:
            playerBody.constraints = RigidbodyConstraints.FreezePositionX;
            playerBody.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }
}
