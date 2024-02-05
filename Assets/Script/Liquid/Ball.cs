using UnityEngine;
public enum BallColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Purple
}


public class Ball : MonoBehaviour
{
   public BallColor color;
    public LineRenderer lineRenderer;

    public void SetColor(BallColor newColor)
    {
        color = newColor;
   
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (newColor)
        {
            case BallColor.Red:
                spriteRenderer.color = Color.red;
                break;
            case BallColor.Blue:
                spriteRenderer.color = Color.blue;
                break;
            case BallColor.Green:
                spriteRenderer.color = Color.green;
                break;
            case BallColor.Yellow:
                spriteRenderer.color = Color.yellow;
                break;
            case BallColor.Purple:
                spriteRenderer.color = new Color(0.6f, 0, 0.8f); 
                break;
        }
    }

     public void ShowLine(Vector2 endPosition)
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPosition);
        }
    }

    public void HideLine()
    {
        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }
}
