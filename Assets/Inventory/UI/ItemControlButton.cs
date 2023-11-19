using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemControlButton : MonoBehaviour
    {
        private event Action _onClickAction;
        
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        
        public void Show(string text, Action onClickAction)
        {
            ResetAll();

            _text.text = text;
            _onClickAction = onClickAction;
            _button.onClick.AddListener(OnClick);
            _button.interactable = true;
            
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void OnClick()
        {
            _onClickAction?.Invoke();
        }

        private void ResetAll()
        {
            _button.onClick.RemoveAllListeners();
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            _text ??= GetComponentInChildren<TMP_Text>();
            _button ??= GetComponentInChildren<Button>();
            _button.interactable = false;
        }

#endif
    }
}
