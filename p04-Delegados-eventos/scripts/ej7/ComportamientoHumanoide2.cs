using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ComportamientoHumanoide2 : MonoBehaviour
{
  public Notificador3 notificador;
  public float speed = 2.5f;
  public float rotSpeed = 4f;
  public GameObject shield1;
  public GameObject shield2;

  private bool moveToShield1 = false;
  private bool moveToShield2 = false;
  private Animator anim;
  private Renderer humanoidRenderer;
  private Color originalColor;
  private bool colorChanged = false;
  private GameObject escudoActual;
  private bool haAlcanzadoEscudo = false;

  void Start()
  {
    anim = GetComponent<Animator>();
    if (anim != null)
      anim.applyRootMotion = false;

    humanoidRenderer = GetComponentInChildren<Renderer>();
    if (humanoidRenderer != null)
      originalColor = humanoidRenderer.material.color;

    if (notificador == null)
    {
      Debug.LogError($"{name} no tiene asignado el Notificador3 en el inspector.");
      return;
    }

    // Suscribirse según el tag del humanoide
    if (CompareTag("Humanoide_Tipo1"))
    {
      notificador.OnTriggerHumanoide2 += OnHumanoide2Touched;
      Debug.Log($"{name} (Tipo1) suscrito a eventos");
    }
    else if (CompareTag("Humanoide_Tipo2"))
    {
      notificador.OnTriggerHumanoide1 += OnHumanoide1Touched;
      Debug.Log($"{name} (Tipo2) suscrito a eventos");
    }

    // Iniciar movimiento automáticamente hacia el escudo asignado
    IniciarMovimientoAutomatico();
  }

  void IniciarMovimientoAutomatico()
  {
    if (CompareTag("Humanoide_Tipo1") && shield1 != null)
    {
      moveToShield1 = true;
      Debug.Log($"{name} iniciando movimiento automático hacia {shield1.name}");
    }
    else if (CompareTag("Humanoide_Tipo2") && shield2 != null)
    {
      moveToShield2 = true;
      Debug.Log($"{name} iniciando movimiento automático hacia {shield2.name}");
    }
  }

  void Update()
  {
    VerificarEscudoDesaparecido();

    // MOVIMIENTO - condiciones simplificadas
    bool puedeMoverseHaciaShield1 = moveToShield1 && shield1 != null && shield1.activeInHierarchy && !haAlcanzadoEscudo;
    bool puedeMoverseHaciaShield2 = moveToShield2 && shield2 != null && shield2.activeInHierarchy && !haAlcanzadoEscudo;

    if (puedeMoverseHaciaShield1)
    {
      MoveTowards(shield1.transform);
    }
    else if (puedeMoverseHaciaShield2)
    {
      MoveTowards(shield2.transform);
    }
    else
    {
      // Si no puede moverse, detener animación
      if (anim != null)
        anim.SetBool("isWalking", false);
    }

    // Verificar si alcanzó el escudo
    if (!haAlcanzadoEscudo)
    {
      VerificarSiAlcanzoEscudo();
    }
  }

  void VerificarEscudoDesaparecido()
  {
    if (colorChanged && escudoActual != null && !escudoActual.activeInHierarchy)
    {
      ResetColor();
      escudoActual = null;
      haAlcanzadoEscudo = false; // Permitir movimiento nuevamente
    }
  }

  void MoveTowards(Transform target)
  {
    if (target == null || !target.gameObject.activeInHierarchy)
    {
      DetenerMovimiento();
      return;
    }

    Vector3 direction = target.position - transform.position;
    direction.y = 0;

    if (direction.magnitude > 0.5f) // Aumentada la distancia mínima
    {
      // Rotación
      Quaternion lookRot = Quaternion.LookRotation(direction);
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotSpeed);

      // Movimiento
      transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

      // Animación
      if (anim != null)
        anim.SetBool("isWalking", true);
    }
    else
    {
      // Muy cerca del objetivo
      if (anim != null)
        anim.SetBool("isWalking", false);
    }
  }

  void VerificarSiAlcanzoEscudo()
  {
    float distanciaUmbral = 1.0f; // Distancia para considerar "alcanzado"

    if (CompareTag("Humanoide_Tipo1") && shield1 != null && shield1.activeInHierarchy)
    {
      float distancia = Vector3.Distance(transform.position, shield1.transform.position);
      if (distancia <= distanciaUmbral)
      {
        AlcanzoEscudo();
      }
    }
    else if (CompareTag("Humanoide_Tipo2") && shield2 != null && shield2.activeInHierarchy)
    {
      float distancia = Vector3.Distance(transform.position, shield2.transform.position);
      if (distancia <= distanciaUmbral)
      {
        AlcanzoEscudo();
      }
    }
  }

  void AlcanzoEscudo()
  {
    haAlcanzadoEscudo = true;

    // Cambiar color
    if (!colorChanged)
    {
      Color colorEscudo = CompareTag("Humanoide_Tipo1") ? Color.green : Color.red;
      CambiarColor(colorEscudo);
      colorChanged = true;
    }

    // Detener movimiento
    DetenerMovimiento();

    // Notificar al GameManager
    GameManager gameManager = GameManager.Instance;
    if (gameManager != null)
    {
      gameManager.HumanoideLlegoEscudo(this);
      Debug.Log($"🏁 {name} notificó que alcanzó su escudo");
    }

    Debug.Log($"🎯 {name} ALCANZÓ su escudo objetivo");
  }

  void OnHumanoide1Touched()
  {
    Debug.Log($"🎯 {name} (Tipo2) recibió evento - moviéndose hacia escudo tipo 2");

    if (!haAlcanzadoEscudo)
    {
      moveToShield2 = true;
      moveToShield1 = false;

      if (colorChanged)
      {
        ResetColor();
      }
    }
  }

  void OnHumanoide2Touched()
  {
    Debug.Log($"🎯 {name} (Tipo1) recibió evento - moviéndose hacia escudo tipo 1");

    if (!haAlcanzadoEscudo)
    {
      moveToShield1 = true;
      moveToShield2 = false;

      if (colorChanged)
      {
        ResetColor();
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if ((other.CompareTag("Shield_Tipo1") || other.CompareTag("Shield_Tipo2")) && !colorChanged)
    {
      // Solo cambiar color si es el escudo objetivo
      if ((CompareTag("Humanoide_Tipo1") && moveToShield1 && other.gameObject == shield1) ||
          (CompareTag("Humanoide_Tipo2") && moveToShield2 && other.gameObject == shield2))
      {
        Color colorEscudo = CompareTag("Humanoide_Tipo1") ? Color.green : Color.red;
        CambiarColor(colorEscudo);
        colorChanged = true;
        escudoActual = other.gameObject;
        Debug.Log($"{name} cambió de color al tocar su escudo objetivo");
      }
    }
  }

  void CambiarColor(Color nuevoColor)
  {
    if (humanoidRenderer != null)
    {
      Material m = new Material(humanoidRenderer.material);
      m.color = nuevoColor;
      humanoidRenderer.material = m;
    }
  }

  void DetenerMovimiento()
  {
    moveToShield1 = false;
    moveToShield2 = false;
    if (anim != null)
      anim.SetBool("isWalking", false);
  }

  // Métodos públicos
  public void ReanudarMovimiento()
  {
    if (!haAlcanzadoEscudo)
    {
      if (CompareTag("Humanoide_Tipo1") && shield1 != null && shield1.activeInHierarchy)
      {
        moveToShield1 = true;
        Debug.Log($"{name} reanudó movimiento hacia shield1");
      }
      else if (CompareTag("Humanoide_Tipo2") && shield2 != null && shield2.activeInHierarchy)
      {
        moveToShield2 = true;
        Debug.Log($"{name} reanudó movimiento hacia shield2");
      }
    }
  }

  public bool HaAlcanzadoEscudo()
  {
    return haAlcanzadoEscudo;
  }

  public void ResetColor()
  {
    if (humanoidRenderer != null && colorChanged)
    {
      Material m = new Material(humanoidRenderer.material);
      m.color = originalColor;
      humanoidRenderer.material = m;
      colorChanged = false;
      escudoActual = null;
    }
  }

  public void ResetHumanoide()
  {
    haAlcanzadoEscudo = false;
    ResetColor();
    ReanudarMovimiento();
  }

  void OnDestroy()
  {
    if (notificador != null)
    {
      if (CompareTag("Humanoide_Tipo1"))
      {
        notificador.OnTriggerHumanoide2 -= OnHumanoide2Touched;
      }
      else if (CompareTag("Humanoide_Tipo2"))
      {
        notificador.OnTriggerHumanoide1 -= OnHumanoide1Touched;
      }
    }
  }
}