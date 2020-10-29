using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Set Dynamically")]
    public GameObject player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            offset = transform.position - player.transform.position;
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = desiredPosition;
    }
}
