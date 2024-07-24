using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject projectilePrefab; // Or a LineRenderer
    public float projectileSpeed = 10f;
    public float deleteDuration = 1f; // Duration for smooth deletion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create a projectile or cast a ray
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        }
        else
        {
            // Use a LineRenderer or other raycasting method
            Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If projectile hits something, delete it smoothly
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);

            // Smoothly delete the hit object
            Destroy(collision.collider.gameObject, deleteDuration);
            StartCoroutine(FadeOut(collision.collider.gameObject.GetComponent<Renderer>(), deleteDuration));
        }
    }

    IEnumerator FadeOut(Renderer renderer, float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = renderer.material.color;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            renderer.material.color = newColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.enabled = false;
    }
}
