using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBlock : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Sprite _emptySprite;

    private int _amountOfStars;

    private Vector2 _startPosition;
    private Vector2 _bounceUpPosition;

    private bool _isBounced;

    private void Start()
    {
        _startPosition = transform.position;
        _bounceUpPosition = _startPosition + new Vector2(0f, 0.7f);
        _amountOfStars = Random.Range(1, 6);
    }

    private void Update()
    {
        if (_isBounced)
        {
            Bounce();
        }
        else
            transform.position = _startPosition;
    }

    private void SubstructCoin()
    {
        if (_amountOfStars > 0)
        {
            _amountOfStars -= 1;

            if (_amountOfStars == 0)
            {
                GetComponent<SpriteRenderer>().sprite = _emptySprite;
                Destroy(this);
            }
        }
    }

    private void Bounce()
    {
        transform.Translate(Vector2.up * 2f * Time.deltaTime);

        if (Vector2.Distance(transform.position, _bounceUpPosition) < 0.1f)
        {
            _isBounced = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (transform.position.y > collision.transform.position.y)
            {
                SubstructCoin();
                Instantiate(_coinPrefab, _startPosition, Quaternion.identity);
                _isBounced = true;
            }
        }
    }
}
