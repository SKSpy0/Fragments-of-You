using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakThePlatform : MonoBehaviour
{
    // Declare variables
    public float breakTimer;
    public GameObject prefab;
    public GameObject Left;
    public GameObject Center;
    public GameObject Right;

    private bool playerOn = false;
    private FixedJoint2D LeftJoin;
    private FixedJoint2D CenterJoin;
    private FixedJoint2D RightJoin;
    private SpriteRenderer LeftJoinColor;
    private SpriteRenderer CenterJoinColor;
    private SpriteRenderer RightJoinColor;
    private float colorAlpha = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Initiate varaibles
        LeftJoin = Left.GetComponent<FixedJoint2D>();
        CenterJoin = Center.GetComponent<FixedJoint2D>();
        RightJoin = Right.GetComponent<FixedJoint2D>();

        LeftJoinColor = Left.GetComponent<SpriteRenderer>();
        CenterJoinColor = Center.GetComponent<SpriteRenderer>();
        RightJoinColor = Right.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // When player stand on one of the joins, start the join destory
        // count down.
        if (playerOn)
        {
            // Decrease the timer also make the joins slowly 
            // become transparent.
            if (breakTimer > 0)
            {
                breakTimer -= Time.deltaTime;
                colorAlpha -= Time.deltaTime * 0.8f;
                LeftJoinColor.color = new Color(1, 1, 1, colorAlpha);
                CenterJoinColor.color = new Color(1, 1, 1, colorAlpha);
                RightJoinColor.color = new Color(1, 1, 1, colorAlpha);
            }
            else // When time is up, disconnect the join and explode
            {
                if (LeftJoin != null)
                {
                    LeftJoin.breakForce = 0;
                    Left.GetComponent<Rigidbody2D>().AddForce(transform.right * -500, ForceMode2D.Impulse);
                    CenterJoin.breakForce = 0;
                    Center.GetComponent<Rigidbody2D>().AddForce(transform.up * -500, ForceMode2D.Impulse);
                    RightJoin.breakForce = 0;
                    Right.GetComponent<Rigidbody2D>().AddForce(transform.right * 500, ForceMode2D.Impulse);
                }
                // Kill the update
                playerOn = false;

                breakPlatManager.Instance.StartCoroutine("SpawnPlatform",
                    new Vector2(transform.position.x, transform.position.y));

                // // Destory the old platform obj
                Destroy(this.gameObject, breakTimer + 2f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Start break the platform when player jump on it.
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Land");
            playerOn = true;
        }
    }
}
