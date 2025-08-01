using System.Collections.Generic;
using UnityEngine;

namespace Controllers.ui {
    public class LifeDisplayController : StateChangeSubscriber {
        public GameObject lifeIconPrefab;
        public Transform canvasParent;
        
        private List<GameObject> _lifeIcons;
        
        private void Start() {
            _lifeIcons = new List<GameObject>();
            int health = PlayerStateController.GetInstance().GetHealth();
            ShowHealthValue(health);
            PlayerStateController.GetInstance().Subscribe(this);
        }

        private void ShowHealthValue(int amount) {
            foreach(GameObject icon in _lifeIcons)
            {
                Destroy(icon);
            }
            _lifeIcons.Clear();
            for (int i = 0; i < amount; i++) {
                GameObject newIcon = Instantiate(lifeIconPrefab, canvasParent);
                _lifeIcons.Add(newIcon);
                newIcon.GetComponent<RectTransform>().anchoredPosition = GetLifeIconPosition(i);
            }
        }
        
        // Function to get the position for the Nth life icon
        private Vector2 GetLifeIconPosition(int index) {
            RectTransform rt = lifeIconPrefab.GetComponent<RectTransform>();
            return new Vector3(rt.anchoredPosition.x - (index * rt.sizeDelta.x), rt.anchoredPosition.y, 0); // Adjust the spacing as needed
        }

        public override void OnHealthChange(int newValue) {
            ShowHealthValue(newValue);
        }
    }
}
