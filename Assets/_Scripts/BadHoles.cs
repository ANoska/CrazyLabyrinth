using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadHoles : MonoBehaviour
{
    void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Player") {
            Destroy(coll.gameObject);
        }
    }
}
