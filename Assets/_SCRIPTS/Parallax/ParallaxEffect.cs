using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float startPos, length;

    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startPos = this.transform.position.x;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            length = sr.bounds.size.x;
        if (cam == null && Camera.main != null)
            cam = Camera.main.gameObject;
    }
    private void LateUpdate()
    {
        if (cam == null || length <= 0f)
            return;

        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance,
            transform.position.y, transform.position.z);

        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}
