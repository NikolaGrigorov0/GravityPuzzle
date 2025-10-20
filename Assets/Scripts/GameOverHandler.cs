using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject gameOverUI;   // assign in Inspector
    public float fallY = -10f;      // change if your map is higher

    private bool isGameOver = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (!isGameOver && player.position.y < fallY)
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f; // pause game
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

