using UnityEngine;

namespace Editor_Helpers
{
    public class DrawSphere : MonoBehaviour
    {
        [SerializeField] private Color color = Color.green;
        [SerializeField] private float radius = 1f;

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
