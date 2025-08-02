namespace Dialog {
    public class DialogOption {
        public string optionText;
        private DialogOptionContext _context;
        private IDialogOptionSelectHandler _selectHandler;
        
        public DialogOption(string text) {
            optionText = text;
            _context = new DialogOptionContext();
            _selectHandler = new NoOpDialogOptionSelectHandler();
        }
        
        public DialogOption(string text, IDialogOptionSelectHandler handler) 
        {
            optionText = text;
            _context = new DialogOptionContext();
            _selectHandler = handler ?? new NoOpDialogOptionSelectHandler();
        }
        
        public DialogOption(string text, IDialogOptionSelectHandler handler, DialogOptionContext context) 
        {
            optionText = text;
            _context = context;
            _selectHandler = handler ?? new NoOpDialogOptionSelectHandler();
        }
        
        public IDialogOptionSelectHandler GetSelectHandler() 
        {
            return _selectHandler;
        }

        public DialogOptionContext GetContext()
        {
            return _context;
        }
    }
}