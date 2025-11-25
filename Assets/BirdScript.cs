using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float flapVelocity;
    public LogicScript logicScript;
    public bool birdAlive = true;
    public AudioSource flapAudio;
    public AudioSource deathAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        flapAudio = logicScript.flapSound;
        deathAudio = logicScript.deathSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdAlive)
        {
            rigidBody.linearVelocity = Vector2.up * flapVelocity;
            flapAudio.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected");
        if (birdAlive)
        {
            deathAudio.Play();
        }
        logicScript.gameOver();
        birdAlive = false;
    }
}
