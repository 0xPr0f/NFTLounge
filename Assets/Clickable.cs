using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
   // public AudioClip audio;
    public AudioSource audioSource;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMouseDown()
    {
        if(audioSource.isPlaying == false)
        {
            audioSource.Play();
        }
         else if(audioSource.isPlaying == true)
        {
            audioSource.Stop();
        }   
        
        //audioSource.PlayOneShot(audio);
        Debug.Log("clicked");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
