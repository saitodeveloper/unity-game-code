using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 8;
    public float sprintMultiplier = 2;
    public Vector3 target;
    private string[] _rightMovement = { "N", "NE", "S", "SE", "E" };
    private GameObject _targetGameObject = null;
    private ItemAbstractBehaviour _touchedObject = null;
    private Animator _animator;
    private PlayerStateController _playerStateController = new PlayerStateController();
    public List<Item> _items;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _items = new List<Item>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            _targetGameObject = hit.collider?.gameObject;

            if (_targetGameObject != null)
            {
                _playerStateController.CollectingItemClicked = _targetGameObject.CompareTag("NatureCollectable");
            }
            else
            {
                _playerStateController.CollectingItemClicked = false;
            }
        }

        if (_targetGameObject != null)
        {
            Collider2D enemyCollider = _targetGameObject.GetComponent<Collider2D>();
            float targetRadius = enemyCollider.bounds.size.x / 1f;
            float distanceToEdge = Vector3.Distance(
                transform.position, _targetGameObject.transform.position
            ) - targetRadius;
            _playerStateController.PlayerTargetDistance = distanceToEdge;
        }

        _playerStateController.ForceStop =
            _touchedObject != null &&
            _targetGameObject != null &&
            _playerStateController.PlayerTargetDistance <= 2f;

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
        var state = _animator.GetCurrentAnimatorStateInfo(0);

        if (_playerStateController.IsMoving)
        {
            _animator.SetBool("isWalking", !_playerStateController.IsAccelerating);
            _animator.SetBool("isRunning", _playerStateController.IsAccelerating);
            _animator.SetBool("isAttacking", false);
            _animator.SetBool("isCollecting", false);
        }
        else
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isAttacking", _playerStateController.IsAttacking);
            _animator.SetBool("isCollecting",
                _playerStateController.CollectingItemClicked &&
                _touchedObject != null &&
                _touchedObject.Item.Quanity > 0
            );
        }

        if (state.IsName("Collecting") && state.normalizedTime >= 1f)
        {
            if (_touchedObject != null && _touchedObject.Item.Quanity > 0)
            {
                _touchedObject.OnInteractionFinished(_items);
                _touchedObject = null;
            }
            _playerStateController.CollectingItemClicked = false;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("NatureCollectable"))
        {
            _touchedObject = collision.gameObject.GetComponent<BushBehaviour>();
        }
    }
}
