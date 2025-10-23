using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager2 : MonoBehaviour
{
  public static GameManager2 Instance;

  [Header("Configuración del Juego")]
  public int maxEscudosEnMapa = 10;
  public float tiempoOcupacionEscudo = 3f;
  public float tiempoRespawnEscudo = 5f;
  public float velocidadHumanoideTipo1 = 1.5f;
  public float velocidadHumanoideTipo2 = 1.5f;
  public float velocidadJugador = 5f;

  [Header("Área de Spawn")]
  public Vector2 areaMin = new Vector2(-20, -20);
  public Vector2 areaMax = new Vector2(20, 20);

  [Header("Mejoras Activas")]
  public float velocidadMultiplier = 1f;
  public float gananciasMultiplier = 1f;
  public float respawnMultiplier = 1f;

  [Header("Incrementos")]
  public float incrementoPorMejora = 0.01f;

  private List<GameObject> escudosEnMapa = new List<GameObject>();
  private Dictionary<GameObject, float> escudosOcupados = new Dictionary<GameObject, float>();
  private List<ComportamientoHumanoide2> humanoidesConEscudoAlcanzado = new List<ComportamientoHumanoide2>();
  private UIManager3 uiManager;
  private PlayerMovement playerMovement;
  private bool gameOver = false;

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
    uiManager = FindFirstObjectByType<UIManager3>();
    playerMovement = FindFirstObjectByType<PlayerMovement>();

    Debug.Log("GameManager iniciado - Buscando humanoides...");

    // Verificar humanoides al inicio
    ComportamientoHumanoide2[] humanoides = FindObjectsByType<ComportamientoHumanoide2>(FindObjectsSortMode.None);
    Debug.Log($"Encontrados {humanoides.Length} humanoides en la escena");

    ActualizarUImejoras();
  }

  void Update()
  {
    if (!gameOver)
    {
      VerificarCondicionGameOver();
    }
  }
  public void HumanoideLlegoEscudo(ComportamientoHumanoide2 humanoide)
  {
    if (!humanoidesConEscudoAlcanzado.Contains(humanoide))
    {
      humanoidesConEscudoAlcanzado.Add(humanoide);
      Debug.Log($"🏁 GameManager: {humanoide.name} alcanzó su escudo. Total: {humanoidesConEscudoAlcanzado.Count}");
    }
  }

  void VerificarCondicionGameOver()
  {
    ComportamientoHumanoide2[] todosHumanoides = FindObjectsByType<ComportamientoHumanoide2>(FindObjectsSortMode.None);

    if (todosHumanoides.Length == 0) return;

    int humanoidesQueAlcanzaronEscudo = humanoidesConEscudoAlcanzado.Count;
    int totalHumanoides = todosHumanoides.Length;

    Debug.Log($"🔍 Game Over Check: {humanoidesQueAlcanzaronEscudo}/{totalHumanoides} humanoides alcanzaron escudo");

    // Si TODOS los humanoides alcanzaron sus escudos
    if (humanoidesQueAlcanzaronEscudo >= totalHumanoides && totalHumanoides > 0)
    {
      MostrarGameOver();
    }
  }

  void MostrarGameOver()
  {
    if (gameOver) return;

    gameOver = true;

    Debug.Log("🎮 GAME OVER ACTIVADO - Todos los humanoides alcanzaron sus escudos");

    if (uiManager != null)
    {
      uiManager.ShowGameOver();
      Debug.Log("✅ Game Over UI mostrada");
    }
    else
    {
      Debug.LogError("❌ UIManager3 no encontrado para mostrar Game Over");
    }
  }

  void InicializarEscudos()
  {
    GameObject[] escudosTipo1 = GameObject.FindGameObjectsWithTag("Shield_Tipo1");
    GameObject[] escudosTipo2 = GameObject.FindGameObjectsWithTag("Shield_Tipo2");

    foreach (GameObject escudo in escudosTipo1)
    {
      if (!escudosEnMapa.Contains(escudo))
      {
        escudosEnMapa.Add(escudo);
      }
    }

    foreach (GameObject escudo in escudosTipo2)
    {
      if (!escudosEnMapa.Contains(escudo))
      {
        escudosEnMapa.Add(escudo);
      }
    }

    Debug.Log($"Inicializados {escudosEnMapa.Count} escudos en el mapa");
  }

  void AsignarEscudosPredefinidos()
  {
    ComportamientoHumanoide2[] humanoides = FindObjectsByType<ComportamientoHumanoide2>(FindObjectsSortMode.None);

    foreach (ComportamientoHumanoide2 humanoide in humanoides)
    {
      string tagEscudoBuscado = humanoide.CompareTag("Humanoide_Tipo1") ? "Shield_Tipo1" : "Shield_Tipo2";
      GameObject[] escudosDelTipo = GameObject.FindGameObjectsWithTag(tagEscudoBuscado);

      if (escudosDelTipo.Length > 0)
      {
        if (humanoide.CompareTag("Humanoide_Tipo1") && humanoide.shield1 == null)
        {
          foreach (GameObject escudo in escudosDelTipo)
          {
            humanoide.shield1 = escudo;
            Debug.Log($"Asignado {escudo.name} a {humanoide.name} (Tipo1)");
            break;
          }
        }
        else if (humanoide.CompareTag("Humanoide_Tipo2") && humanoide.shield2 == null)
        {
          foreach (GameObject escudo in escudosDelTipo)
          {
            humanoide.shield2 = escudo;
            Debug.Log($"Asignado {escudo.name} a {humanoide.name} (Tipo2)");
            break;
          }
        }
      }
    }
  }

  public Vector3 ObtenerPosicionSpawnSegura()
  {
    Vector3 posicion;
    bool posicionSegura = false;
    int intentos = 0;
    int maxIntentos = 50;

    do
    {
      float x = Random.Range(areaMin.x, areaMax.x);
      float z = Random.Range(areaMin.y, areaMax.y);
      posicion = new Vector3(x, 0.5f, z);

      posicionSegura = EsPosicionSegura(posicion);
      intentos++;

    } while (!posicionSegura && intentos < maxIntentos);

    return posicionSegura ? posicion : Vector3.zero;
  }

  bool EsPosicionSegura(Vector3 posicion)
  {
    float distanciaMinima = 3f;

    foreach (GameObject escudo in escudosEnMapa)
    {
      if (escudo != null && escudo.activeInHierarchy &&
          Vector3.Distance(posicion, escudo.transform.position) < distanciaMinima)
        return false;
    }

    GameObject jugador = GameObject.FindGameObjectWithTag("Player");
    if (jugador != null && Vector3.Distance(posicion, jugador.transform.position) < distanciaMinima)
      return false;

    GameObject[] humanoidesTipo1 = GameObject.FindGameObjectsWithTag("Humanoide_Tipo1");
    GameObject[] humanoidesTipo2 = GameObject.FindGameObjectsWithTag("Humanoide_Tipo2");

    foreach (GameObject humanoide in humanoidesTipo1)
    {
      if (humanoide != null && Vector3.Distance(posicion, humanoide.transform.position) < distanciaMinima)
        return false;
    }

    foreach (GameObject humanoide in humanoidesTipo2)
    {
      if (humanoide != null && Vector3.Distance(posicion, humanoide.transform.position) < distanciaMinima)
        return false;
    }

    return true;
  }

  public void EscudoRecolectado(GameObject escudo)
  {
    if (escudosEnMapa.Contains(escudo))
    {
      float tiempoRespawn = tiempoRespawnEscudo * respawnMultiplier;
      StartCoroutine(RespawnEscudo(escudo, tiempoRespawn));
      Debug.Log($"Escudo {escudo.name} recolectado. Respawn en {tiempoRespawn} segundos");
      NotificarHumanoidesEscudoDesaparecido(escudo);
    }
  }

  private System.Collections.IEnumerator RespawnEscudo(GameObject escudo, float tiempoEspera)
  {
    yield return new WaitForSeconds(tiempoEspera);

    Vector3 nuevaPosicion = ObtenerPosicionSpawnSegura();
    if (nuevaPosicion != Vector3.zero)
    {
      ShieldCollectible3 shieldCollectible = escudo.GetComponent<ShieldCollectible3>();
      if (shieldCollectible != null)
      {
        shieldCollectible.ReactivateShield(nuevaPosicion);
      }
      Debug.Log($"Escudo {escudo.name} respawneado en {nuevaPosicion}");
      NotificarHumanoidesEscudoRespawned(escudo);
    }
    else
    {
      StartCoroutine(RespawnEscudo(escudo, 2f));
    }
  }

  void NotificarHumanoidesEscudoDesaparecido(GameObject escudo)
  {
    ComportamientoHumanoide2[] humanoides = FindObjectsByType<ComportamientoHumanoide2>(FindObjectsSortMode.None);

    foreach (ComportamientoHumanoide2 humanoide in humanoides)
    {
      if ((humanoide.shield1 == escudo) || (humanoide.shield2 == escudo))
      {
        humanoide.ResetColor();
      }
    }
  }

  void NotificarHumanoidesEscudoRespawned(GameObject escudo)
  {
    ComportamientoHumanoide2[] humanoides = FindObjectsByType<ComportamientoHumanoide2>(FindObjectsSortMode.None);

    foreach (ComportamientoHumanoide2 humanoide in humanoides)
    {
      if (humanoide.CompareTag("Humanoide_Tipo1") && humanoide.shield1 == escudo)
      {
        humanoide.ReanudarMovimiento();
      }
      else if (humanoide.CompareTag("Humanoide_Tipo2") && humanoide.shield2 == escudo)
      {
        humanoide.ReanudarMovimiento();
      }
    }
  }

  public void HumanoideEnEscudo(GameObject escudo)
  {
    if (!escudosOcupados.ContainsKey(escudo))
    {
      escudosOcupados[escudo] = Time.time;
      Debug.Log($"Escudo {escudo.name} ocupado por humanoide");
    }
  }

  void ActualizarEscudosOcupados()
  {
    List<GameObject> escudosParaLiberar = new List<GameObject>();

    foreach (var kvp in escudosOcupados)
    {
      if (Time.time - kvp.Value >= tiempoOcupacionEscudo)
      {
        escudosParaLiberar.Add(kvp.Key);
      }
    }

    foreach (GameObject escudo in escudosParaLiberar)
    {
      escudosOcupados.Remove(escudo);
      Debug.Log($"Escudo {escudo.name} liberado");
    }
  }

  public bool PuedeRecolectarEscudo(GameObject escudo)
  {
    return !escudosOcupados.ContainsKey(escudo);
  }

  // SISTEMA DE MEJORAS
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
      playerMovement.speed = velocidadJugador * velocidadMultiplier;
    }

    ComportamientoHumanoide2[] humanoides = FindObjectsByType<ComportamientoHumanoide2>(FindObjectsSortMode.None);
    foreach (ComportamientoHumanoide2 humanoide in humanoides)
    {
      humanoide.speed *= (1f + incrementoPorMejora);
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

  public float GetVelocidadBaseHumanoide(string tipo)
  {
    return tipo == "Tipo1" ? velocidadHumanoideTipo1 : velocidadHumanoideTipo2;
  }
}