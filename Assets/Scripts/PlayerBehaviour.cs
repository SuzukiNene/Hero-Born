using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public float bulletSpeed = 100f;
    
    private float vInput;
    private float hInput;
    private GameObject newBullet = null;
    private Rigidbody _rigid;
    private BoxCollider _collider;
    private GameBehaviour _game;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
        _game = GameObject.Find("Game").GetComponent<GameBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // check for Move, Rotate
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        // Jump
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("[Update] Pressed Space key!");
            _rigid.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        
        // check for Shot Bullet
        if (newBullet == null && Input.GetMouseButtonDown(0))
        {
            newBullet = Instantiate(bullet,
//              this.transform.position + new Vector3(1, 0, 0),
                this.transform.position + new Vector3(0, 0, 0),
                this.transform.rotation
            ) as GameObject;
        }
        
        /* 
        this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }

    // 
    void FixedUpdate()
    {
        // Move, Rotate
        Vector3 position = this.transform.forward * vInput;
        Vector3 rotation = Vector3.up * hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rigid.MovePosition(this.transform.position + position * Time.fixedDeltaTime);
        _rigid.MoveRotation(_rigid.rotation * angleRot);
        
        // Shot Bullet
        if (newBullet)
        {
            Rigidbody rigid = newBullet.GetComponent<Rigidbody>();
            rigid.velocity = this.transform.forward * bulletSpeed;
            newBullet = null;
        }
    }

    private bool IsGrounded()
    {
        Vector3 halfExtents = new Vector3(
            _collider.bounds.size.x * 0.5f,
            _collider.bounds.size.y * 0.5f,
            _collider.bounds.size.z * 0.5f
        );
        bool bGrounded = Physics.CheckBox(
            _collider.bounds.center,
            halfExtents,
            Quaternion.identity,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );
        return bGrounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Update Player's HP
        if (collision.gameObject.name == "Enemy(Clone)")
        {
            _game.HP -= 1;
        }
    }
}
