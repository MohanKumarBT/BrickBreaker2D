using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float force;
    public Transform explosion;
    public Transform explosion1;
    public Transform powerUp;
    public GameManager gameManager;
    private bool gameover;
    public AudioSource Audio;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        
        BallPosition();
        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * force);
            
        }
    }
    public void BallPosition()
    {
        if (!inPlay)
        {
            transform.position = paddle.position;
            force = 0;
            force = 500;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bottom"))
        {
           
            rb.velocity = Vector2.zero;
            inPlay = false;
            gameManager.UpdateLives(-1);
        }  
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("brick"))
        {
            BrickController brickController = other.gameObject.GetComponent<BrickController>();
            if (brickController.hitsToBreak > 1)
            {
                brickController.BreakBrick();
            }
            else
            {
                int randchance = Random.Range(1, 300);
                if (randchance < 50)
                {
                    Instantiate(powerUp, other.transform.position, other.transform.rotation);
                }



                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);
                gameManager.UpdateScore(other.gameObject.GetComponent<BrickController>().points);
                gameManager.UpdateNumberofBricks();
                Destroy(other.gameObject);
            }
            Audio.Play ();
        }

        if (other.transform.CompareTag("specialbrick"))
        {
            BrickController brickController = other.gameObject.GetComponent<BrickController>();
            if (brickController.hitsToBreak > 1)
            {
                brickController.BreakBrick();
            }
            else
            {
                int randchance = Random.Range(1, 300);
                if (randchance < 50)
                {
                    Instantiate(powerUp, other.transform.position, other.transform.rotation);
                }



                Transform newExplosion1 = Instantiate(explosion1, other.transform.position, other.transform.rotation);
                Destroy(newExplosion1.gameObject, 2.5f);
                gameManager.UpdateScore(other.gameObject.GetComponent<BrickController>().points);
                gameManager.UpdateNumberofBricks();
                Destroy(other.gameObject);
            }
            Audio.Play();
        }
    }
}
