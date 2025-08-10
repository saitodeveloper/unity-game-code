using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 8;
    public float sprintMultiplier = 2;
    public Vector3 target;
    private GameObject _targetGameObject = null;
    private CombactableAbstractBehaviour _targetEnemy = null;
    private ItemAbstractBehaviour _targetItem = null;
    private Animator _animator;
    private PlayerStateController _playerStateController = new PlayerStateController();
    public List<Item> _items;
    private float _limitDistance = 3f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _items = new List<Item>();
    }

    void Update()
    {
        this.CaptureClick();
        this.CaptureCommands();
        this.UpdatePlayerActions();
        this.AnimationPhase();
        this.BroadcastInfo();
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

    private void CaptureClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            _targetGameObject = hit.collider?.gameObject;
            target = mousePos;
            target.z = transform.position.z;

            if (_targetGameObject != null) Debug.Log("Target Object clicked");
        }

        if (_targetGameObject != null)
        {
            if (_targetGameObject.CompareTag("Enemy"))
            {
                _targetEnemy = _targetGameObject.GetComponent<CombactableAbstractBehaviour>();
                _limitDistance = 5f;

                _targetEnemy?.OnForceStop();
                _targetEnemy?.OnCombatStart();
            }
            if (_targetGameObject.CompareTag("NatureCollectable"))
            {
                _targetItem = _targetGameObject.GetComponent<ItemAbstractBehaviour>();
                _limitDistance = 2.5f;
            }
        }
        else
        {
            if (_targetEnemy != null)
            {
                _targetEnemy.OnReleaseObject();
            }
            _targetEnemy = null;
            _targetItem = null;
        }

        if (_targetGameObject == null && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            _targetGameObject = hit.collider?.gameObject;
            target = mousePos;
            target.z = transform.position.z;
        }
    }

    private void CaptureCommands()
    {
        _playerStateController.IsAccelerating = Input.GetAxis("Fire3") > 0;
    }

    private void UpdatePlayerActions()
    {
        if (_targetGameObject != null)
        {
            _playerStateController.PlayerTargetDistance = Vector3.Distance(
                transform.position, _targetGameObject.transform.position
            );
        }

        _playerStateController.ForceStop =
            (_targetEnemy != null || _targetItem != null) &&
            _playerStateController.PlayerTargetDistance <= _limitDistance;

        _playerStateController.IsEnemyChallenged =
            _targetEnemy != null &&
            _playerStateController.PlayerTargetDistance <= _limitDistance;

        _playerStateController.IsMoving =
            transform.position != target && !_playerStateController.ForceStop;
    }

    private void AnimationPhase()
    {
        Vector3 scale = transform.localScale;
        string direction = CompassIndicator.WalkingDirection(transform.position, target);
        bool toFaceRight = CompassIndicator.FaceRight(direction);

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
            _animator.SetBool("isWalking", !_playerStateController.IsAccelerating);
            _animator.SetBool("isRunning", _playerStateController.IsAccelerating);
            _animator.SetBool("isAttacking", false);
            _animator.SetBool("isCollecting", false);
        }
        else
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isAttacking",
                _targetEnemy != null &&
                _playerStateController.IsEnemyChallenged
            );
            _animator.SetBool("isCollecting",
                _targetItem != null &&
                _targetItem.Item.Quanity > 0
            );
        }
    }

    private void BroadcastInfo()
    {
        var state = _animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("Collecting") && state.normalizedTime >= 1f)
        {
            if (_targetItem != null && _targetItem.Item.Quanity > 0)
            {
                _targetItem.OnInteractionFinished(_items);
                _targetItem = null;
            }
        }

        if (state.IsName("Explorer Attack 1") && state.normalizedTime >= 1f)
        {
            if (_targetEnemy != null)
            {
                _targetEnemy.OnCambatInteraction();
            }
        }
    }
}
