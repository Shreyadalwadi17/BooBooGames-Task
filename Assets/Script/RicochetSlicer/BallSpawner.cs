using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; 
    public int poolSize = 10; 
    public float ballSpeed = 10f;

    private List<GameObject> ballPool;

    private void Start()
    {
       
        ballPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.SetActive(false);
            ballPool.Add(ball);
        }
    }

    private void Update()
    {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

       
        if (Input.GetMouseButtonDown(0))
        {
            ShootBall();
        }
    }

    void ShootBall()
    {
       
        GameObject ball = GetInactiveBallFromPool();
        if (ball == null)
            return;

        
        ball.transform.position = transform.position;
        ball.transform.rotation = transform.rotation;

        
        ball.SetActive(true);
        
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        
       
        ball.GetComponent<Rigidbody2D>().velocity = direction * ballSpeed;
    }

    GameObject GetInactiveBallFromPool()
    {
        
        foreach (GameObject ball in ballPool)
        {
            if (!ball.activeInHierarchy)
                return ball;
        }

       
        foreach (GameObject ball in ballPool)
        {
            ball.SetActive(false);
        }

        
        return ballPool[0];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Square"))
        {
            other.gameObject.SetActive(false); 
           
        }
    }
}