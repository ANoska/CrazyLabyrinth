using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Vector3 boardPos;
    public GameObject[] boards;
    public GameObject player;

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public GameObject board;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        levelMax = boards.Length;
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartLevel()
    {
        if (board != null)
            Destroy(board);

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject pTemp in gos)
            Destroy(pTemp);

        board = Instantiate<GameObject>(boards[level]);
        board.transform.position = boardPos;

        player = Instantiate<GameObject>(player);
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
}
