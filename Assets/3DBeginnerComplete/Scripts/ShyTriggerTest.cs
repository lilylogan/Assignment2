using UnityEngine;

public class ShyTriggerTest : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered: " + other.name + " | Tag: " + other.tag);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Something exited: " + other.name + " | Tag: " + other.tag);
    }
}
