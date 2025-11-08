using UnityEditor;
using UnityEngine;

public class AlertSystem : MonoBehaviour
{
    [SerializeField] private float fov = 45f;
    [SerializeField] private float radius = 10f;
    private float alertThreshold;

    private Animator animator;
    private static readonly int blinking = Animator.StringToHash("isBlinking");

    public LayerMask targetlayer;

    private Color color = new Color(0, 1, 0, 0.3f);
    private void Start()
    {
        animator = GetComponent<Animator>();
        alertThreshold = Mathf.Cos(fov / 2 * Mathf.Deg2Rad);
    }

    private void Update()
    {
        CheckAlert();
    }

    private void CheckAlert()
    {
        bool targetFound = false;

       Vector2 myUp = transform.rotation * Vector3.up;

        Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(transform.position, radius, targetlayer);


        foreach (Collider2D target in targetsInRadius)
        {
            if (target.transform == transform)
                continue;

            Vector2 targetDir = target.transform.position - transform.position;

            float dot = Vector2.Dot(myUp, targetDir.normalized);

            if (dot >= alertThreshold)
            {
                break;
            }
        }
        animator.SetBool(blinking, targetFound);
    }

    private void OnDrawGizmos()
    {
        Handles.color = color;

        Vector2 myUp = transform.rotation * Vector3.up;
        Vector2 startDirection = Quaternion.Euler(0, 0, -fov / 2) * myUp;

        Handles.DrawSolidArc(transform.position, Vector3.forward, startDirection, fov, radius);
    }
}