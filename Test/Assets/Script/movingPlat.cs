using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlat : MonoBehaviour
{
    public float speed = 0.05f, changeDirection = -1;
    Vector3 move;
    public pausedUI pausel;
    // Start is called before the first frame update
    void Start()
    {
        move = this.transform.position;
        pausel = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInParent<pausedUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pausel.pause)
        {
            this.transform.position = this.transform.position;
        }
        if(pausel.pause == false)
        {
            move.x += speed;
            this.transform.position = move;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("grounded"))
        {
            speed *= changeDirection;
        }
    }
}
