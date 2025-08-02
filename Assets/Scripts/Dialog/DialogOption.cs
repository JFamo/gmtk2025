using System.Collections.Generic;

namespace Dialog {
    public class DialogOption {
        public string optionText;
        private DialogOptionContext _context;
        private List<IDialogOptionSelectHandler> _selectHandlers;

        public DialogOption(string text) {
            optionText = text;
            _context = new DialogOptionContext();
            _selectHandlers = new List<IDialogOptionSelectHandler> { new NoOpDialogOptionSelectHandler() };
        }

        public DialogOption(string text, IDialogOptionSelectHandler handler)
        {
            optionText = text;
            _context = new DialogOptionContext();
            _selectHandlers = new List<IDialogOptionSelectHandler> { handler ?? new NoOpDialogOptionSelectHandler() };
        }

        public DialogOption(string text, IEnumerable<IDialogOptionSelectHandler> handlers, DialogOptionContext context)
        {
            optionText = text;
            _context = context;
            _selectHandlers = new List<IDialogOptionSelectHandler>(handlers ?? new[] { new NoOpDialogOptionSelectHandler() });
            if (_selectHandlers.Count == 0)
                _selectHandlers.Add(new NoOpDialogOptionSelectHandler());
        }

        public IReadOnlyList<IDialogOptionSelectHandler> GetSelectHandlers()
        {
            return _selectHandlers.AsReadOnly();
        }

        public void AddSelectHandler(IDialogOptionSelectHandler handler)
        {
            if (handler != null)
                _selectHandlers.Add(handler);
        }

        public DialogOptionContext GetContext()
        {
            return _context;
        }
    }
}