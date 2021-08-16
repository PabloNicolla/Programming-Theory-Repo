using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int player;

    [SerializeField] protected Animator animator;

    protected float runSpeed = 40.0f;
    protected float horizontalMove = 0.0f;
    protected float verticalSpeed = 8.0f;
    protected float verticalMove = 0.0f;
    
    protected int health = 40;

    protected bool isJumping = false;
    protected bool isCrouching = false;
    protected bool isLadder = false;
    protected bool isClimbing = false;

    private void Update()
    {
        GetPlayerInput();
    }

    protected abstract void GetPlayerInput();

    public void Onlanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        ClimbingMovement();
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
    }

    protected void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    protected void TakeDamage (int damage)
    {
        health -= damage;

        if (health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        LevelManager.instance.Respawn(player);
    }

    private void ClimbingMovement()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, verticalMove * verticalSpeed);
        }
        else
        {
            rb.gravityScale = 2f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        BulletTrigger(other);
        LadderTriggerEnter(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LadderTriggerExit(other);
    }

    private void BulletTrigger(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            int damage = other.GetComponent<Bullet>().damage;
            TakeDamage(damage);
        }
    }

    private void LadderTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void LadderTriggerExit(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
