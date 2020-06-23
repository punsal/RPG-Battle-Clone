using UnityEngine;

namespace Character.Graphics
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterGraphicsController : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        
        public void Construct(Sprite sprite)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
        }
    }
}