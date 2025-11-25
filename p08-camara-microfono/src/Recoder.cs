using UnityEngine;

public class Recorder : MonoBehaviour
{
    private AudioSource audioSource;
    private string micName;
    private bool isRecording = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (Microphone.devices.Length > 0)
        {
            micName = Microphone.devices[0];
            Debug.Log("Micrófono encontrado: " + micName);
        }
        else
        {
            Debug.LogError("No se detectaron micrófonos.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isRecording)
        {
            Debug.Log("Iniciando grabación...");
            audioSource.clip = Microphone.Start(micName, false, 10, 44100);
            isRecording = true;
        }

        if (Input.GetKeyUp(KeyCode.R) && isRecording)
        {
            Debug.Log("Grabación detenida. Reproduciendo...");
            Microphone.End(micName);
            audioSource.Play();
            isRecording = false;
        }
    }
}
