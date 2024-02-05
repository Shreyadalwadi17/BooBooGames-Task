using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGameManager : MonoBehaviour
{
    public LayerMask ballLayer; 
    public GameObject linePrefab; 

    private Vector2 dragStartPosition;
    private List<GameObject> connectedBalls = new List<GameObject>(); 
    private bool isDragging;
    private LineRenderer lineRenderer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
            Debug.Log("mouse down");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDragging();
        }

        if (isDragging)
        {
            UpdateDragging();
            Debug.Log("mouse drag");
        }
    }

    void StartDragging()
    {
        dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        CheckConnectedBalls(dragStartPosition);
    }

    void UpdateDragging()
    {
        Vector2 currentDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
        foreach (GameObject ball in connectedBalls)
        {
            Ball ballScript = ball.GetComponent<Ball>();
            ballScript.ShowLine(currentDragPosition);
        }
      
        CheckConnectedBalls(currentDragPosition);
    }

    void EndDragging()
    {
        isDragging = false;
        DestroyConnectedBalls();
    }

    void CheckConnectedBalls(Vector2 position)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, 0.1f, ballLayer);
        foreach (Collider2D col in hitColliders)
        {
            GameObject ball = col.gameObject;
            Ball ballScript = ball.GetComponent<Ball>();
            if (ballScript != null && !connectedBalls.Contains(ball))
            {
                connectedBalls.Add(ball);
                ballScript.ShowLine(position);
            }
        }
    }

    void DestroyConnectedBalls()
    {
        if (connectedBalls.Count > 1)
        {
           
            BallColor referenceColor = GetReferenceColor();
            bool sameColor = true;
            foreach (GameObject ball in connectedBalls)
            {
                Ball ballScript = ball.GetComponent<Ball>();
                if (ballScript.color != referenceColor)
                {
                    sameColor = false;
                    break;
                }
            }

            
            if (sameColor)
            {
                foreach (GameObject ball in connectedBalls)
                {
                    Destroy(ball);
                }
            }
            else
            {
                
                if (lineRenderer != null)
                {
                    lineRenderer.enabled = false;
                }
            }
        }
        connectedBalls.Clear();
    }

    BallColor GetReferenceColor()
    {
        if (connectedBalls.Count > 0)
        {
            Ball firstBallScript = connectedBalls[0].GetComponent<Ball>();
            return firstBallScript.color;
        }
        else
        {
            
            return BallColor.Blue;
        }
    }
}