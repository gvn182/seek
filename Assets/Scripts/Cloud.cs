using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour
{

    //Use this for initialization
    public float MySpeed;
    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {
        CheckDirection();
        Move();
    }

    private void Move()
    {
        this.transform.position = new Vector2(transform.position.x - MySpeed * Time.deltaTime, transform.position.y);
    }

    private void CheckDirection()
    {
        
        if (transform.position.x < -4)
        {
            transform.position = new Vector2(4, transform.position.y);
        }

    }
}
