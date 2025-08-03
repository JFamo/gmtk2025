using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Controllers.ui {
    public class DrinkDisplayController : StateChangeSubscriber {
        public GameObject drinkIconPrefab;
        public Transform canvasParent;
        public TMP_Text text;
        
        private List<GameObject> _drinkIcons;
        
        private void Start() {
            _drinkIcons = new List<GameObject>();
            int drinks = PlayerStateController.GetInstance().GetDrinks();
            // ShowDrinkValue(health);
            ShowDrinkText(drinks);
            PlayerStateController.GetInstance().Subscribe(this);
        }

        private void ShowDrinkText(int amount)
        {
            // Show the drink text as "x Drinks"
            text.text = "x " + amount;
        }

        private void ShowDrinkValue(int amount) {
            foreach(GameObject icon in _drinkIcons)
            {
                Destroy(icon);
            }
            _drinkIcons.Clear();
            for (int i = 0; i < amount; i++) {
                GameObject newIcon = Instantiate(drinkIconPrefab, canvasParent);
                _drinkIcons.Add(newIcon);
                newIcon.GetComponent<RectTransform>().anchoredPosition = GetDrinkIconPosition(i);
            }
        }
        
        // Function to get the position for the Nth drink icon
        private Vector2 GetDrinkIconPosition(int index) {
            RectTransform rt = drinkIconPrefab.GetComponent<RectTransform>();
            return new Vector3(rt.anchoredPosition.x - (index * rt.sizeDelta.x), rt.anchoredPosition.y, 0); // Adjust the spacing as needed
        }

        public override void OnDrinkChange(int newValue) {
            // ShowDrinkValue(newValue);
            ShowDrinkText(newValue);
        }
    }
}
