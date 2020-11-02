using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Set Dynamically")]
    public GameObject player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 25, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = desiredPosition;
    }
}
