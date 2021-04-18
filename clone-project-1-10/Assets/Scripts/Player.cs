using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private float _playerSpeed = 1f;
    private float timeBetweenShots = 1f;

    private Joystick _joystick;
    private CharacterController _characterController;

    private Vector3 _moveDirection;
    private Enemy[] _enemies;
    private Enemy _nearestEnemy;
    private bool _isMoving;
    private bool _isShooting;
    private bool _ifShooted;

    private void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        _characterController = FindObjectOfType<CharacterController>();

        _isMoving = false;
        _nearestEnemy = null;

        Invoke("GetDamage", 1f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        CollectAllEnemies();
        DetectNearestEnemy();
    }

    private void Update()
    {
        Move();

        if (!_isMoving)
        {
            ShootOnIdle();
        }
    }

    private void Move()
    {
        // Move
        float horizontalAxis = _joystick.Horizontal;
        float verticalAxis = _joystick.Vertical;

        _moveDirection = new Vector3(horizontalAxis, 0f, verticalAxis);

        _isMoving = _moveDirection.sqrMagnitude > 0f;

        Vector3 movement = _moveDirection.normalized * _playerSpeed * Time.deltaTime;

        _characterController.Move(movement);

        // Rotate
        if (_isMoving)
        {
            transform.rotation = Quaternion.LookRotation(_moveDirection);
            _isShooting = false;
        }
        else
        {
            transform.rotation = Quaternion.identity;
            _isShooting = true;
        }
    }

    private void ShootOnIdle()
    {
        // Look at nearest enemy
        if (_nearestEnemy != null)
        {
            transform.LookAt(_nearestEnemy.transform);

            if (_isShooting)
            {
                if (!_ifShooted)
                {
                    float projectileForce = 7f;

                    GameObject currentProjectile = Instantiate(projectile, transform.position + Vector3.forward, Quaternion.identity);
                    currentProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForce, ForceMode.Impulse);
                    StartCoroutine(ShootAndWait());
                    _ifShooted = true;
                }
            }
        }
    }

    private IEnumerator ShootAndWait()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        _ifShooted = !_ifShooted;
    }

    private void CollectAllEnemies()
    {
        _enemies = FindObjectsOfType<Enemy>();
    }

    private void DetectNearestEnemy()
    {
        if (_enemies.Length > 0)
        {
            _nearestEnemy = _enemies[0];

            float distance = Vector3.Distance(transform.position, _enemies[0].transform.position);

            for (int i = 0; i < _enemies.Length; i++)
            {
                float tempDistance = Vector3.Distance(transform.position, _enemies[i].transform.position);

                if (tempDistance < distance)
                    _nearestEnemy = _enemies[i];
            }
        }
    }

    private void GetDamage()
    {
        Attack(30);
    }
}