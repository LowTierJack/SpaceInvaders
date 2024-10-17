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
    [SerializeField] ParticleSystem gunEffect;

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

            Vector2 playerPos = transform.position;

            var emitParamsGun = new ParticleSystem.EmitParams();
            emitParamsGun.applyShapeToPosition = true;
            emitParamsGun.position = playerPos;
            gunEffect.Emit(emitParamsGun, 6
                );


            var emitParams = new ParticleSystem.EmitParams();
            emitParams.applyShapeToPosition = true;
            emitParams.position = transform.position + new Vector3(0,15,0);

           
            system.Emit(emitParams, 1);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, Vector2.up, Mathf.Infinity, 1 << 7);
            Debug.DrawRay(transform.position, Vector2.up, Color.green);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                _freezer.Freeza();
                GameManager.Instance.OnInvaderKilled(hit.transform);
            }

                // if it hits something
               /* if (hits.collider != null)
            {
                _freezer.Freeza();
                GameManager.Instance.OnInvaderKilled(hit.transform);

            }*/
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
