/**
  Este script crea dos variables Vector3 públicas.
  Permite darles valor desde el inspector.
  Muestra por consola:
    - La magnitud de cada vector.
    - El ángulo que forman.
    - La distancia entre ambos.
    - Un mensaje indicando qué vector está a una altura mayor.
    - Muestra en el inspector cada uno de esos valores.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompararVectores : MonoBehaviour {
    /// Vectores 3D que se pueden modificar desde el inspector
    [SerializeField] private Vector3 _primerVector;
    [SerializeField] private Vector3 _segundoVector;
    /// Variables para mostrar los resultados en el inspector
    [SerializeField] private float _magnitudPrimerVector;
    [SerializeField] private float _magnitudSegundoVector;
    [SerializeField] private float _anguloEntreVectores;
    [SerializeField] private float _distanciaEntreVectores;
    [SerializeField] private string _alturaMayorMensaje;

    /// Función que calcula la magnitud de un vector
    private float Magnitud(Vector3 vector) {
        /// Usamos la función Magnitude de la clase Vector3
        return vector.magnitude;
    }

    /// Función que calcula el ángulo entre dos vectores
    private float Angulo(Vector3 primerVector, Vector3 segundoVector) {
        /// Usamos la función Angle de la clase Vector3
        return Vector3.Angle(primerVector, segundoVector);
    }

    /// Función que calcula la distancia entre dos vectores
    private float Distancia(Vector3 primerVector, Vector3 segundoVector) {
        /// Usamos la función Distance de la clase Vector3
        return Vector3.Distance(primerVector, segundoVector);
    }

    /// Función que indica cuál de los dos vectores está a una altura mayor
    private string AlturaMayor(Vector3 primerVector, Vector3 segundoVector) {
        /// Comparamos la coordenada Y de ambos vectores
        if (primerVector.y > segundoVector.y) {
            return "El primer vector está a una altura mayor";
        } else if (primerVector.y < segundoVector.y) {
            return "El segundo vector está a una altura mayor";
        } else {
            return "Ambos vectores están a la misma altura";
        }
    }

    /// Muestra por consola los resultados
    private void MostrarResultados() {
        Debug.Log("Magnitud del primer vector: " + _magnitudPrimerVector);
        Debug.Log("Magnitud del segundo vector: " + _magnitudSegundoVector);
        Debug.Log("Ángulo entre vectores: " + _anguloEntreVectores);
        Debug.Log("Distancia entre vectores: " + _distanciaEntreVectores);
        Debug.Log(_alturaMayorMensaje);
    }

    // Start is called before the first frame update
    void Start() {
        /// Damos valores iniciales aleatorios a los vectores
        _primerVector = new Vector3(Random.value, Random.value, Random.value);
        _segundoVector = new Vector3(Random.value, Random.value, Random.value);
        /// Calculamos lo que pide el ejercicio
        _magnitudPrimerVector = Magnitud(_primerVector);
        _magnitudSegundoVector = Magnitud(_segundoVector);
        _anguloEntreVectores = Angulo(_primerVector, _segundoVector);
        _distanciaEntreVectores = Distancia(_primerVector, _segundoVector);
        _alturaMayorMensaje = AlturaMayor(_primerVector, _segundoVector);
        /// Mostramos los resultados por consola
        MostrarResultados();
    }

    // Update is called once per frame
    void Update() {
        
    }
}