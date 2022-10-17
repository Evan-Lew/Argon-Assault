using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathFX;
    [SerializeField] private GameObject hitVFX;
    
    [SerializeField] private int scorePerHit = 15;
    [SerializeField] private int hitPoints = 4;
    
    private ScoreBoard _scoreBoard;
    private GameObject _parentGameObject;
    private void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        _parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    private void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(name + "destroyed by " + other.gameObject.name);
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();    
        }
    }

    private void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = _parentGameObject.transform;
        hitPoints--;
    }

    private void KillEnemy()
    {
        _scoreBoard.IncreaseScore(scorePerHit);
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = _parentGameObject.transform;
        Destroy(gameObject);
    }
}
