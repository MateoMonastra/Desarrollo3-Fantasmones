using UnityEngine;
using UnityEngine.UI;

namespace Minigames
{
    public class AD : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        public void SetProgressBarFill( float value )
        {
            progressBar.fillAmount = value;
        }
    }
}