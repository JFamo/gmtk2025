using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialog {
    public class DialogPanelController : MonoBehaviour {

        [Header("Links and Prefabs")]
        public GameObject dialogPanel;
        public GameObject optionPrefab;
        public UnityEngine.UI.Image dialogImage;
        public TMP_Text dialogText;
        public TMP_Text dialogName;

        [Header("Dialog Config")] public Color defaultOptionColor = Color.black;
        public Color selectedOptionColor = Color.magenta;
        public float characterPrintRate = 0.1f;
        public float optionSpacing = 10.0f;
        
        [Header("Debug")]
        public Sprite debugImage; // Assign a sprite for debugging purposes

        private int selectedOption;
        private bool hasRenderedOptions = false;
        private List<GameObject> options = new List<GameObject>();
        
        // Singleton instance
        private static DialogPanelController _instance;
        
        public static DialogPanelController GetInstance() {
            if (_instance == null) {
                Debug.LogError("DialogPanelController singleton is null! Please ensure it is initialized in the scene.");
            }
            return _instance;
        }
        
        private void Awake() {
            if (_instance == null) {
                _instance = this;
            } else {
                Debug.LogWarning("Multiple instances of DialogPanelController detected. Using the existing instance.");
                Destroy(gameObject);
            }
            dialogPanel.SetActive(false);
        }

        void Update() {
            if (hasRenderedOptions) {
                if (Input.GetKeyDown(KeyCode.W)) {
                    // Move selection up
                    selectedOption = Mathf.Max(0, selectedOption - 1);
                    ColorSelectedOption();
                }
                if (Input.GetKeyDown(KeyCode.S)) {
                    // Move selection down
                    selectedOption = Mathf.Min(options.Count - 1, selectedOption + 1);
                    ColorSelectedOption();
                }

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.D)) {
                    CloseDialog();
                }
            }
        }

        public void LaunchDialog(DialogInstance dialogInstance) {
            dialogPanel.SetActive(true);
            dialogImage.sprite = dialogInstance.image;
            dialogName.text = dialogInstance.name;
            StartCoroutine(PrintCharacters(dialogInstance));
            Time.timeScale = 0; // Pause the game
        }

        public void CloseDialog() {
            ClearOptions();
            dialogPanel.SetActive(false);
            Time.timeScale = 1; // Resume the game
        }

        private void RenderOptions(DialogInstance dialogInstance) {
            selectedOption = 0;
            ClearOptions();
            if (dialogInstance.options == null || dialogInstance.options.Count == 0) {
                Debug.LogError("DialogInstance has no options to render.");
                return;
            }

            for (int i = 0; i < dialogInstance.options.Count; i++) {
                GameObject option = Instantiate(optionPrefab, dialogPanel.transform);
                RectTransform optionRect = option.GetComponent<RectTransform>();
                optionRect.anchoredPosition = GetOptionPosition(i);
                TMP_Text optionText = option.GetComponentInChildren<TMP_Text>();
                optionText.text = dialogInstance.options[i].optionText;
                options.Add(option);
            }

            ColorSelectedOption();
            hasRenderedOptions = true;
        }
        
        private Vector2 GetOptionPosition(int index) {
            RectTransform rt = optionPrefab.GetComponent<RectTransform>();
            return new Vector3(rt.anchoredPosition.x, rt.anchoredPosition.y - (index * rt.sizeDelta.y) - optionSpacing, 0);
        }

        private void ClearOptions() {
            hasRenderedOptions = false;
            foreach (GameObject opt in options) {
                Destroy(opt);
            }
            options.Clear();
        }

        private void ColorSelectedOption() {
            for(int i = 0; i< options.Count; i++) {
                options[i].GetComponentInChildren<TMP_Text>().color = i == selectedOption ? selectedOptionColor : defaultOptionColor;
            }
        }
        
        // Use a coroutine to print one character at a time
        public IEnumerator PrintCharacters(DialogInstance dialogInstance) {
            dialogText.text = "";
            foreach (char c in dialogInstance.text) {
                dialogText.text += c;
                yield return new WaitForSecondsRealtime(characterPrintRate);
            }
            RenderOptions(dialogInstance);
        }
        
    }
}