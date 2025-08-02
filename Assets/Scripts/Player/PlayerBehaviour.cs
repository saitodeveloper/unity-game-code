using System;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 8;
    public float sprintMultiplier = 2;
    public Vector3 target;
    private string[] _rightMovement = { "N", "NE", "S", "SE", "E" };
    private GameObject _targetGameObject = null;
    private Rigidbody2D _rb;
    private Animator _anim;
    private PlayerStateController _playerStateController = new PlayerStateController();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            CheckForEnemyClick();
        }

        if (_targetGameObject != null)
        {
            Collider2D enemyCollider = _targetGameObject.GetComponent<Collider2D>();
            float enemyRadius = enemyCollider.bounds.size.x / 2f;
            float distanceToEdge = Vector3.Distance(
                transform.position, _targetGameObject.transform.position
            ) - enemyRadius;
            _playerStateController.ForceStop = distanceToEdge <= 3f;
        }
        else
        {
            _playerStateController.ForceStop = false;
        }

        _playerStateController.IsAccelerating = Input.GetAxis("Fire3") > 0;
        _playerStateController.IsMoving =
            transform.position != target &&
            !_playerStateController.ForceStop;

        Vector3 scale = transform.localScale;
        string direction = CompassIndicator.WalkingDirection(transform.position, target);
        bool toFaceRight = _rightMovement.Contains(direction);

        if (toFaceRight)
        {
            scale.x = Math.Abs(scale.x);
        }
        else if (direction != "STOP")
        {
            scale.x = -Math.Abs(scale.x);
        }

        transform.localScale = scale;

        if (_playerStateController.IsMoving)
        {
            _anim.SetBool("isWalking", !_playerStateController.IsAccelerating);
            _anim.SetBool("isRunning", _playerStateController.IsAccelerating);
            _anim.SetBool("isAttacking", false);
        }
        else
        {
            _anim.SetBool("isWalking", false);
            _anim.SetBool("isRunning", false);
            _anim.SetBool("isAttacking", _playerStateController.IsAttacking);
        }
    }

    void FixedUpdate()
    {
        var totalSpeed = (_playerStateController.IsAccelerating ? sprintMultiplier : 1) * speed;

        if (_playerStateController.IsMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, target, totalSpeed * Time.deltaTime
            );
        }
    }

    void CheckForEnemyClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        _playerStateController.IsAttacking = hit.collider != null && hit.collider.gameObject != null;
        _targetGameObject = hit.collider?.gameObject;
    }
}
