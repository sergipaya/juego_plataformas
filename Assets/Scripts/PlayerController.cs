using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float maxSpeed = 5f;
    public bool grounded;
    public float jumpPower=6.5f;
    public AudioSource enemyDestroy;
    public AudioSource dobleSalto;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;
    private bool doubleJump;
    private bool movement = true;
    private SpriteRenderer spr;

    private AudioSource SonidoDeSalto;

    // Start is called before the first frame update
    void Start()
    {
        SonidoDeSalto = GetComponent <AudioSource>();

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        PlayerPrefs.SetInt ("lastLevel", SceneManager.GetActiveScene().buildIndex);

    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);

        if (grounded){
            doubleJump = true;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if (grounded){
                jump=true;
                doubleJump = true;
                // Sonido al saltar
                SonidoDeSalto.Play();
            } else if (doubleJump){
                jump = true;
                doubleJump = false;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                dobleSalto.Play();
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

    }


    void FixedUpdate()
    {

        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;

        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }

        float h = Input.GetAxis("Horizontal");
        if (!movement) h = 0;

        rb2d.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(jump){
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump=false;
        }




    }


    void OnBecameInvisible()
    {
        StartCoroutine(espera());
        IEnumerator espera()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(7);
        }
    }

    public void EnemyJump()
    {
        jump = true;
        enemyDestroy.Play();
    }

    public void EnemyKnockBack(float enemyPosX)
    {
        jump = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);

        movement = false;
        Invoke("EnableMovement", 0.7f);

        Color color = new Color(255 / 255f, 106 / 255f, 0 / 255f);
        spr.color = color;
    }

    void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "CambioEscena")
        {
            movement=false;
        }
    }
}
