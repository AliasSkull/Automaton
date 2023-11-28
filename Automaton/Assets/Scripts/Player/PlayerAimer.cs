using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public ElementInfoDatabase.Element element;
    public ElementInfoDatabase EID;

    public Image CooldownUIRightClick;

    private bool shootable = true;
    private float timer;
    private Vector3 distanceOfMouse;
    private GameObject currentObjectPool;
    
    // Start is called before the first frame update
    void Start()
    {
        EID = GameObject.Find("RuneManager").GetComponent<ElementManager>().publicAccessElementDatabase;
        mainCam = Camera.main;
        SetElement(0);
        shootable = true;
        CooldownUIRightClick.type = Image.Type.Filled;
        CooldownUIRightClick.fillAmount = 0;
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
        distanceOfMouse = vectorBetween;
        float rotation = -(Mathf.Atan2(vectorBetween.z, vectorBetween.x) * Mathf.Rad2Deg);
        rotationPlayerToCursor = Quaternion.Euler(transform.rotation.x, rotation + 90, transform.rotation.z);
        transform.rotation = rotationPlayerToCursor;

        if (Input.GetMouseButton(1) && shootable)
        {
            ShootBullet();
        }
    }

    public void SetElement(int elementIndex)
    {
        element = EID.elements[elementIndex];
        if(currentObjectPool != null)
        {
            Destroy(currentObjectPool);
        }

        if(element.optionalObjectPool != null)
        {
            currentObjectPool = Instantiate(element.optionalObjectPool, new Vector3(10000, 10000, 10000), element.optionalObjectPool.transform.rotation);
        }
    }

    public void ShootBullet()
    {
        
        GameObject shotBullet = Instantiate(element.projectileShape, transform.position, transform.rotation);
        Rigidbody bulletRB = shotBullet.GetComponent<Rigidbody>();
        bulletRB.AddRelativeForce(bulletRB.velocity.x, bulletRB.velocity.y, -element.projectileSpeed, ForceMode.Impulse);
        StartCoroutine(TimedDestruction(shotBullet));
        shootable = false;

        if (element.mouseDistance)
        {
            shotBullet.transform.position = shotBullet.transform.position - distanceOfMouse;
        }


        StartCoroutine(ShotCooldown(element.shotCooldownTime));
        

    }

    public IEnumerator TimedDestruction(GameObject currentBullet)
    {
        yield return new WaitForSeconds(element.projectileLifetime);
        if(currentBullet != null)
        {
            Destroy(currentBullet);
        }
    }

    public IEnumerator ShotCooldown(float cooldown)
    {
        CooldownUIRightClick.fillAmount = 1;
        
        while (timer <= cooldown)
        {
            timer += Time.deltaTime;

            if(CooldownUIRightClick != null)
            {
                CooldownUIRightClick.fillAmount = -((timer / cooldown) - 1);
            }

            yield return null;
        }

        shootable = true;
        timer = 0;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, aimCursor.transform.position);
    }
}
