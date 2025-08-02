class PlayerStateController
{
    private bool _isAccelerating = false;
    private bool _isAttacking = false;
    private bool _isMoving = false;
    private bool _hasCollided = false;
    private bool _forceStop = false;

    public bool IsAccelerating
    {
        get { return _isAccelerating; }
        set { _isAccelerating = value; }
    }

    public bool IsAttacking
    {
        get { return _isAttacking; }
        set { _isAttacking = value; }
    }

    public bool IsMoving
    {
        get { return _isMoving; }
        set { _isMoving = value; }
    }

    public bool HasCollided
    {
        get { return _hasCollided; }
        set { _hasCollided = value; }
    }

    public bool ForceStop
    {
        get { return _forceStop; }
        set { _forceStop = value; }
    }
}