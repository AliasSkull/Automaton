using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWayIcons : MonoBehaviour
{
    public Image img;
    public GameObject iimage;

    public Transform target;
    private Camera cam;

    public Vector3 viewPos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float minX = img.GetPixelAdjustedRect().width * 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height * 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(new Vector3(target.position.x, target.position.y, target.position.z));

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.parent.position = pos;

        viewPos = cam.WorldToViewportPoint(target.position);

        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.x > 0)
        {
            img.gameObject.SetActive(false);
        }
        else
        {
            img.gameObject.SetActive(true);
        }

        Vector3 diff = (cam.WorldToScreenPoint(target.position) - img.transform.position).normalized;

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        Vector3 rot = new Vector3(iimage.transform.rotation.x, iimage.transform.rotation.y, angle + 90);
        iimage.transform.eulerAngles = rot;
    }
}
