/*
Crea un script asociado al cubo que en cada iteración traslade al cubo una cantidad proporcional un vector que indica la dirección del movimiento: moveDirection que debe poder modificarse en el inspector.  La velocidad a la que se produce el movimiento también se especifica en el inspector, con la propiedad speed. Inicialmente la velocidad debe ser mayor que 1 y el cubo estar en una posición y=0. En el informe de la práctica comenta los resultados que obtienes en cada una de las siguientes situaciones:
a) duplicas las coordenadas de la dirección del movimiento.
b) duplicas la velocidad manteniendo la dirección del movimiento.
c) la velocidad que usas es menor que 1
d) la posición del cubo tiene y>0
e) intercambiar movimiento relativo al sistema de referencia local y el mundial.
*/
using UnityEngine;

public class MovimientoCubo : MonoBehaviour
{
  [Header("Parámetros configurables desde el Inspector")]
  // Vector que define la dirección del movimiento
  public Vector3 moveDirection = new Vector3(1f, 0f, 0f);

  // Velocidad del movimiento (mayor que 1 inicialmente)
  public float speed = 2f;

  [Header("Configuración del sistema de referencia")]
  // Si es true, se moverá en el sistema local (respecto a la orientación del cubo)
  // Si es false, se moverá en coordenadas globales (mundo)
  public bool usarEspacioLocal = false;

  void Start()
  {
    // Asegurar que el cubo empiece con y = 0 (como indica el enunciado)
    Vector3 pos = transform.position;
    pos.y = 0f;
    transform.position = pos;
  }

  void Update()
  {
    // Movimiento proporcional a la dirección y a la velocidad
    // Multiplicamos por deltaTime para hacerlo independiente del frame rate
    Vector3 desplazamiento = moveDirection * speed * Time.deltaTime;

    // Aplicar el movimiento
    if (usarEspacioLocal)
    {
      // Movimiento relativo al sistema de referencia local
      transform.Translate(desplazamiento, Space.Self);
    }
    else
    {
      // Movimiento en el sistema de coordenadas global
      transform.Translate(desplazamiento, Space.World);
    }
  }
}
