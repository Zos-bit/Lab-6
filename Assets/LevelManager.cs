using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject CurrentCheckpoint;

    private PlayerController player;
    private Vector3 initialSpawnPosition;
    private bool isGameOver;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            initialSpawnPosition = player.transform.position;
        }
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void RespawnPlayer()
    {
        if (isGameOver || player == null)
        {
            return;
        }

        Vector3 spawnPosition = CurrentCheckpoint != null
            ? CurrentCheckpoint.transform.position
            : initialSpawnPosition;

        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        if (body != null)
        {
            body.linearVelocity = Vector2.zero;
        }

        player.transform.position = spawnPosition;
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        if (player != null)
        {
            player.enabled = false;
            Rigidbody2D body = player.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                body.linearVelocity = Vector2.zero;
            }
        }

        Time.timeScale = 0f;
    }

    private void OnGUI()
    {
        if (!isGameOver)
        {
            return;
        }

        const float width = 320f;
        const float height = 140f;
        Rect panel = new Rect((Screen.width - width) / 2f, (Screen.height - height) / 2f, width, height);
        GUI.Box(panel, "Game Over");
        GUI.Label(new Rect(panel.x + 40f, panel.y + 45f, width - 80f, 25f), "No lives remaining");
        if (GUI.Button(new Rect(panel.x + 40f, panel.y + 85f, width - 80f, 35f), "Restart (R)"))
        {
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
