using UnityEngine;

namespace Utility.Custom.Scene
{
    public class DrawCircle : MonoBehaviour
    {
        [SerializeField] private Color customColor = Color.green;
        [SerializeField] private float customRadius = 1f;

        private void OnDrawGizmos()
        {
            Gizmos.color = customColor;
            Gizmos.DrawSphere(transform.position, customRadius);
        }
    }
}