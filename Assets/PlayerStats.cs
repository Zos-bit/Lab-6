using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth = 3;
    [SerializeField, Min(1)] private int startingLives = 3;
    [SerializeField, Min(0f)] private float immunityDuration = 1.5f;
    [SerializeField, Min(0.01f)] private float flickerInterval = 0.1f;

    public int Health { get; private set; }
    public int Lives { get; private set; }
    public bool IsImmune { get; private set; }
    public bool IsGameOver { get; private set; }

    private SpriteRenderer playerSprite;
    private LevelManager levelManager;
    private Coroutine immunityRoutine;
    private bool originalSpriteEnabled;
    private GUIStyle hudStyle;

    private void Awake()
    {
        Health = maxHealth;
        Lives = startingLives;
        playerSprite = GetComponent<SpriteRenderer>();
        if (playerSprite != null)
        {
            originalSpriteEnabled = playerSprite.enabled;
        }
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || IsImmune || IsGameOver)
        {
            return;
        }

        Health = Mathf.Max(0, Health - amount);
        if (Health == 0)
        {
            Lives = Mathf.Max(0, Lives - 1);
            if (Lives == 0)
            {
                IsGameOver = true;
                StopImmunity();
                if (levelManager != null)
                {
                    levelManager.GameOver();
                }
                return;
            }

            Health = maxHealth;
            if (levelManager != null)
            {
                levelManager.RespawnPlayer();
            }
        }

        StartImmunity();
    }

    private void StartImmunity()
    {
        StopImmunity();
        if (immunityDuration > 0f)
        {
            immunityRoutine = StartCoroutine(FlickerWhileImmune());
        }
    }

    private IEnumerator FlickerWhileImmune()
    {
        IsImmune = true;
        float elapsed = 0f;

        while (elapsed < immunityDuration)
        {
            if (playerSprite != null)
            {
                playerSprite.enabled = !playerSprite.enabled;
            }

            float wait = Mathf.Min(flickerInterval, immunityDuration - elapsed);
            yield return new WaitForSeconds(wait);
            elapsed += wait;
        }

        immunityRoutine = null;
        RestoreSprite();
        IsImmune = false;
    }

    private void StopImmunity()
    {
        if (immunityRoutine != null)
        {
            StopCoroutine(immunityRoutine);
            immunityRoutine = null;
        }
        RestoreSprite();
        IsImmune = false;
    }

    private void RestoreSprite()
    {
        if (playerSprite != null)
        {
            playerSprite.enabled = originalSpriteEnabled;
        }
    }

    private void OnDisable()
    {
        StopImmunity();
    }

    private void OnGUI()
    {
        if (hudStyle == null)
        {
            hudStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleLeft,
                fontSize = 20,
                fontStyle = FontStyle.Bold
            };
            hudStyle.normal.textColor = Color.white;
        }

        Color previousColor = GUI.color;
        GUI.color = new Color(0f, 0f, 0f, 0.7f);
        GUI.Box(new Rect(12f, 12f, 260f, 48f), GUIContent.none);
        GUI.color = previousColor;
        GUI.Label(new Rect(24f, 18f, 244f, 36f), $"Health: {Health}/{maxHealth}    Lives: {Lives}", hudStyle);
    }
}
