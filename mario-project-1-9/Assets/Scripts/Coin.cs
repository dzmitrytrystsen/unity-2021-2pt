using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float _coinSpeed = 5f;

    private Vector2 _startPosition;
    private Vector2 _topPosition;

    private bool _isTopReached = false;

    private void Start()
    {
        _startPosition = transform.position;
        _topPosition = _startPosition + new Vector2(0f, 2f);
    }

    private void Update()
    {
        if (!_isTopReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, _topPosition, _coinSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _topPosition) < 0.1f)
                _isTopReached = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition, _coinSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _startPosition) < 0.1f)
                Destroy(gameObject);
        }
    }
}
