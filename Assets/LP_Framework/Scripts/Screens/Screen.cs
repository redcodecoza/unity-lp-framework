using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LPFramework {
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class Screen : MonoBehaviour {
        #region Variables

        [Header("Main Properties")]
        public Selectable startSelectable;

        [Header("Screen Events")]
        public UnityEvent onScreenStart = new UnityEvent();
        public UnityEvent onScreenClose = new UnityEvent();

        private Animator _animator;

        #endregion

        #region Main

        private void Start() {
            _animator = GetComponent<Animator>();

            if (startSelectable) {
                EventSystem.current.SetSelectedGameObject(startSelectable.gameObject);
            }
        }

        #endregion

        #region Helper Methods

        public virtual void StartScreen() {
            if (onScreenStart != null) {
                onScreenStart.Invoke();
            }

            HandleAnimator("show");
        }
        
        public virtual void CloseScreen() {
            if (onScreenClose != null) {
                onScreenClose.Invoke();
            }
            
            HandleAnimator("hide");
        }

        private void HandleAnimator(string trigger) {
            if (_animator) {
                _animator.SetTrigger(trigger);
            }
        }
        #endregion
    }
}