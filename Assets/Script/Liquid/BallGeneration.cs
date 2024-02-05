using UnityEngine;

public class BallGeneration: MonoBehaviour
{
    public GameObject ballPrefab;
    public int rows = 5;
    public int columns = 5;
    public float spacing = 1.2f;
    public Transform parentObject;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        if(parentObject == null)
        {
            Debug.LogError("Parent object is not assigned.");
            return;
        }

        float startX = -((columns - 1) * spacing) / 2;
        float startY = ((rows - 1) * spacing) / 2;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(startX + col * spacing, startY - row * spacing, 0f);
                GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity, parentObject);
                Ball ballScript = ball.GetComponent<Ball>();
                ballScript.SetColor((BallColor)Random.Range(0, System.Enum.GetValues(typeof(BallColor)).Length));
            }
        }
    }
}