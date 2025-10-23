/*
  ShieldCollectible.cs
  Maneja la recolección de escudos, asigna puntos y permite reactivación.
*/

using UnityEngine;

public class ShieldCollectible3 : MonoBehaviour
{
  public int points = 5;
  private bool collected = false;
  private Renderer shieldRenderer;
  private Collider shieldCollider;

  void Start()
  {
    shieldRenderer = GetComponent<Renderer>();
    shieldCollider = GetComponent<Collider>();

    if (CompareTag("Shield_Tipo1"))
    {
      points = 5;
    }
    else if (CompareTag("Shield_Tipo2"))
    {
      points = 10;
    }
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player") && !collected)
    {
      collected = true;

      ScoreManager3 scoreManager = FindFirstObjectByType<ScoreManager3>();
      if (scoreManager != null)
      {
        scoreManager.AddPoints(points);
      }

      Debug.Log($"Escudo recolectado: +{points} puntos base");

      // 👇 DESACTIVAR COMPLETAMENTE el objeto
      gameObject.SetActive(false);
    }
  }

  // Método para reactivar el escudo
  public void ReactivateShield()
  {
    collected = false;
    gameObject.SetActive(true);
    Debug.Log($"Escudo {name} reactivado");
  }

  public bool IsShieldActive()
  {
    return gameObject.activeInHierarchy && !collected;
  }
}