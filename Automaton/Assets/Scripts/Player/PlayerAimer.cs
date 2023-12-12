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
    public ElementInfoDatabase.Element element1;
    public ElementInfoDatabase.Element element2;
    public ElementInfoDatabase EID;

    public Image CooldownUIRightClick;
    public Image CooldownUILeftClick;

    public Transform leftShootSpot;
    public Transform rightShootSpot;

    private bool shootable1 = true;
    private bool shootable2 = true;
    private float timer;
    private float timer2;
    private Vector3 distanceOfMouse;
    private GameObject currentObjectPool1;
    private GameObject currentObjectPool2;

    // Start is called before the first frame update
    void Start()
    {
        EID = GameObject.Find("RuneManager").GetComponent<ElementManager>().publicAccessElementDatabase;
        mainCam = Camera.main;
        SetElement(1,0);
        SetElement(2,0);
        shootable1 = true;
        shootable2 = true;
        CooldownUIRightClick.type = Image.Type.Filled;
        CooldownUIRightClick.fillAmount = 0;

        CooldownUILeftClick.type = Image.Type.Filled;
        CooldownUILeftClick.fillAmount = 0;
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

        if(Input.GetMouseButton(0) && shootable1)
        {
            ShootBullet(1);
        }

        if (Input.GetMouseButton(1) && shootable2)
        {
            ShootBullet(2);
        }
    }

    public void SetElement(int gunIndex,int elementIndex)
    {
        if (gunIndex == 1)
        {
            element1 = EID.elements[elementIndex];
            if (currentObjectPool1 != null)
            {
                Destroy(currentObjectPool1);
            }

            if (element1.optionalObjectPool != null)
            {
                currentObjectPool1 = Instantiate(element1.optionalObjectPool, new Vector3(10000, 10000, 10000), element1.optionalObjectPool.transform.rotation);
            }
        }
        else if(gunIndex == 2)
        {
            element2 = EID.elements[elementIndex];
            if (currentObjectPool2 != null)
            {
                Destroy(currentObjectPool2);
            }

            if (element2.optionalObjectPool != null)
            {
                currentObjectPool2 = Instantiate(element2.optionalObjectPool, new Vector3(10000, 10000, 10000), element2.optionalObjectPool.transform.rotation);
            }
        }
    }

    public void ShootBullet(int shotID)
    {
        if(shotID == 1)
        {
            GameObject shotBullet = Instantiate(element1.projectileShape, leftShootSpot.transform.position, leftShootSpot.transform.rotation);
            Rigidbody bulletRB = shotBullet.GetComponent<Rigidbody>();
            bulletRB.AddRelativeForce(bulletRB.velocity.x, bulletRB.velocity.y, -element1.projectileSpeed, ForceMode.Impulse);
            StartCoroutine(TimedDestruction1(shotBullet));
            shootable1 = false;

            if (element1.mouseDistance)
            {
                shotBullet.transform.position = shotBullet.transform.position - distanceOfMouse;
            }


            StartCoroutine(ShotCooldown1(element1.shotCooldownTime));
        }
        else if(shotID == 2)
        {
            GameObject shotBullet = Instantiate(element2.projectileShape, rightShootSpot.transform.position, rightShootSpot.transform.rotation);
            Rigidbody bulletRB = shotBullet.GetComponent<Rigidbody>();
            bulletRB.AddRelativeForce(bulletRB.velocity.x, bulletRB.velocity.y, -element1.projectileSpeed, ForceMode.Impulse);
            StartCoroutine(TimedDestruction2(shotBullet));
            shootable2 = false;

            if (element2.mouseDistance)
            {
                shotBullet.transform.position = shotBullet.transform.position - distanceOfMouse;
            }


            StartCoroutine(ShotCooldown2(element2.shotCooldownTime));
        }
    }

    public IEnumerator TimedDestruction1(GameObject currentBullet)
    {
        yield return new WaitForSeconds(element1.projectileLifetime);
        if(currentBullet != null)
        {
            Destroy(currentBullet);
        }
    }

    public IEnumerator TimedDestruction2(GameObject currentBullet)
    {
        yield return new WaitForSeconds(element2.projectileLifetime);
        if (currentBullet != null)
        {
            Destroy(currentBullet);
        }
    }

    public IEnumerator ShotCooldown1(float cooldown)
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

        shootable1 = true;
        timer = 0;
    }

    public IEnumerator ShotCooldown2(float cooldown)
    {
        CooldownUILeftClick.fillAmount = 1;

        while (timer2 <= cooldown)
        {
            timer2 += Time.deltaTime;

            if (CooldownUILeftClick != null)
            {
                CooldownUILeftClick.fillAmount = -((timer2 / cooldown) - 1);
            }

            yield return null;
        }

        shootable2 = true;
        timer2 = 0;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, aimCursor.transform.position);
    }
}
