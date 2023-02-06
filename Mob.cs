using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mob : MonoBehaviour
{

    public float speed;
    public bool MoveRight;
    public bool shell;

    private SpriteRenderer mySpriteRenderer;
    Rigidbody2D rb;

    public Transform groundDetection;

    public float distance;

    [SerializeField]
    float invicibilityTime;



    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (MoveRight && shell == false)
        {
            GetComponent<Animator>().SetBool("mob_shell", false);
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
            GetComponent<Animator>().SetBool("mob_run", true);
        }
        else if (MoveRight == false && shell == false)
        {
            GetComponent<Animator>().SetBool("mob_shell", false);
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, 1);
            GetComponent<Animator>().SetBool("mob_run", true);
        }
        else if (shell == true)
        {
            GetComponent<Animator>().SetBool("mob_shell", true);
        }

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector3.down, distance);
        if (groundInfo == false)
        {
            if (MoveRight == true)
            {
                transform.Translate(2 * Time.deltaTime * speed, 0, 0);
                transform.localScale = new Vector2(1, 1); ;
                MoveRight = false;
            }
            else
            {
                transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
                transform.localScale = new Vector2(-1, 1);
                MoveRight = true;
            }
        }



    }
    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("turn"))
        {

            if (MoveRight)
            {
                MoveRight = false;
            }
            else
            {
                MoveRight = true;
            }
        }

        if (trig.gameObject.tag == "Player")
        {
            shell = true;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0 && !shell)
        {
            Destroy(gameObject);
        }

        if (MoveRight == true)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(1, 1);
            MoveRight = false;
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-1, 1);
            MoveRight = true;
        }

        if (col.gameObject.tag == "Player"  && Player.playerInstance.inviciblityTimer == false && Player.playerInstance.grounded == true)
        {
            Player.playerInstance.playerLifes--;
            Player.playerInstance.inviciblityTimer = true;
            StartCoroutine(invicible());

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shell = false;
        }
    }

    IEnumerator invicible()
    {
        yield return new WaitForSeconds(invicibilityTime);
        Player.playerInstance.inviciblityTimer = false;
    }

}