using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    
    private void OggerEnter(Collider other)
    {
        audioSource.Play();
    }
}
