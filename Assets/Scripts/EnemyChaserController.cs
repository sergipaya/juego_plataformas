using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserController : MonoBehaviour
{
    public float visionRadius;
    public float speed;
    private Rigidbody2D rb2d;

    private AudioSource SonidoDestroy;

    GameObject player;
    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        SonidoDestroy = GetComponent <AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 target = initialPosition;

        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < visionRadius) target = player.transform.position;

        float fixedSpeed = speed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SonidoDestroy.Play();
            float yOffset = 0.3f;
            if (transform.position.y + yOffset < col.transform.position.y)
            {
                col.SendMessage("EnemyJump");
                Destroy(gameObject);
            }
            else
            {
                col.SendMessage("EnemyKnockBack", transform.position.x);
            }
        }
        if (col.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
