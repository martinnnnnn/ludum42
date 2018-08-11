using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{

    bool OnGround = false;
    private bool OnWall = false;
    //bool HasDoubleJumped = false;
    //bool HasWallJumped = false;
    private Vector2 wallNormal = Vector2.zero;

    public bool aircontrol = true;

    public float jumpForce = 7;
    public float doubleJumpForce = 3;
    public float groundSpeed = 2;
    public float groundAcceleration = 0.5f;
    public float airSpeed = 2;
    public float airAcceleration = 0.01f;
    public float wallJumpForce = 5f;

    public static Rigidbody2D rigid;
    public static PlayerController instance;

    private bool hasReset = false;

    //private MovingPlateform onMP;

    //public Animator anim;
    public SpriteRenderer srend;


    // Use this for initialization
    void Start()
    {
        instance = this;
        rigid = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {


        if (true)
        {
            Vector2 direction = rigid.velocity;
            if (OnGround)
            {
                direction.x = Input.GetAxisRaw("Horizontal") * groundAcceleration;
                if (direction.x > groundSpeed)
                {
                    direction.x = groundSpeed;
                }
                else if (direction.x < -groundSpeed)
                {
                    direction.x = -groundSpeed;
                }

            }
            else if (aircontrol)
            {
                direction.x += Input.GetAxisRaw("Horizontal") * airAcceleration;
                if (direction.x > airSpeed)
                {
                    direction.x = airSpeed;
                }
                else if (direction.x < -airSpeed)
                {
                    direction.x = -airSpeed;
                }
            }


            if (/*Input.GetAxisRaw("Vertical") > 0.5 ||*/ Input.GetButtonDown("Jump"))
            {
                //--------------------------------------------
                //  ON GROUND && WALL
                if (OnGround && OnWall)
                {
                    direction.y = jumpForce;
                    //SoundManager.Instance.PlayFromPool(SoundType.JUMP);
                }
                //--------------------------------------------
                //  ON GROUND
                else if (OnGround)
                {
                    direction.y = jumpForce;
                    //SoundManager.Instance.PlayFromPool(SoundType.JUMP);
                }

                //--------------------------------------------
                //  ON WALL
                //else if (OnWall && !HasWallJumped)
                //{
                //    Vector2 dir = (Vector2.up + wallNormal) * wallJumpForce;
                //    direction.y = dir.y;
                //    direction.x += dir.x;
                //    HasWallJumped = true;
                //    //SoundManager.Instance.PlayFromPool(SoundType.JUMP);
                //}

                //--------------------------------------------
                //  DOUBLE JUMP
                //else if (!HasDoubleJumped)
                //{
                //    Vector2 dir = doubleJumpForce * (Vector2.up);
                //    direction.y = dir.y;
                //    direction.x += dir.x;
                //    HasDoubleJumped = true;
                //    //SoundManager.Instance.PlayFromPool(SoundType.JUMP);
                //}

            }
            else
                hasReset = true;


            rigid.velocity = direction;
        }

        //if (onMP)
        //{
        //    transform.position += onMP.direction * Time.deltaTime;
        //}

        //anim.SetFloat("Speed", Mathf.Abs(rigid.velocity.x));
        //if (Mathf.Abs(rigid.velocity.x) > 0.1)
        //{
        //    srend.flipX = rigid.velocity.x < 0;
        //}
        //anim.SetFloat("YSpeed", rigid.velocity.y);
        //anim.SetBool("OnGround", OnGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 norm = collision.contacts[0].normal;
        norm.x = Mathf.Abs(norm.x);
        norm.y = Mathf.Abs(norm.y);
        if (Vector2.Dot(Vector2.up, collision.contacts[0].normal) > 0.6f) // Hit Ground
        {
            OnGround = true;
            //SoundManager.Instance.PlayFromPool(SoundType.LAND);
            //HasDoubleJumped = false;
            //HasWallJumped = false;
        }
        else if (Vector2.Dot(Vector2.right, norm) > 0.6f)
        {
            //if (collision.contacts[0].normal.x * wallNormal.x <= 0)
            //{
            //    HasWallJumped = false;
            //}
            wallNormal = collision.contacts[0].normal;
            OnWall = true;
            //SoundManager.Instance.PlayFromPool(SoundType.LAND);
        }

        //MovingPlateform mp = collision.collider.GetComponent<MovingPlateform>();
        //if (mp)
        //{
        //    onMP = mp;
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Vector2 direction = rigid.velocity;
        if (Mathf.Abs(direction.y) > 0.1f)
        {
            OnGround = false;
        }
        if (Mathf.Abs(direction.x) > 0.1f)
        {
            OnWall = false;
        }

        //MovingPlateform mp = collision.collider.GetComponent<MovingPlateform>();
        //if (mp)
        //{
        //    onMP = null;
        //}
    }
}