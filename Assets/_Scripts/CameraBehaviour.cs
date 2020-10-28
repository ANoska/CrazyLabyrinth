using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = desiredPosition;
    }
}
