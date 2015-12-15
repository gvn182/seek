using UnityEngine;
using System.Collections;

public class Baloon1 : MonoBehaviour
{

    // Use this for initialization
    bool GoingUp = false;
    bool GoingBot = true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Move();
    }

    private void Move()
    {
        if (GoingBot)
            this.transform.position = new Vector2(transform.position.x, transform.position.y - 3f * Time.deltaTime);
        else
            this.transform.position = new Vector2(transform.position.x, transform.position.y + 3f * Time.deltaTime);

        if (this.transform.position.y > 5)
        {
            GoingBot = true;
            GoingUp = false;
        }
        if (this.transform.position.y < -4)
        {
            GoingBot = false;
            GoingUp = true;
        }
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collided me");
        this.gameObject.SetActive(false);
    }
}
