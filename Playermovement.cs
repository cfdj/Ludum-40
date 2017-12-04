using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour {
    public float speed;
    private float hspeed;
    private float vspeed;
    //private float jump;
    public Rigidbody2D rb;
    public GameObject Player;
    public GameObject Projectile;
    private Shirtcontrol shirtcontrol;
    public bool onGround;
    private float time;
    public float reload;
    public int Projvelocity;
    private Vector2 facing;
    private string looking;
    private string firingshirt;
    // Use this for initialization
    void Start() {
        looking = "left";
        time = 0;
        vspeed = 30;
        Player = gameObject;
        rb = Player.GetComponent<Rigidbody2D>();
        shirtcontrol = Player.GetComponent<Shirtcontrol>();
    }
    //Checks if on the ground, if yes the player jumps when space is pressed
    void Jump()
    {
        if (onGround)
        {
            rb.AddForce(new Vector2(rb.velocity.x, vspeed));
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate() {
        hspeed = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(hspeed * speed, rb.velocity.y));
        if (Input.GetButton("Jump"))
        {
            Jump();
        }
        if (Input.GetButton("Fire2"))
        {
            fire();
        }
        facing = new Vector2(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"));
        looking = Direction(facing);
    }
    string Direction(Vector2 look)
    {
        string templook = "";
        int x = (int)look.x;
        int y = (int)look.y;
        if((x == 0 ) && (y ==0))
        {
            return looking;
        }
        switch (x) {
            case 1:
                templook = "right";
                break;
            case 0:
                templook = "";
                break;
            case -1:
                templook = "left";
                break;
        }
        switch (y) {
            case 1:
                return "up" + templook;
            case 0:
                return templook;
            case -1:
                return "down" + templook;
        }
        return "";
    }

    void fire()
    {
        if (Time.time >= time)
        {
            time = Time.time + reload;
            firingshirt = shirtcontrol.GetTop();
            GameObject clone;
            clone = Instantiate(Projectile, Player.transform);
            clone.SendMessage("Settype", firingshirt);
            Rigidbody2D cloneRB = clone.GetComponent<Rigidbody2D>();
            switch (looking)
            {
                case ("right"):
                    //cloneRB.transform.rotation = new Quaternion(cloneRB.transform.rotation.x, cloneRB.transform.rotation.y,-90, cloneRB.transform.rotation.w);
                    cloneRB.transform.Rotate(0, 0,-90);
                    cloneRB.velocity = new Vector2(Projvelocity, 0);
                    break;
                case ("left"):
                    cloneRB.transform.Rotate(0, 0, 90);
                    cloneRB.velocity = new Vector2(-Projvelocity, 0);
                    break;
                case ("up"):
                    cloneRB.transform.Rotate(0, 0, 0);
                    cloneRB.velocity = new Vector2(0,Projvelocity);
                    break;
                case ("down"):
                    cloneRB.transform.Rotate(0, 0, 180);
                    cloneRB.velocity = new Vector2(0,-Projvelocity);
                    break;
                case ("upright"):
                    cloneRB.transform.Rotate(0, 0, -45);
                    cloneRB.velocity = new Vector2(Projvelocity/2, Projvelocity/2);
                    break;
                case ("downright"):
                    cloneRB.transform.Rotate(0, 0, -135);
                    cloneRB.velocity = new Vector2(Projvelocity / 2, -Projvelocity / 2);
                    break;
                case ("upleft"):
                    cloneRB.transform.Rotate(0, 0, 45);
                    cloneRB.velocity = new Vector2(-Projvelocity / 2, Projvelocity / 2);
                    break;
                case ("downleft"):
                    cloneRB.transform.Rotate(0, 0, 135);
                    cloneRB.velocity = new Vector2(-Projvelocity / 2, -Projvelocity / 2);
                    break;
            }
        }
    }
}

