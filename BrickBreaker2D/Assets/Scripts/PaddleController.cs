using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;
    public float rightscreenedge;
    public float leftscreenedge;
    public GameManager gameManager;
    void Start()
    {
        
    }

    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if(transform.position.x < leftscreenedge)
        {
            transform.position = new Vector2(leftscreenedge, transform.position.y);
        }
        if (transform.position.x > rightscreenedge)
        {
            transform.position = new Vector2(rightscreenedge, transform.position.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("extralife"))
        {
            gameManager.UpdateLives(1);
            Destroy(other.gameObject);
        }
    }
}
