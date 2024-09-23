using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource source;

    Collider soundTrigger;

    void Awake(){

        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            source.Play();
        }
    }

}
