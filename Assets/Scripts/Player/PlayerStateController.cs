public class PlayerStateController
{
    private bool _isAccelerating = false;
    private bool _isEnemyChallenged = false;
    private bool _isMoving = false;
    private bool _hasCollided = false;
    private bool _forceStop = false;
    private bool _collectingItemClicked = false;
    private float _playerTargetDistance = float.MaxValue;

    public bool IsAccelerating
    {
        get { return _isAccelerating; }
        set { _isAccelerating = value; }
    }

    public bool IsEnemyChallenged
    {
        get { return _isEnemyChallenged; }
        set { _isEnemyChallenged = value; }
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

    public bool CollectingItemClicked
    {
        get { return _collectingItemClicked; }
        set { _collectingItemClicked = value; }
    }

    public float PlayerTargetDistance
    {
        get { return _playerTargetDistance; }
        set { _playerTargetDistance = value; }
    }
}