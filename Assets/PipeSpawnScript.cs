using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{

    public GameObject pipe;
    public float spawnInterval = 2f;
    private float timer;
    public float heightOffset = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnInterval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0f;
        }
    }

    void spawnPipe()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;

        float lowestPoint = -camHeight + 1f;
        float highestPoint = camHeight - 1f;

        float y = Random.Range(lowestPoint, highestPoint);

        Instantiate(pipe, new Vector3(transform.position.x, y, 0), transform.rotation);
    }
}
