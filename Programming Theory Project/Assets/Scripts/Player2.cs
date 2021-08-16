using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Player
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
        horizontalMove = Input.GetAxisRaw("Horizontal-Player-2") * runSpeed;
    }

    private void CheckIfJumping()
    {
        if (Input.GetButtonDown("Jump-Player-2"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
    }

    private void CheckIfCrouching()
    {
        if (Input.GetButtonDown("Crouch-Player-2"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch-Player-2"))
        {
            isCrouching = false;
        }
    }

    private void CheckIfShooting()
    {
        if (Input.GetButtonDown("Fire1-Player-2"))
        {
            Shoot();
        }
    }

    private void CheckIfClimbing()
    {
        verticalMove = Input.GetAxis("Vertical-Player-2");

        if (isLadder && Mathf.Abs(verticalMove) > 0f)
        {
            isClimbing = true;
        }
    }
}
