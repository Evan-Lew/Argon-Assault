using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    /*
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.name + "--Collided with--" + other.gameObject.name);
    }
    */
    [SerializeField] private float loadDelay = 1f;
    [SerializeField] private ParticleSystem crashVFX;
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{this.name} **Triggered by** {other.gameObject.name}");
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
