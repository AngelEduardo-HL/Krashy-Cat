using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    [Header("Spawn por defecto")]
    public Transform defaultSpawn;

    [Header("Invencibilidad post-respawn (s)")]
    public float respawnInvincibility = 1.0f;

    private Transform currentSpawn;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        currentSpawn = defaultSpawn;
    }

    public void SetCheckpoint(Transform t) => currentSpawn = t;

    public void RespawnPlayer(GameObject player)
    {
        if (player == null) return;
        Transform spawn = currentSpawn != null ? currentSpawn : defaultSpawn;
        if (spawn == null) { Debug.LogWarning("No hay checkpoint ni defaultSpawn."); return; }

        var cc = player.GetComponent<CharacterController>();
        if (cc) cc.enabled = false;

        player.transform.SetPositionAndRotation(spawn.position, spawn.rotation);

        // Limpia velocidad vertical del movimiento si expones el helper (ver paso 3)
        var pm = player.GetComponent<PlayerMovement>();
        if (pm) pm.ResetVerticalVelocity();

        if (cc) cc.enabled = true;

        // Invencibilidad breve tras respawn
        var hp = player.GetComponent<PlayerHP>();
        if (hp) StartCoroutine(GrantInvincibility(hp, respawnInvincibility));
    }

    IEnumerator GrantInvincibility(PlayerHP hp, float t)
    {
        hp.isInvincible = true;
        yield return new WaitForSeconds(t);
        hp.isInvincible = false;
    }
}
