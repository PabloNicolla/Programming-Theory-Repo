using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private Transform[] respawPoint;
    [SerializeField] private GameObject[] playerPrefab;
    [SerializeField] private GameObject[] player1Lifes;
    [SerializeField] private GameObject[] player2Lifes;

    private int player1 = 5;
    private int player2 = 5;

    private void Awake()
    {
        instance = this;
    }

    public void Respawn(int player)
    {
        int point = Random.Range(0, 4);
        Instantiate(playerPrefab[player], respawPoint[point].position, Quaternion.identity);

        if (player == 0)
        {
            player1 -= 1;
            Destroy(player1Lifes[player1]);
            if (player1 == 0)
            {
                FindObjectOfType<GameManager>().EndGame();
                Restart(1);
            }
        }
        else
        {
            player2 -= 1;
            Destroy(player2Lifes[player2]);
            if (player2 == 0)
            {
                FindObjectOfType<GameManager>().EndGame();
                Restart(2);
            }
        }
    }

    void Restart(int player)
    {
        if (player == 1)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
