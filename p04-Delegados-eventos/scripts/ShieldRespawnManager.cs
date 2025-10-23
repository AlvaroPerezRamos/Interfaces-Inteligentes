/*
  ShieldRespawnManager.cs
  Gestiona el respawn de los escudos recolectados, aplicando un multiplicador de tiempo desde GameManager.
*/

using UnityEngine;

public class ShieldRespawnManager : MonoBehaviour
{
  public float respawnTime = 5f;

  private ShieldCollectible2 shieldCollectible;
  private bool isCollected = false;

  void Start()
  {
    shieldCollectible = GetComponent<ShieldCollectible2>();
    gameManager = FindFirstObjectByType<GameManager>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player") && !isCollected)
    {
      isCollected = true;

      Debug.Log($"Escudo {name} recolectado");
    }
  }

  void RespawnShield()
  {
    isCollected = false;

    // Reactivar usando el método del ShieldCollectible
    if (shieldCollectible != null)
    {
      shieldCollectible.ReactivateShield();
    }

    Debug.Log($"Escudo {name} respawneado");
  }
}