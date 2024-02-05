using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 mouseDownPos;
    private Vector3 mouseUpPos;

    public LineRenderer lineRenderer;
    private const int initialCapacity = 100; 
    private Vector3[] linePositions = new Vector3[initialCapacity]; 
    private int lineIndex = 0;

    public Color fillColor = Color.green;

    public GameObject filledAreaParent;

    
    public Collider2D filledAreaCollider;

 
    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
        mouseDownPos = GetMouseWorldPos();

        lineIndex = 0;
        UpdateLine(mouseDownPos);
    }

    void OnMouseUp()
    {
        isDragging = false;
        mouseUpPos = GetMouseWorldPos();

        FillArea();
    }

    private void Update()
    {
        if (isDragging)
        {
            UpdateLine(GetMouseWorldPos());
            MoveSquareWithMouse();
        }
    }
 private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == filledAreaCollider)
        {
            ChangeColorToRed();
        }
    }

    private void ChangeColorToRed()
    {
      
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void UpdateLine(Vector3 newPos)
    {
     
        if (lineIndex >= linePositions.Length)
        {
            ResizeLinePositions();
        }

        linePositions[lineIndex++] = newPos;
        lineRenderer.positionCount = lineIndex;
        lineRenderer.SetPositions(linePositions);
    }

    private void MoveSquareWithMouse()
    {
        Vector3 mousePos = GetMouseWorldPos();
        transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, transform.position.z);
    }

    private void ResizeLinePositions()
    {
     
        int newCapacity = linePositions.Length * 2;
        Vector3[] newLinePositions = new Vector3[newCapacity];
        linePositions.CopyTo(newLinePositions, 0);
        linePositions = newLinePositions;
    }

    private void FillArea()
    {
        if (lineIndex < 2)
        {
            Debug.LogWarning("Need at least 3 points to form a closed shape.");
            return;
        }

       
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(mouseDownPos.x, mouseDownPos.y, transform.position.z);
        vertices[1] = new Vector3(mouseUpPos.x, mouseDownPos.y, transform.position.z);
        vertices[2] = new Vector3(mouseUpPos.x, mouseUpPos.y, transform.position.z);
        vertices[3] = new Vector3(mouseDownPos.x, mouseUpPos.y, transform.position.z);

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;

    
        int[] triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        mesh.triangles = triangles;

      
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = fillColor;
        }
        mesh.colors = colors;

        GameObject filledArea = new GameObject("FilledArea");
          filledArea.layer = LayerMask.NameToLayer("PlayerEnemyCollision"); 
        filledArea.transform.SetParent(filledAreaParent.transform); 
        filledArea.AddComponent<MeshFilter>().mesh = mesh;
        filledArea.AddComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default")); 
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; 
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}