using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterGeneratorBehaviour : MonoBehaviour
{
    public GameObject MonsterPrefab;
    public int MaxMonstersNearBy = 5;
    public float RespawnTimer = 5f;
    private float _respawnTimerCounter;
    private List<MonsterBehaviour> list = new List<MonsterBehaviour>();

    void Start()
    {
        _respawnTimerCounter = RespawnTimer;
    }

    void Update()
    {
        if (_respawnTimerCounter <= 0f)
        {
            _respawnTimerCounter = RespawnTimer;
        }
        else
        {
            _respawnTimerCounter -= Time.deltaTime * 1f;
        }

        if (list.Count < MaxMonstersNearBy && _respawnTimerCounter <= 0f)
        {
            float[] directions = {
                this.transform.position.x + Random .Range(-10f, 10f),
                this.transform.position.y + Random.Range(-10f, 10f),
                this.transform.position.z
            };
            var obj = Instantiate(
                MonsterPrefab,
                new Vector3(directions[0], directions[1], directions[2]),
                Quaternion.identity
            );
            var behaviour = obj.GetComponent<MonsterBehaviour>();

            list.Add(behaviour);
        }

        foreach (var behaviour in list)
        {
            if (!behaviour._playerStateController.IsAlive)
            {
                list.Remove(behaviour);
            }
        }
    }
}
