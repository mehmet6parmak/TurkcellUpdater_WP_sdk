using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Phone.Controls;

namespace Turkcell.Updater.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateMessageDialog : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateMessageDialog()
        {
            DefaultStyleKey = typeof(UpdateMessageDialog);
            Loaded += MessageDialog_Loaded;
            //_frame = Application.Current.RootVisual as PhoneApplicationFrame;
            //_page = _frame.Content as PhoneApplicationPage;

            _popup = new Popup();
        }

        void MessageDialog_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyTemplate();
        }

        private Grid _container;
        private TextBlock _txtMessage;
        private TextBlock _txtTitle;
        private Popup _popup;

        private readonly PhoneApplicationFrame _frame;
        private PhoneApplicationPage _page;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _container = GetTemplateChild("GridContainer") as Grid;
            _txtTitle = GetTemplateChild("TxtTitle") as TextBlock;
            _txtMessage = GetTemplateChild("TxtMessage") as TextBlock;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="showSecondButton"></param>
        public void Show(string title, string message, bool showSecondButton)
        {
            if (_popup.IsOpen)
                return;
            //_page.BackKeyPress += _page_BackKeyPress;

            _txtTitle.Text = title;
            _txtMessage.Text = message;

            _popup.Child = this;
            _popup.IsOpen = true;
            
        }

        void _page_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_popup.IsOpen)
            {
                e.Cancel = true;
                CloseDialog();
            }
        }

        private void CloseDialog()
        {
            _popup.IsOpen = false;
        }
    }
}
