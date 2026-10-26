using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpawnGhost : MonoBehaviour
{
    public GameObject ghostPrefab;
    public Transform spawnPoint;
    [SerializeField] private bool spawnOnlyOnce = false;

    private bool hasSpawned;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ghostPrefab == null || (spawnOnlyOnce && hasSpawned))
        {
            return;
        }

        PlayerStats player = other.GetComponentInParent<PlayerStats>();
        if (player == null)
        {
            return;
        }

        Transform point = spawnPoint != null ? spawnPoint : transform;
        Instantiate(ghostPrefab, point.position, point.rotation);
        hasSpawned = true;
    }
}
