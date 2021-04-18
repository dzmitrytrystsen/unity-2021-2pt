using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _enemySlowIndex;

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private void Start()
    {
        _enemySlowIndex = Random.Range(0.2f, 0.4f);

        _startPosition = transform.position;
        _endPosition = _startPosition + new Vector2(-3f, 0f);
    }

    private void Update()
    {
        Move();
    }

    private void Death(Rigidbody2D playerRigidbody2D)
    {
        float blowForce = 12f;

        playerRigidbody2D.AddForce(Vector2.up * blowForce, ForceMode2D.Impulse);

        Destroy(gameObject);
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(_startPosition, _endPosition, Mathf.PingPong(Time.time * _enemySlowIndex, 1f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Death(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }
}
