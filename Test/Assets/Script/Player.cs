using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 100f, maxspeed = 3, maxJump = 4, jumpPow = 250f;
    public bool grounded = true, faceRight = true, doubleJump = false;


    public int ourHealth;
    public int maxHealh;

    public Rigidbody2D r2;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        r2 = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("grounded", grounded);
        anim.SetFloat("speed", Mathf.Abs(r2.velocity.x)); // trả về gt dương

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grounded)
            {
                grounded = false;
                doubleJump = true;
                r2.AddForce(Vector2.up * jumpPow);
            }
            else
            {
                if(doubleJump)
                {
                    doubleJump = false;
                    r2.velocity = new Vector2(r2.velocity.x, 0);
                    r2.AddForce(Vector2.up * jumpPow * 0.5f);
                }
            }
        }

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        r2.AddForce((Vector2.right) * speed * h);

        if (r2.velocity.x > maxspeed)
            r2.velocity = new Vector2(maxspeed, r2.velocity.y);
        if (r2.velocity.x < -maxspeed)
            r2.velocity = new Vector2(-maxspeed, r2.velocity.y);

        if (r2.velocity.y > maxJump)
            r2.velocity = new Vector2(r2.velocity.x, maxJump);
        if (r2.velocity.y < -maxJump)
            r2.velocity = new Vector2(r2.velocity.x, -maxJump);

        if (h>0 && !faceRight)
        {
            Flip();
        }
        if(h<0 && faceRight)
        {
            Flip();
        }

        if(grounded)
        {
            r2.velocity = new Vector2(r2.velocity.x * 0.7f, r2.velocity.y);
        }
    }

    public void Flip()
    {
        faceRight = !faceRight;
        Vector3 Scale;
        Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Damage(int damage)
    {
        ourHealth -= damage;
    }

    public void KnockBack(float knockPow, Vector2 knockDir)
    {
        r2.velocity = new Vector2(0, 0);
        r2.AddForce(new Vector2(knockDir.x * -100, knockDir.y * knockPow));
    }
}
