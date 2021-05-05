using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(6, 4);
    public Vector2 GridOffset = new Vector2(1.0f, 1.0f);
    public GameObject enemyPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                var offset = new Vector3(x * GridOffset.x, y * GridOffset.y, 0f);
                Instantiate(enemyPrefab, transform.position + offset, Quaternion.identity, transform);
            }
        }
    }
}
