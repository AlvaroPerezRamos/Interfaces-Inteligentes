/**
  Este script inicializa un vector de 3 posiciones con valores entre 0.0 y 1.0, para tomarlo como un vector de color (Color).
  Cada 120 frames se cambia el valor de una posición aleatoria y se asigna el nuevo color al objeto.
  La cantidad de frames de espera está parametrizado para poderlo cambiar desde el inspector.
*/

using UnityEngine;
public class CambioColor : MonoBehaviour
{
    // Frames de espera antes de cambiar un valor del color
    public int framesDeEspera = 120;

    // Vector de color (R, G, B)
    private float[] colorVector = new float[3];

    // Contador de frames
    private int contadorFrames = 0;

    // Referencia al Renderer del objeto
    private Renderer objRenderer;

    void Start()
    {
        // Inicializa el vector con valores aleatorios entre 0.0 y 1.0
        for (int i = 0; i < 3; i++)
        {
            colorVector[i] = Random.Range(0f, 1f);
        }

        // Obtiene el renderer del objeto
        objRenderer = GetComponent<Renderer>();

        // Asigna el color inicial
        ActualizarColor();
    }

    void Update()
    {
        contadorFrames++;

        // Cuando alcanza la cantidad de frames especificada...
        if (contadorFrames >= framesDeEspera)
        {
            contadorFrames = 0; // reiniciar contador

            // Escoger un índice aleatorio (0=R, 1=G, 2=B)
            int indice = Random.Range(0, 3);

            // Cambiar solo ese valor a otro aleatorio
            colorVector[indice] = Random.Range(0f, 1f);

            // Actualizar color del objeto
            ActualizarColor();
        }
    }

    // Convierte el vector en un Color y lo aplica al material
    void ActualizarColor()
    {
        Color nuevoColor = new Color(colorVector[0], colorVector[1], colorVector[2]);
        objRenderer.material.color = nuevoColor;
    }
}
