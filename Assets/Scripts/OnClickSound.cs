using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioClip clickSound; 
    private AudioSource audioSource;

    void Start()
    {
        // Agregar un AudioSource si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clickSound;
        audioSource.playOnAwake = false; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            audioSource.Play(); 
        }
    }
}
