using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    private Action<MonsterBehaviour> _hitAction;
    private List<Guid> _hitAcctionList = new List<Guid>();
    public Vector3 targetDirection;
    public float attackRest = 3f;
    public float walkingRest = 5f;
    public float forgetRevengeRest = 5f;
    private Animator _animator;
    public PlayerBehaviour Revenge;
    public float _attackRestTimer;
    public float _walkingRestTimer;
    public float _forgetRevengeRestTimer;
    private PlayerStateController _playerStateController = new PlayerStateController();
    private float _limitDistance = 0f;
    private Guid _guid = Guid.NewGuid();
    public GameObject DamagePrefab;
    public bool InvertOnFaceRight = false;

    void Start()
    {
        this._animator = GetComponent<Animator>();
        this.targetDirection = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _attackRestTimer = attackRest;
        _walkingRestTimer = walkingRest;
    }

    void Update()
    {
        this.CalculateTimers();
        this.UpdateMonsterState();
        this.UpdateNextMovent();
        this.AnimationPhase();
    }

    void FixedUpdate()
    {
        if (_playerStateController.IsMoving)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetDirection, Time.fixedDeltaTime * 8f);
        }
    }

    private void CalculateTimers()
    {
        if (_attackRestTimer <= 0f)
        {
            _playerStateController.AttackEnabled = true;
            _attackRestTimer = attackRest;
        }
        if (_attackRestTimer > 0f)
        {
            _attackRestTimer -= Time.deltaTime * 1.0f;
        }

        if (_walkingRestTimer <= 0f)
        {
            _walkingRestTimer = walkingRest;
        }
        if (_walkingRestTimer > 0f)
        {
            _walkingRestTimer -= Time.deltaTime * 1.0f;
        }

        if (_forgetRevengeRestTimer <= 0f)
        {
            _forgetRevengeRestTimer = forgetRevengeRest;
        }
        if (_forgetRevengeRestTimer > 0f && _playerStateController.EnemyDistance >= 20f)
        {
            _forgetRevengeRestTimer -= Time.deltaTime * 1.0f;
        }
    }

    private void UpdateMonsterState()
    {
        var distance = Revenge != null ?
            Vector3.Distance(transform.position, Revenge.transform.position) : float.MaxValue;

        _playerStateController.ForceStop = _playerStateController.IsEnemyChallenged || (distance <= _limitDistance);
        _playerStateController.IsMoving = (transform.position != this.targetDirection) && !_playerStateController.ForceStop;
        _playerStateController.EnemyDistance = Revenge != null ? distance : null;

        Revenge = _forgetRevengeRestTimer <= 0f ? null : Revenge;
    }

    private void UpdateNextMovent()
    {
        if (_walkingRestTimer <= 0f && Revenge == null)
        {
            float[] directions = {
                this.transform.position.x + UnityEngine.Random .Range(-10f, 10f),
                this.transform.position.y + UnityEngine.Random.Range(-10f, 10f),
                this.transform.position.z
            };
            _limitDistance = 0f;
            this.targetDirection = new Vector3(directions[0], directions[1], directions[2]);
        }
        else if (Revenge != null)
        {
            _limitDistance = 5f;
            this.targetDirection = Revenge.transform.position;
        }
    }

    public void SetChallenge(bool isEnemyChallenge, PlayerBehaviour enemy)
    {
        _playerStateController.IsEnemyChallenged = isEnemyChallenge;

        if (isEnemyChallenge)
        {
            Revenge = enemy;
            Revenge.RegisterOnHitAction(_guid, OnHitAction);
        }
    }

    private void AnimationPhase()
    {
        var direction = CompassIndicator.WalkingDirection(transform.position, this.targetDirection);
        bool toFaceRight = CompassIndicator.FaceRight(direction);
        Vector3 scale = transform.localScale;

        if (toFaceRight)
        {
            scale.x = (InvertOnFaceRight ? -1 : 1) * Math.Abs(scale.x);
        }
        else if (direction != "STOP")
        {
            scale.x = (InvertOnFaceRight ? 1 : -1) * Math.Abs(scale.x);
        }

        transform.localScale = scale;

        this._animator?.SetBool("isWalking", _playerStateController.IsMoving && !_playerStateController.ForceStop);
        this._animator?.SetBool("isFighting", _playerStateController.IsEnemyChallenged || Revenge != null);
        this._animator?.SetBool("inPain", _playerStateController.InPain);
        this._animator?.SetBool("isAttacking",
            !_playerStateController.InPain &&
            !_playerStateController.IsMoving &&
            _playerStateController.AttackEnabled
        );
    }

    public void OnHitAction(PlayerBehaviour enemy)
    {
        _playerStateController.InPain = true;
        Revenge = enemy;
        var obj = Instantiate(DamagePrefab, transform.position, Quaternion.identity);
        var message = obj.GetComponent<DamagePopUpBehaviour>();
        message.SetText("20");
    }

    public void StopInPain()
    {
        _playerStateController.InPain = false;
    }

    public void OnHitEvent()
    {
        if (_playerStateController.AttackEnabled)
        {
            _hitAction?.Invoke(this);
        }
    }

    public void OnAttackEnd()
    {
        _playerStateController.AttackEnabled = false;
    }

    public void OnAttackStart()
    {
        this.transform.position = Revenge.transform.position - new Vector3(2f, 0, 0);
    }

    public void RegisterOnHitAction(Guid guid, Action<MonsterBehaviour> action)
    {
        if (!CheckOnHitAction(guid))
        {
            _hitAcctionList.Add(guid);
            _hitAction += action;
        }
    }

    public void RemoveOnHitAction(Guid guid, Action<MonsterBehaviour> action)
    {
        if (CheckOnHitAction(guid))
        {
            _hitAcctionList.Remove(guid);
            _hitAction -= action;
        }
    }

    public bool CheckOnHitAction(Guid guid)
    {
        return _hitAcctionList.Contains(guid);
    }
}