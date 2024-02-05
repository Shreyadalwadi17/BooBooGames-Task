using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour
{
    
  public ParticleSystem particle;
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            gameObject.SetActive(false); 
              particle.Play();
           
        }
    }
}