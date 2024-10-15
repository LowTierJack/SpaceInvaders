using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public Laser laserPrefab;
    Laser laser;
    float speed = 5f;
    Freeze _freezer;
    public ParticleSystem system;

    private void Start()
    {

        GameObject mgr = GameObject.FindWithTag("Manager");
        if (mgr)
        {
            _freezer = mgr.GetComponent<Freeze>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        ParticleSystem ps = GetComponent<ParticleSystem>();

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
        }

        transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Railgun.Emit(Vector3.zero, Vector3.up, 0.2, 2, Color.yellow);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, 1 << 7);
            Debug.DrawRay(transform.position, Vector2.up, Color.green);

            // if it hits something
            if (hit.collider != null)
            {
                _freezer.Freeza();
                GameManager.Instance.OnInvaderKilled(hit.transform);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }
}