using UnityEngine;
using UnityEngine.UI;

namespace Utility.UI.Progress_Bar.Filler
{
    [RequireComponent(typeof(Image))]
    public class FillerController : MonoBehaviour
    {
        [SerializeField] protected Image fillerImage;

        private void OnValidate()
        {
            if (fillerImage == null)
            {
                fillerImage = GetComponent<Image>();
            }
        }

        /// <summary>
        /// Set fill amount of image.
        /// </summary>
        /// <param name="amount">amount must be in range of [of, 1f]</param>
        public void SetFill(float amount)
        {
            fillerImage.fillAmount = amount < 0f ? 0f : amount > 1f ? 1f : amount;
        }
    }
}
