/*
  ShieldRespawnManager.cs
  Gestiona el respawn de los escudos recolectados, aplicando un multiplicador de tiempo desde GameManager.
*/

using UnityEngine;

public class ShieldRespawnManager2 : MonoBehaviour
{
  public float respawnTime = 5f;

  private ShieldCollectible4 shieldCollectible;
  private bool isCollected = false;
  private GameManager2 gameManager;

  void Start()
  {
    shieldCollectible = GetComponent<ShieldCollectible4>();
    gameManager = FindFirstObjectByType<GameManager2>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player") && !isCollected)
    {
      isCollected = true;

      Debug.Log($"Escudo {name} recolectado");

      // Aplicar multiplicador de respawn
      float tiempoRespawn = respawnTime * gameManager.GetRespawnMultiplier();
      Invoke("RespawnShield", tiempoRespawn);
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