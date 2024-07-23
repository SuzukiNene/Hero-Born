using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;

    public delegate void DestroyedEnemyEvent();
    public event DestroyedEnemyEvent EnemyDestroyed; 

    private int locationIndex = 0;
    private NavMeshAgent agent;

    private int _lives = 3;
    public int EnemyLives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                //Debug.Log("敵が倒れる。");
                EnemyDestroyed();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

        InitPatrolRoute();
        MoveToNextRoute();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            // Close to Locations, not Player
            if (agent.destination != player.position)
            {
                MoveToNextRoute();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            //Debug.Log("プレーヤーを検出した - 攻撃せよ！");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            MoveToNextRoute();
            //Debug.Log("プレーヤーは領域外へ - パトロール続行！");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            //Debug.Log("命中！");
        }
    }

    void InitPatrolRoute()
    {
        patrolRoute = GameObject.Find("Patrol Route").transform;

        locations.Clear();
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextRoute()
    {
        if (locations.Count == 0)
        {
            return;
        }
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }
}
