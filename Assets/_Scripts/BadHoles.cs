﻿using UnityEngine;

public class BadHoles : MonoBehaviour
{
    public static bool badHoleMet;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            //f all through floor:
            other.GetComponent<Collider>().enabled = false;

            // move player to center of hole:
            other.gameObject.transform.position = this.transform.position;

            // using lerp would make the fall-in motion much smoother, but requires time calculation:
            // player.transform.position = Vector3.Lerp(transform.position, this.gameObject.transform.position, .05f);

            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;

            badHoleMet = true;
        }
    }
}
