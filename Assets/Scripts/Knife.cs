using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void KnifeFire(bool isGameActive);
public class Knife : MonoBehaviour
{
    [SerializeField] private float forceStrong;
    [SerializeField] ParticleSystem partical;

    private Rigidbody2D knifeRb;
    private BoxCollider2D knifeCollider;
    private bool isActive = true;
    private bool knifeInKnife = false;
    private bool knifeInLog = false;

    public event KnifeFire KnifeHitLog;

    private void Start()
    {
        knifeRb = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        #if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        #else

        if (Input.touchCount > 0)
        {
            Fire();
        }

        #endif
    }

    private void Fire()
    {
        if (!isActive)
        {
            return;
        }

        isActive = false;
        knifeRb.AddForce(Vector2.up * forceStrong, ForceMode2D.Impulse);
        knifeRb.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Log") && !knifeInKnife)
        {
            knifeInLog = true;
            knifeRb.velocity = Vector2.zero;
            knifeRb.isKinematic = true;
            gameObject.transform.SetParent(collision.transform);
            partical.Play();
            
            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.5f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1f);

            KnifeHitLog(true);
        }
        else if (collision.gameObject.CompareTag("Knife") && !knifeInLog)
        {
            knifeInKnife = true;
            KnifeHitLog(false);
            Destroy(gameObject, 1f);
        }
    }
}
