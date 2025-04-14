using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().Play();
    }
}
