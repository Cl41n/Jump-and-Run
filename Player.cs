using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public static Player playerInstance;
    
    public bool grounded;
    public float start_speed;
    public float max_speed;
    public float jump_Force;
    
    public bool right;
    private SpriteRenderer mySpriteRenderer;
    Rigidbody2D rb;

    public Text points;
    public float speed;
    public int coinsCollected;

    public int playerLifes = 3;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public bool inviciblityTimer;

    // Start is called before the first frame update
    void Start()
    {
        playerInstance = this;
        playerLifes = 3;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
            float x_movement = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(x_movement, 0);
            rb.velocity = new Vector2(x_movement * speed, rb.velocity.y);
            gameObject.tag = "Player";


        if (x_movement == 0)
        {
            GetComponent<Animator>().SetBool("fast_af", false);
            GetComponent<Animator>().SetBool("run", false);
            speed = start_speed;
        }

        else if (x_movement > 0)
        {
            mySpriteRenderer.flipX = false;
            if (speed < max_speed) 
            {
                gameObject.tag = "Player";
                speed += 0.05f;
                GetComponent<Animator>().SetBool("run", true);
            }
            
            if(speed >= max_speed) 
            {
                gameObject.tag = "Ball";
                GetComponent<Animator>().SetBool("fast_af", true);
            }
        }

        else if (x_movement < 0)
        {
            mySpriteRenderer.flipX = true;
            if (speed < max_speed)
            {
                gameObject.tag = "Player";
                speed += 0.05f;
                GetComponent<Animator>().SetBool("run", true);
            }
            
            if (speed >= max_speed)
            {
                gameObject.tag = "Ball";

                GetComponent<Animator>().SetBool("fast_af", true);
            }
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, 1 * jump_Force);
            grounded = false;
        }

        points.text = coinsCollected.ToString();

        if(playerLifes == 3)
        {
            
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
        }
        else if(playerLifes == 2)
        {
            
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(false);
        }
        else if (playerLifes == 1)
        {
            life1.SetActive(true);
            life2.SetActive(false);
            life3.SetActive(false);
        }
        else if (playerLifes == 0)
        {

            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            coinsCollected += 1;
        }
    }

}
