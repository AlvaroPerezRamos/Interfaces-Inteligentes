/*
  GameManager.cs
  Gestiona las mejoras del jugador y su aplicación aleatoria.
*/

using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

  [Header("Mejoras Activas")]
  public float velocidadMultiplier = 1f;
  public float gananciasMultiplier = 1f;
  public float respawnMultiplier = 1f;

  [Header("Incrementos")]
  public float incrementoPorMejora = 0.01f;

  private UIManager2 uiManager;
  private PlayerMovement playerMovement;

  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  void Start()
  {
    uiManager = FindFirstObjectByType<UIManager2>();
    playerMovement = FindFirstObjectByType<PlayerMovement>();
    ActualizarUImejoras();
  }

  public void AplicarMejoraAleatoria()
  {
    List<System.Action> mejoras = new List<System.Action>
        {
            MejorarVelocidad,
            MejorarGanancias,
            MejorarRespawn
        };

    int indiceAleatorio = Random.Range(0, mejoras.Count);
    mejoras[indiceAleatorio]?.Invoke();

    ActualizarUImejoras();
  }

  void MejorarVelocidad()
  {
    velocidadMultiplier += incrementoPorMejora;
    if (playerMovement != null)
    {
      playerMovement.speed *= (1f + incrementoPorMejora);
    }
    Debug.Log($"Velocidad mejorada: x{velocidadMultiplier:F2}");
  }

  void MejorarGanancias()
  {
    gananciasMultiplier += incrementoPorMejora;
    Debug.Log($"Ganancias mejoradas: x{gananciasMultiplier:F2}");
  }

  void MejorarRespawn()
  {
    respawnMultiplier -= incrementoPorMejora;
    respawnMultiplier = Mathf.Max(0.1f, respawnMultiplier);
    Debug.Log($"Tiempo de respawn mejorado: x{respawnMultiplier:F2}");
  }

  void ActualizarUImejoras()
  {
    if (uiManager != null)
    {
      uiManager.ActualizarMejoras(velocidadMultiplier, gananciasMultiplier, respawnMultiplier);
    }
  }

  public float GetGananciasMultiplier()
  {
    return gananciasMultiplier;
  }

  public float GetRespawnMultiplier()
  {
    return respawnMultiplier;
  }
}