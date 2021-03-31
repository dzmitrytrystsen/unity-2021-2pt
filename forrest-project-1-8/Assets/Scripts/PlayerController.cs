using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _playerAnimator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    MoveLeft();
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    MoveRight();
                }
            }

            
        }
        else
            Idle();
    }

    private void MoveLeft()
    {
        ParallaxEffect.MyDirection = ParallaxEffect.Direction.Left;
        _playerAnimator.SetBool("IsRun", true);
        _spriteRenderer.flipX = true;
    }

    private void MoveRight()
    {
        ParallaxEffect.MyDirection = ParallaxEffect.Direction.Right;
        _playerAnimator.SetBool("IsRun", true);
        _spriteRenderer.flipX = false;
    }

    private void Idle()
    {
        ParallaxEffect.MyDirection = ParallaxEffect.Direction.Idle;
        _playerAnimator.SetBool("IsRun", false);
    }
}
