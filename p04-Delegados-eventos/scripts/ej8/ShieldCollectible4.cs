using UnityEngine;

public class ShieldCollectible4 : MonoBehaviour
{
  [Header("Configuración")]
  public int points = 5;
  private bool collected = false;
  private Renderer shieldRenderer;
  private Collider shieldCollider;
  private GameManager2 gameManager;

  void Start()
  {
    shieldRenderer = GetComponent<Renderer>();
    shieldCollider = GetComponent<Collider>();
    gameManager = GameManager2.Instance;

    if (CompareTag("Shield_Tipo1"))
    {
      points = 5;
    }
    else if (CompareTag("Shield_Tipo2"))
    {
      points = 10;
    }

    Debug.Log($"Escudo {name} inicializado - Tipo: {(CompareTag("Shield_Tipo1") ? "1" : "2")}");
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player") && !collected)
    {
      // Verificar si el escudo está ocupado por humanoides
      if (gameManager != null && gameManager.PuedeRecolectarEscudo(gameObject))
      {
        collected = true;

        // Calcular puntos con multiplicador de ganancias
        int puntosFinales = Mathf.RoundToInt(points * gameManager.GetGananciasMultiplier());

        ScoreManager4 scoreManager = FindFirstObjectByType<ScoreManager4>();
        if (scoreManager != null)
        {
          scoreManager.AddPoints(puntosFinales);
          Debug.Log($"Escudo recolectado: +{puntosFinales} puntos (base: {points} × {gameManager.GetGananciasMultiplier():F2})");
        }
        else
        {
          Debug.LogError("ScoreManager3 no encontrado");
        }

        gameObject.SetActive(false);

        // Notificar al GameManager para que maneje el respawn
        if (gameManager != null)
        {
          gameManager.EscudoRecolectado(gameObject);
        }
      }
      else
      {
        Debug.Log($"Escudo {name} está ocupado, no se puede recolectar");
      }
    }
    else if ((other.CompareTag("Humanoide_Tipo1") || other.CompareTag("Humanoide_Tipo2")) && gameManager != null)
    {
      // Solo notificar si el escudo está activo
      if (gameObject.activeInHierarchy && CorrespondeHumanoideEscudo(other.tag))
      {
        gameManager.HumanoideEnEscudo(gameObject);
      }
    }
  }

  void OnTriggerStay(Collider other)
  {
    if ((other.CompareTag("Humanoide_Tipo1") || other.CompareTag("Humanoide_Tipo2")) && gameManager != null)
    {
      // Solo notificar si el escudo está activo
      if (gameObject.activeInHierarchy && CorrespondeHumanoideEscudo(other.tag))
      {
        gameManager.HumanoideEnEscudo(gameObject);
      }
    }
  }

  bool CorrespondeHumanoideEscudo(string tagHumanoide)
  {
    return (tagHumanoide == "Humanoide_Tipo1" && CompareTag("Shield_Tipo1")) ||
           (tagHumanoide == "Humanoide_Tipo2" && CompareTag("Shield_Tipo2"));
  }

  public void ReactivateShield(Vector3? nuevaPosicion = null)
  {
    collected = false;

    // Si se proporciona una nueva posición, mover el escudo
    if (nuevaPosicion.HasValue)
    {
      transform.position = nuevaPosicion.Value;
      Debug.Log($"Escudo {name} reactivado en posición: {nuevaPosicion.Value}");
    }
    else
    {
      Debug.Log($"Escudo {name} reactivado en posición actual");
    }

    gameObject.SetActive(true);
  }

  public bool IsShieldActive()
  {
    return gameObject.activeInHierarchy && !collected;
  }

  public bool PuedeCambiarColorHumanoide(string tagHumanoide)
  {
    return gameObject.activeInHierarchy && CorrespondeHumanoideEscudo(tagHumanoide);
  }

  public void CambiarColorOcupado()
  {
    if (shieldRenderer != null && gameObject.activeInHierarchy)
    {
      Material nuevoMaterial = new Material(shieldRenderer.material);
      nuevoMaterial.color = Color.gray;
      shieldRenderer.material = nuevoMaterial;
    }
  }

  public void ResetearColor()
  {
    if (shieldRenderer != null && gameObject.activeInHierarchy)
    {
      // Volver al color original basado en el tipo
      Material nuevoMaterial = new Material(shieldRenderer.material);
      nuevoMaterial.color = CompareTag("Shield_Tipo1") ? Color.green : Color.red;
      shieldRenderer.material = nuevoMaterial;
    }
  }
}