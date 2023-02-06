using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_trigger_kill : MonoBehaviour
{
    [SerializeField]
    float invicibilityTime;

    private void Start()
    {
        invicibilityTime = 0.7f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ball"))
        {

            Player.playerInstance.playerLifes--;
            Player.playerInstance.inviciblityTimer = true;
            StartCoroutine(invicible());
        }
    }

    IEnumerator invicible()
    {
        yield return new WaitForSeconds(invicibilityTime);
        Player.playerInstance.inviciblityTimer = false;
    }


}
