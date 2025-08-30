using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    [Tooltip("Donde hace Spawn")]
    public Transform spawnPoint;

    void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        CheckpointManager.Instance?.SetCheckpoint(spawnPoint != null ? spawnPoint : transform);
    }

    void OnDrawGizmos()
    {
        Transform sp = spawnPoint != null ? spawnPoint : transform;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(sp.position, 0.25f);
        Gizmos.DrawLine(sp.position, sp.position + sp.forward * 0.6f);
    }
}
