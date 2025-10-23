/*
  Teleport.cs
  Maneja el teletransporte de humanoides a escudos específicos en respuesta a eventos del Notificador.
*/

using UnityEngine;

[RequireComponent(typeof(ComportamientoHumanoide))]
public class Teleport : MonoBehaviour
{
  public Notificador4 notificador;
  public float rotSpeed = 4f;
  public Transform objetoOrientacion; // Para grupo 2

  private bool teletransportado = false;
  private Animator anim;
  private Renderer humanoidRenderer;
  private Color originalColor;
  private ComportamientoHumanoide comportamiento; // Referencia al script existente

  void Start()
  {
    // Obtener referencia al ComportamientoHumanoide que ya tiene los escudos asignados
    comportamiento = GetComponent<ComportamientoHumanoide>();

    anim = GetComponent<Animator>();
    anim.applyRootMotion = false;

    humanoidRenderer = GetComponentInChildren<Renderer>();
    if (humanoidRenderer != null)
      originalColor = humanoidRenderer.material.color;

    if (notificador == null)
    {
      Debug.LogError($"{name} no tiene asignado el Notificador4 en el inspector.");
      return;
    }

    // Suscribirse según el tipo
    if (CompareTag("Humanoide_Tipo1"))
    {
      notificador.OnAproximacionObjetoReferencia1 += OnAproximacionObjetoReferencia1;
    }
    else if (CompareTag("Humanoide_Tipo2"))
    {
      notificador.OnAproximacionObjetoReferencia2 += OnAproximacionObjetoReferencia2;
    }
  }

  void Update()
  {
    // Grupo 2 siempre se orienta hacia el objeto de referencia si existe
    if (CompareTag("Humanoide_Tipo2") && objetoOrientacion != null)
    {
      OrientarHaciaObjeto();
    }
  }

  void OrientarHaciaObjeto()
  {
    Vector3 direction = objetoOrientacion.position - transform.position;
    direction.y = 0;
    if (direction.magnitude > 0.1f)
    {
      Quaternion lookRot = Quaternion.LookRotation(direction);
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotSpeed);
    }
  }

  void OnAproximacionObjetoReferencia1()
  {
    // Grupo 1: Teletransporte a shield1 (usando el escudo ya asignado en ComportamientoHumanoide)
    if (CompareTag("Humanoide_Tipo1") && comportamiento.shield1 != null && !teletransportado)
    {
      transform.position = comportamiento.shield1.transform.position;
      teletransportado = true;
      Debug.Log($"{name} (T1) teletransportado a Shield1");
    }
  }

  void OnAproximacionObjetoReferencia2()
  {
    // Grupo 2: Teletransporte a shield2 (usando el escudo ya asignado en ComportamientoHumanoide)
    if (CompareTag("Humanoide_Tipo2") && comportamiento.shield2 != null && !teletransportado)
    {
      transform.position = comportamiento.shield2.transform.position;
      teletransportado = true;
      Debug.Log($"{name} (T2) teletransportado a Shield2");
    }
  }

  // --- 🎨 CAMBIO DE COLOR (opcional, si quieres mantenerlo) ---
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Shield_Tipo1"))
    {
      CambiarColor(Color.green);
      Debug.Log($"{name} cambió de color al tocar un escudo tipo 1");
    }
    if (other.CompareTag("Shield_Tipo2"))
    {
      CambiarColor(Color.red);
      Debug.Log($"{name} cambió de color al tocar un escudo tipo 2");
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Shield_Tipo1") || other.CompareTag("Shield_Tipo2"))
    {
      CambiarColor(originalColor);
      Debug.Log($"{name} volvió a su color original");
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
}