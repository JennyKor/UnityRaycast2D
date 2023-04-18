using System.Collections;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private LineRenderer lineRenderer;

    public int damage = 5;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (hitInfo)
        {
            EnemyHealth enemy = hitInfo.transform.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Debug.Log(hitInfo.transform.name);

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }

        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 50);
        }

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.04f);

        lineRenderer.enabled = false;
    }
}
