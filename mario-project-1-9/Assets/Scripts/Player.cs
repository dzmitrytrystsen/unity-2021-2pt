using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _playerSpeed = 2f;
    [SerializeField] private float _jumpPower = 1f;
    [SerializeField] private ContactFilter2D _contactFilter;

    private Rigidbody2D _rigidboydy2D;

    [SerializeField] private bool _isGrounded => _rigidboydy2D.IsTouching(_contactFilter);

    private void Start()
    {
        _rigidboydy2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        if (_isGrounded && Input.GetButtonDown("Jump"))
            Jump();

        //if (_rigidboydy2D.velocity.y < -0.1f && !_isGrounded)
        //    LandFast();
    }

    private void LandFast()
    {
        float landForce = 10f;

        _rigidboydy2D.AddForce(Vector2.down * landForce, ForceMode2D.Force);
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = Vector2.right * horizontalInput;


        transform.position += moveDirection * _playerSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        _rigidboydy2D.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
    }

    private void Death()
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Death();
        }
    }
}
