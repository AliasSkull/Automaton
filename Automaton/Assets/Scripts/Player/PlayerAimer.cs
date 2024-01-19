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

    public Image CooldownUIRightClickHold;
    public Image CooldownUILeftClickHold;

    public Transform leftShootSpot;
    public Transform rightShootSpot;

    private bool shootable1 = true;
    private bool shootable2 = true;
    private bool holdShootable1 = true;
    private bool holdShootable2 = true;
    private float timer;
    private float timer2;
    private float timerHold;
    private float timer2Hold;
    private Vector3 distanceOfMouse;
    private GameObject currentObjectPool1;
    private GameObject currentObjectPool2;

    [HideInInspector]
    public bool menuOpen;
    public PlayerController pc;

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

        CooldownUIRightClickHold.type = Image.Type.Filled;
        CooldownUIRightClickHold.fillAmount = 0;

        CooldownUILeftClickHold.type = Image.Type.Filled;
        CooldownUILeftClickHold.fillAmount = 0;
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

        if (!menuOpen)
        {
            if (Input.GetMouseButton(0) && shootable1 && holdShootable1)
            {
                ShootBullet(1);
            }
            else if (Input.GetMouseButtonUp(0) && element1.holdingSpell && holdShootable1)
            {
                holdShootable1 = false;
                StartCoroutine(ShotHoldCooldown1(element1.afterHoldCooldownTime));
            }

            if (Input.GetMouseButton(1) && shootable2 && holdShootable2)
            {
                ShootBullet(2);
            }
            else if (Input.GetMouseButtonUp(1) && element2.holdingSpell && holdShootable2)
            {
                holdShootable2 = false;
                StartCoroutine(ShotHoldCooldown2(element2.afterHoldCooldownTime));
            }

            if (Input.GetMouseButtonDown(0) && element1.slowPlayer)
            {
                pc.accelerationRate = 5f;
            }
            
            if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1) && element1.slowPlayer && pc.accelerationRate == 5)
            {
                pc.accelerationRate = 10f;
            }

            if (Input.GetMouseButtonDown(1) && element2.slowPlayer)
            {
                pc.accelerationRate = 5f;
            }

            if (Input.GetMouseButtonUp(1) && !Input.GetMouseButton(0) && element2.slowPlayer && pc.accelerationRate == 5)
            {
                pc.accelerationRate = 10f;
            }
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

            if (element1.holdingSpell)
            {
                CooldownUIRightClick.color = Color.yellow;
            }
            else
            {
                CooldownUIRightClick.color = Color.black;
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

            if (element2.holdingSpell)
            {
                CooldownUILeftClick.color = Color.yellow;
            }
            else
            {
                CooldownUILeftClick.color = Color.black;
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

    public IEnumerator ShotHoldCooldown1(float cooldown)
    {
        CooldownUIRightClick.gameObject.SetActive(false);
        CooldownUIRightClickHold.gameObject.SetActive(true);
        CooldownUIRightClickHold.fillAmount = 1;

        while (timerHold <= cooldown)
        {
            timerHold += Time.deltaTime;

            if (CooldownUIRightClickHold != null)
            {
                CooldownUIRightClickHold.fillAmount = -((timerHold / cooldown) - 1);
            }

            yield return null;
        }

        CooldownUIRightClick.gameObject.SetActive(true);
        CooldownUIRightClickHold.gameObject.SetActive(false);
        holdShootable1 = true;
        timerHold = 0;
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

    public IEnumerator ShotHoldCooldown2(float cooldown)
    {
        CooldownUILeftClick.gameObject.SetActive(false);
        CooldownUILeftClickHold.gameObject.SetActive(true);
        CooldownUILeftClickHold.fillAmount = 1;

        while (timer2Hold <= cooldown)
        {
            timer2Hold += Time.deltaTime;

            if (CooldownUILeftClickHold != null)
            {
                CooldownUILeftClickHold.fillAmount = -((timer2Hold / cooldown) - 1);
            }

            yield return null;
        }

        CooldownUILeftClick.gameObject.SetActive(true);
        CooldownUILeftClickHold.gameObject.SetActive(false);
        holdShootable2 = true;
        timer2Hold = 0;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, new Vector2(Input.GetAxis("AimJH") + transform.position.x, Input.GetAxis("AimJV") + transform.position.y));
    }
}
