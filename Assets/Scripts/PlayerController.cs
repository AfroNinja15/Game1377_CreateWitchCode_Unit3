using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;

    public ParticleSystem explosionPartical;
    public ParticleSystem dirtPartical;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier; //short for Physics.gravity = Physics.gravity * graviyModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtPartical.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtPartical.Play();
        }
        else if (collision.gameObject.CompareTag("Obstical"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionPartical.Play();
            dirtPartical.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
