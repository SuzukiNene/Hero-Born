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
    [SerializeField] Material normalMaterial;
    [SerializeField] Material attackMaterial;

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
        patrolRoute = GameObject.Find("Patrol Route").transform;

        InitPatrolRoute();
        MoveToNextRoute();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            MoveToNextRoute();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            //Debug.Log("プレーヤーを検出した - 攻撃せよ！");
            GetComponent<MeshRenderer>().material = attackMaterial;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            //Debug.Log("プレーヤーが逃げても追う");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            MoveToNextRoute();
            //Debug.Log("プレーヤーは領域外へ - パトロール続行！");
            GetComponent<MeshRenderer>().material = normalMaterial;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            EnemyLives -= 1;
            //Debug.Log("命中！");
        }
        else if (collision.gameObject.name == "Player")
        {
            //Debug.Log("つかまえた！ - 攻撃中");
        }
    }

    void InitPatrolRoute()
    {
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
        //Debug.Log("MoveToNextRoute()");
    }
}
