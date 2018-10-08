using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LPFramework {
    public class OperatingSystem : MonoBehaviour {
        #region Variables

        [Header("Main Properties")]
        public Screen startScreen;

        [Header("System Events")]
        public UnityEvent onSwitchedScreen = new UnityEvent();

        [Header("Fader Properties")]
        public Image fader;
        public float fadeInDuration = 1f;
        public float fadeOutDuration = 1f;

        private Screen[] _screens = new Screen[0];
        private Screen _previousScreen;
        private Screen _currentScreen;

        #endregion

        #region Main

        private void Start() {
            _screens = GetComponentsInChildren<Screen>(true);
            InitializeScreens();

            if (startScreen) {
                SwitchScreen(startScreen);
            }

            if (fader) {
                fader.gameObject.SetActive(true);
            }

            FadeIn();
        }

        #endregion

        #region Helper Methods

        public void FadeIn() {
            if (fader) {
                fader.CrossFadeAlpha(0f, fadeInDuration, false);
            }
        }

        public void FadeOut() {
            if (fader) {
                fader.CrossFadeAlpha(1f, fadeOutDuration, false);
            }
        }

        public void SwitchScreen(Screen screen) {
            if (screen) {
                if (_currentScreen) {
                    _currentScreen.CloseScreen();
                    _previousScreen = _currentScreen;
                }

                _currentScreen = screen;
                _currentScreen.gameObject.SetActive(true);
                _currentScreen.StartScreen();

                if (onSwitchedScreen != null) {
                    onSwitchedScreen.Invoke();
                }
            }
        }

        public void GotoPreviousScreen() {
            if (_previousScreen) {
                SwitchScreen(_previousScreen);
            }
        }

        public void LoadScene(int sceneIndex) {
            StartCoroutine(WaitToLoadScene(sceneIndex));
        }

        private IEnumerator WaitToLoadScene(int sceneIndex) {
            yield return null;
        }

        private void InitializeScreens() {
            foreach (Screen screen in _screens) {
                screen.gameObject.SetActive(true);
            }
        }

        #endregion
    }
}