using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimer : MonoBehaviour
{

    [Header("Cursor Settings")]
    public LayerMask aimLayer;
    public GameObject aimCursor;
    public Quaternion rotationPlayerToCursor;

    private Camera mainCam;
    private Ray mouseAimRay;
    private RaycastHit hit;

    [Header("Bullet Settings")]
    public float projectileSpeed;
    public float projectileLifetime;
    public float shotCooldownTime;
    public GameObject projectileShape;

    public int effectorType;

    private ElementInfoDatabase EID;
    private bool shootable = true;

    [Header("Animation")]

    public Rigidbody rb;
    public Animator player;
    public SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        EID = GameObject.Find("ElementDatabase").gameObject.GetComponent<ElementManager>().publicAccessElementDatabase;
        SetElement(0);
        shootable = true;
        player = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        mouseAimRay = mainCam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(mouseAimRay, out hit, Mathf.Infinity, aimLayer.value);

        if (aimCursor == null)
        {
            Debug.LogWarning("No target cusor assigned");
        }
        else
        {
            if (hit.point != null)
            {
                aimCursor.transform.position = hit.point;
            }
        }

        Vector3 vectorBetween = new Vector3(transform.position.x, transform.position.y, transform.position.z) - new Vector3(hit.point.x, transform.position.y, hit.point.z);
        float rotation = -(Mathf.Atan2(vectorBetween.z, vectorBetween.x) * Mathf.Rad2Deg);
        rotationPlayerToCursor = Quaternion.Euler(transform.rotation.x, rotation + 90, transform.rotation.z);
        transform.rotation = rotationPlayerToCursor;

        if (Input.GetMouseButton(1) && shootable)
        {
            ShootBullet();
        }

        if (Input.GetKeyDown("1"))
        {
            SetElement(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            SetElement(1);
        }
        else if (Input.GetKeyDown("3"))
        {
            SetElement(2);
        }

       

        AnimationHandler();
    }

    public void AnimationHandler() 
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            player.SetBool("isRunning", true);

        }
        else
        {
            player.SetBool("isRunning", false);

        }

        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var playerScreenPoint = Camera.main.WorldToScreenPoint(player.transform.position);

        if (mouse.x > playerScreenPoint.x + 20f)
        {
            //left
            player.SetBool("FacingLeft", true);
            player.SetBool("FacingBack", false);
            sprite.flipX = false;
        }
        else if (mouse.x < playerScreenPoint.x - 20f)
        {
            player.SetBool("FacingLeft", true);
            player.SetBool("FacingBack", false);
            sprite.flipX = true;
        }
        else if (mouse.y > playerScreenPoint.y)
        {
            player.SetBool("FacingLeft", false);
            player.SetBool("FacingBack", true);
        }
        else
        {
            player.SetBool("FacingLeft", false);
            player.SetBool("FacingBack", false);

        }

        if (Input.GetMouseButton(0))
        {
            player.SetBool("isRunning", false);
            player.SetTrigger("Attacking");
            
            


        }
    }

    public void SetElement(int index)
    {
        projectileSpeed = EID.elements[index].projectileSpeed;
        projectileLifetime = EID.elements[index].projectileLifetime;
        shotCooldownTime = EID.elements[index].shotCooldownTime;
        projectileShape = EID.elements[index].projectileShape;
        effectorType = ((int)EID.elements[index].effectorType);
    }

    public void ShootBullet()
    {
        GameObject shotBullet = Instantiate(projectileShape, transform.position, transform.rotation);
        Rigidbody bulletRB = shotBullet.GetComponent<Rigidbody>();
        bulletRB.AddRelativeForce(bulletRB.velocity.x, bulletRB.velocity.y, -projectileSpeed, ForceMode.Impulse);
        StartCoroutine(TimedDestruction(shotBullet));
        shootable = false;
        StartCoroutine(ShotCooldown(shotCooldownTime));

        print(effectorType);
    }

    public IEnumerator TimedDestruction(GameObject currentBullet)
    {
        yield return new WaitForSeconds(projectileLifetime);
        Destroy(currentBullet);
    }

    public IEnumerator ShotCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        shootable = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, aimCursor.transform.position);
    }
}
