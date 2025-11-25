using UnityEngine;
using System.Collections.Generic;


public class GrassScript : MonoBehaviour
{
   [Header("Grass Settings")]
    public GameObject grassPrefab;      // Your grass sprite prefab
    public float scrollSpeed = 2f;      // Speed of movement
    public int initialGrassCount = 3;   // Number of grass pieces to start with

    private List<GameObject> grassPieces = new List<GameObject>();
    private float grassWidth;
    private float grassHeight;
    private float bottomY;

    void Start()
    {
        if (grassPrefab == null)
        {
            Debug.LogError("Grass prefab not assigned!");
            return;
        }

        // Get width and height of the grass prefab
        SpriteRenderer sr = grassPrefab.GetComponent<SpriteRenderer>();
        grassWidth = sr.bounds.size.x;
        grassHeight = sr.bounds.size.y;

        // Calculate bottom of screen in world units
        bottomY = Camera.main.transform.position.y - Camera.main.orthographicSize + grassHeight / 2;

        // Spawn initial grass pieces side by side
        for (int i = 0; i < initialGrassCount; i++)
        {
            Vector3 pos = new Vector3(i * grassWidth, bottomY, 0);
            GameObject g = Instantiate(grassPrefab, pos, Quaternion.identity, transform);
            grassPieces.Add(g);
        }
    }

    void Update()
    {
        // Move all grass pieces left
        foreach (GameObject g in grassPieces)
        {
            g.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

            // Keep grass at bottom in case camera moves
            g.transform.position = new Vector3(g.transform.position.x, bottomY, g.transform.position.z);
        }

        // Check if the first grass piece went off-screen
        if (grassPieces.Count > 0)
        {
            GameObject first = grassPieces[0];
            float cameraLeftEdge = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;

            if (first.transform.position.x + grassWidth / 2 < cameraLeftEdge)
            {
                // Remove and destroy first piece
                grassPieces.RemoveAt(0);
                Destroy(first);

                // Spawn a new piece at the right of the last piece
                GameObject last = grassPieces[grassPieces.Count - 1];
                Vector3 newPos = new Vector3(last.transform.position.x + grassWidth, bottomY, 0);
                GameObject newGrass = Instantiate(grassPrefab, newPos, Quaternion.identity, transform);
                grassPieces.Add(newGrass);
            }
        }
    }
}
