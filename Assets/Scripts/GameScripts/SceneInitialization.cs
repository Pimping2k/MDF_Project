using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts
{
    public class SceneInitialization : MonoBehaviour
    {
        [SerializeField] private RawImage screen;
        [SerializeField] private float fadeSpeed = 0.5f;
        private WaitForSeconds waitForFade;
        private void OnEnable()
        {
            waitForFade = new WaitForSeconds(fadeSpeed);
            screen.CrossFadeAlpha(0f, fadeSpeed, false);
            StartCoroutine(DisableScreen());
        }

        private IEnumerator DisableScreen()
        {
            yield return waitForFade;
            screen.gameObject.SetActive(false);
        }
    }
}