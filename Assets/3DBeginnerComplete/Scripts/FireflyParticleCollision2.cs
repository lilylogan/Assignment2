using UnityEngine;

public class FireflyParticleCollision2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   /* void Start()
    {
        
    }*/
    
     [SerializeField] public ParticleSystem particleSystemToTrigger;

    private void Start()
    {
        
        GetComponent<Collider>().isTrigger = true;

        if(particleSystemToTrigger != null)
        {
            Debug.Log("in start");
            particleSystemToTrigger.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        

        }
        else{

             
            Debug.LogError("ParticleSystemToTrigger is not assigned!", this);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on trigger before f");
        Debug.Log($"Trigger entered by: {other.name}", other);
        if(other.CompareTag("Head")) // need to put a tag on head
        {    
            if (particleSystemToTrigger != null)
            {
                Debug.Log("on trigger before play");
                particleSystemToTrigger.Play();

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Head"))
        {
            if(particleSystemToTrigger != null)
            {
                particleSystemToTrigger.Stop();
            }

        }

    }


}
