using System;
using UnityEngine;

public class MonsterBehaviour : CombactableAbstractBehaviour
{
    private Animator _animator;
    public Vector3 targetDirection;
    public GameObject DamagePrefab;
    public float WalkingWaitingTime = 5.0f;
    public float ForgiveTime = 5.0f;
    public bool isTimerFlagged = true;
    public bool ForceStop = false;
    public string direction;
    private Vector3 _instantPosition;
    private bool _isWalking = false;
    public PlayerBehaviour Revenge;

    void OnEnable()
    {
        PlayerBehaviour.HitAction += OnCambatInteraction;
    }

    private void OnDisable()
    {
        PlayerBehaviour.HitAction -= OnCambatInteraction;
    }

    void Start()
    {
        this._animator = GetComponent<Animator>();
        this.targetDirection = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void CalculateForgetTime()
    {
        if (ForgiveTime <= 0f && Revenge != null)
        {
            Revenge = null;
            this._animator.SetBool("isFighting", false);
            ForgiveTime = 5f;
        }
        if (ForgiveTime >= 0f && Revenge != null)
        {
            ForgiveTime -= Time.deltaTime * 1.0f;
        }
    }

    void Update()
    {
        CalculateForgetTime();
        if (ForceStop)
        {
            _isWalking = false;
            this._animator?.SetBool("isWalking", false);
            return;
        }

        if (isTimerFlagged && WalkingWaitingTime <= 0f && Revenge == null)
        {
            float[] directions = {
                this.transform.position.x + UnityEngine.Random .Range(-10f, 10f),
                this.transform.position.y + UnityEngine.Random.Range(-10f, 10f),
                transform.position.z
            };
            this.targetDirection = new Vector3(directions[0], directions[1], directions[2]);
            this.WalkingWaitingTime = 5.0f;
        }
        else if (isTimerFlagged && WalkingWaitingTime > 0f && Revenge == null)
        {
            WalkingWaitingTime -= Time.deltaTime * 1.0f;
        }
        else if (Revenge != null)
        {
            this.targetDirection = Revenge.gameObject.transform.position;
        }

        _isWalking = transform.position != targetDirection;
        this.direction = CompassIndicator.WalkingDirection(_instantPosition, targetDirection);

        bool toFaceRight = CompassIndicator.FaceRight(this.direction);
        Vector3 scale = transform.localScale;

        if (toFaceRight)
        {
            scale.x = Math.Abs(scale.x);
        }
        else if (direction != "STOP")
        {
            scale.x = -Math.Abs(scale.x);
        }
        transform.localScale = scale;

        this._animator?.SetBool("isWalking", _isWalking);

        var state = _animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("InPain") && state.normalizedTime >= 1f)
        {
            this._animator.SetBool("inPain", false);
        }
    }

    void FixedUpdate()
    {
        if (_isWalking)
        {
            _instantPosition = Vector3.MoveTowards(this.transform.position, this.targetDirection, Time.fixedDeltaTime * 6f);
            this.transform.position = _instantPosition;
        }
    }

    public override void OnCombatStart()
    {
        this._animator.SetBool("isFighting", true);
    }

    public void OnCambatInteraction(PlayerBehaviour attacker)
    {
        this._animator.SetBool("inPain", true);
        var obj = Instantiate(DamagePrefab, transform.position, Quaternion.identity);
        var message = obj.GetComponent<DamagePopUpBehaviour>();
        message.SetText("20");
        Revenge = attacker;
    }

    public void StopInPain()
    {
        this._animator.SetBool("inPain", false);
    }

    public override void OnCombatFinished()
    {
        this._animator.SetBool("isFighting", false);
    }

    public override void OnForceStop()
    {
        ForceStop = true;
    }

    public override void OnReleaseObject()
    {
        ForceStop = false;
    }

    public override void OnCombatPause()
    {
        this._animator.SetBool("isFighting", false);
    }

    public override bool IsEnemyAlive()
    {
        return true;
    }
}