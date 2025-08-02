using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialog {
    public class DialogInstance {
        public string name;
        public Sprite image;
        public string text;
        public List<DialogOption> options;
        
        public DialogInstance(string name, Sprite image, string text, List<DialogOption> options) {
            this.name = name;
            this.image = image;
            this.text = text;
            this.options = options;
        }
    }
}