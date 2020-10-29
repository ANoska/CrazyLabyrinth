using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodHoles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collision coll) {
        //Find out what hit the hole
        //GameObject collideWith = coll.gameObject;
        if(coll.gameObject.tag == "Player") {
            //NEXT LEVEL
            Destroy(gameObject);
        }

    }
}
