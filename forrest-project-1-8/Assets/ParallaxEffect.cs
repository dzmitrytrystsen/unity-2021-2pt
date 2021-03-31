using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float _mySpeed = 0.1f;

    public enum Direction { Left, Right, Idle };

    public static Direction MyDirection { get; set; }

    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private float _repeatWidth;

    private void Start()
    {
        _startPosition = transform.position;
        _repeatWidth = GetComponent<SpriteRenderer>().size.x;
        MyDirection = Direction.Idle;
    }

    private void Update()
    {
        _currentPosition = transform.position;

        if (Mathf.Abs(transform.position.x) > Mathf.Abs(_startPosition.x - _repeatWidth))
            transform.position = _startPosition;

        if (MyDirection == Direction.Left)
            MoveLeft();
        else if (MyDirection == Direction.Right)
            MoveRight();
        else
            Idle();
    }

    private void MoveLeft()
    {
        transform.Translate(Vector2.right * _mySpeed * Time.deltaTime);
    }

    private void MoveRight()
    {
        transform.Translate(Vector2.left * _mySpeed * Time.deltaTime);
    }

    private void Idle()
    {
        transform.position = _currentPosition;
    }
}
