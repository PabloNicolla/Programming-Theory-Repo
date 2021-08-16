using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Player
{
    protected override void GetPlayerInput()
    {
        CheckIfMoving();
        CheckIfJumping();
        CheckIfCrouching();
        CheckIfShooting();
        CheckIfClimbing();
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    private void CheckIfMoving()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal-Player-1") * runSpeed;
    }

    private void CheckIfJumping()
    {
        if (Input.GetButtonDown("Jump-Player-1"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
    }

    private void CheckIfCrouching()
    {
        if (Input.GetButtonDown("Crouch-Player-1"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch-Player-1"))
        {
            isCrouching = false;
        }
    }

    private void CheckIfShooting()
    {
        if (Input.GetButtonDown("Fire1-Player-1"))
        {
            Shoot();
        }
    }

    private void CheckIfClimbing()
    {
        verticalMove = Input.GetAxis("Vertical-Player-1");

        if (isLadder && Mathf.Abs(verticalMove) > 0f)
        {
            isClimbing = true;
        }
    }
}
