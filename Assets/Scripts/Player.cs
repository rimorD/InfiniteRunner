using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        asrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Act if were not dead
        if (lifes > 0)
        {
            // Make sure we cant jump/dash multiple times
            if (!isDoingAction)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(Jump());
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    StartCoroutine(Dash());
                }
                // Debug only
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(ReceiveDamage());
                }
            }

            // Keep moving to the right indefinitely
            transform.position = Vector2.MoveTowards(transform.position, new Vector2((transform.position.x + speed), transform.position.y), maxDelta);

            // Update points
            points += pointsPerSecond * (Time.deltaTime);
        }
    }

    //---------------------------------------------------------------------------------------------

    IEnumerator ReceiveDamage()
    {
        // Reduce life
        lifes--;

        // Check if were still alive
        // Dead
        if (lifes <= 0)
        {
            // Play dead animation
            anim.SetBool("isDead", true);

            // Play dead sound
            DeathSound();

            // Animation duration
            yield return new WaitForSeconds(1.5f); ;

            // Save high score
            int highscore = PlayerPrefs.GetInt("highscore", 0);
            int currentScore = Mathf.FloorToInt(points);
            if (currentScore > highscore)
            {
                PlayerPrefs.SetInt("highscore", currentScore);
            }

            // Show end of game menu
            endMenu.ShowEndGamePanel(currentScore);
        }
        // Not dead
        else
        {
            // Make sure we dont get hurt for a while
            invulnerable = true;

            // Play damage sound
            DamageSound();

            // Red is the universal color for pain
            sr.color = Color.red;
            yield return new WaitForSeconds(0.25f);

            // Return to our regular color but with reduced opacity to indicate were in god mode
            sr.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(2f);

            // After a couple of seconds god mode is over
            sr.color = new Color(1f, 1f, 1f);
            invulnerable = false;
        }
    }

    //---------------------------------------------------------------------------------------------

    IEnumerator Dash()
    {
        // Save original collider size and offset
        Vector2 originalSize = boxCollider.size;
        Vector2 originalOffset = boxCollider.offset;

        // Set action flag
        isDoingAction = true;

        // Reduce collider in half since were crouching
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y * 0.5f);
        boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y - boxCollider.size.y * 0.5f);

        // Set animation
        anim.SetInteger("currentAnimation", 2);

        // Animation duration
        yield return new WaitForSeconds(0.5f);

        // Set back to run animation
        anim.SetInteger("currentAnimation", 0);

        // Return collider to original form
        boxCollider.size = originalSize;
        boxCollider.offset = originalOffset;

        // Set action flag
        isDoingAction = false;
    }

    //---------------------------------------------------------------------------------------------

    IEnumerator Jump()
    {
        // Set action flag
        isDoingAction = true;

        // Add force to jump
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Set animation
        anim.SetInteger("currentAnimation", 1);

        // Animation duration
        yield return new WaitForSeconds(1f);

        // Set back to run animation
        anim.SetInteger("currentAnimation", 0);

        // Set action flag
        isDoingAction = false;
    }

    //---------------------------------------------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
            if(!invulnerable)
                StartCoroutine("ReceiveDamage");
        }
        else if(collision.gameObject.tag == "Collectable")
        {
            asrc.PlayOneShot(collision.gameObject.GetComponent<Collectable>().audioClip);
            collision.gameObject.GetComponent<Collectable>().Collect(this);
        }
    }

    // Buffs and pickups //////////////////////////////////////////////////////////////////////////

    public void CloverPickup() 
    {
        StartCoroutine(CloverPickupCoroutine());
    }

    //---------------------------------------------------------------------------------------------

    IEnumerator CloverPickupCoroutine()
    {
        // Clover makes us invulnerable because its good luck?
        invulnerable = true;

        // Regular color but with reduced opacity to indicate were in god mode
        sr.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(4f);

        // After a couple of seconds god mode is over
        sr.color = new Color(1f, 1f, 1f);
        invulnerable = false;
    }

    //---------------------------------------------------------------------------------------------

    public void PotionPickup()
    {
        StartCoroutine(PotionPickupCoroutine());
    }

    //---------------------------------------------------------------------------------------------

    IEnumerator PotionPickupCoroutine()
    {
        // Potion makes us get more points because magic
        pointsPerSecond *= 2;

        // Buff cooldown
        yield return new WaitForSeconds(4f);

        // After a couple of seconds return to regular ratio
        pointsPerSecond /= 2;
    }

    // Audio player ///////////////////////////////////////////////////////////////////////////////

    public void FootstepSound()
    {
        asrc.PlayOneShot(footstep);
    }

    //---------------------------------------------------------------------------------------------

    public void DamageSound()
    {
        asrc.PlayOneShot(damage);
    }

    //---------------------------------------------------------------------------------------------

    public void DashSound()
    {
        asrc.PlayOneShot(dash);
    }

    //---------------------------------------------------------------------------------------------

    public void DeathSound()
    {
        asrc.PlayOneShot(death);
    }


    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public int lifes = 3;
    public bool invulnerable; // Has received damage recently and is now cheating
    private bool isDoingAction = false;
    public float points = 0;
    private int pointsPerSecond = 5;

    // Movement variables
    private float speed = 0.1f;
    private float maxDelta = 0.025f;
    private float jumpForce = 15f;

    // Components /////////////////////////////////////////////////////////////////////////////////
    SpriteRenderer sr;
    Animator anim;
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    AudioSource asrc;

    // Audio clips ////////////////////////////////////////////////////////////////////////////////
    public AudioClip footstep;
    public AudioClip damage;
    public AudioClip death;
    public AudioClip dash;


    // References /////////////////////////////////////////////////////////////////////////////////
    public EndGameMenu endMenu;
}
