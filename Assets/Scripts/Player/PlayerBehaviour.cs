using System;
using System.Linq;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 5;
    public float sprintMultiplier = 2;
    public Animator anim;
    public Vector3 target;
    public bool forceStop = false;
    private string[] _rightMovement = { "N", "NE", "S", "SE", "E" };
    private bool _isAccelerated = false;
    private bool _isAttacking = false;
    private bool _isMoving = false;
    private GameObject _targetGameObject = null;

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
            forceStop = distanceToEdge <= 2f;
        }
        else
        {
            forceStop = false;
        }

        _isAccelerated = Input.GetAxis("Fire3") > 0;
        _isMoving = transform.position != target && !forceStop;

        var totalSpeed = (_isAccelerated ? sprintMultiplier : 1) * speed;

        if (_isMoving)
            transform.position = Vector3.MoveTowards(transform.position, target, totalSpeed * Time.deltaTime);

        Vector3 scale = transform.localScale;
        string direction = WalkingDirection(transform.position, target);
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

        if (_isMoving)
        {
            anim.SetBool("isWalking", !_isAccelerated);
            anim.SetBool("isRunning", _isAccelerated);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", _isAttacking);
        }

                  
    }

    void CheckForEnemyClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        _isAttacking = hit.collider != null && hit.collider.gameObject != null;
        _targetGameObject = hit.collider?.gameObject;
    }

    public string WalkingDirection(Vector3 currentPossition, Vector3 movingTo)
    {
        if (currentPossition.x == movingTo.x)
        {
            if (currentPossition.y == movingTo.y) return "STOP";
            else if (currentPossition.y < movingTo.y) return "N";
            else return "S";
        }
        else if (currentPossition.x > movingTo.x)
        {
            if (currentPossition.y == movingTo.y) return "W";
            else if (currentPossition.y < movingTo.y) return "NW";
            else return "SW";
        }

        if (currentPossition.y == movingTo.y) return "E";
        else if (currentPossition.y < movingTo.y) return "NE";
        else if (currentPossition.y > movingTo.y) return "SE";
        else return "SE";
    }
}
