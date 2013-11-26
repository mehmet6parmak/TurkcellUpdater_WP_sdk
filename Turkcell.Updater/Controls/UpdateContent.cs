using System.Windows;
using System.Windows.Controls;

namespace Turkcell.Updater.Controls
{
    /// <summary>
    /// Control to display properties of an <see cref="Update"/> instance.
    /// </summary>
    public class UpdateContent : Control
    {

        /// <summary>
        /// 
        /// </summary>
        public UpdateContent()
        {
            DefaultStyleKey = typeof(UpdateContent);
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// Identifies the Message dependency property.
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(UpdateContent), new PropertyMetadata(string.Empty, MessageChanged));

        private static void MessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var updateContent = (UpdateContent)d;
            if (updateContent._lblMessage != null)
                updateContent._lblMessage.Visibility = StringToVisibility((string)e.NewValue);
        }

        /// <summary>
        /// Gets or sets the warnings.
        /// </summary>
        public string Warnings
        {
            get { return (string)GetValue(WarningsProperty); }
            set { SetValue(WarningsProperty, value); }
        }

        /// <summary>
        /// Identifies the Warnings dependency property.
        /// </summary>
        public static readonly DependencyProperty WarningsProperty =
            DependencyProperty.Register("Warnings", typeof(string), typeof(UpdateContent), new PropertyMetadata(string.Empty, WarningsChanged));

        private static void WarningsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var updateContent = (UpdateContent)d;
            if (updateContent._gridWarnings != null)
                updateContent._gridWarnings.Visibility = StringToVisibility((string)e.NewValue);
        }

        /// <summary>
        /// Gets or sets the WhatIsNew.
        /// </summary>
        public string WhatIsNew
        {
            get { return (string)GetValue(WhatIsNewProperty); }
            set { SetValue(WhatIsNewProperty, value); }
        }

        /// <summary>
        /// Identifies the WhatIsNew dependency property.
        /// </summary>
        public static readonly DependencyProperty WhatIsNewProperty =
            DependencyProperty.Register("WhatIsNew", typeof(string), typeof(UpdateContent), new PropertyMetadata(string.Empty, WhatIsNewChanged));

        private static void WhatIsNewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var updateContent = (UpdateContent)d;
            if (updateContent._lblWhatIsNew != null)
                updateContent._lblWhatIsNew.Visibility = StringToVisibility((string)e.NewValue);
        }

        private Grid _gridWarnings;
        private TextBlock _lblMessage;
        private TextBlock _lblWhatIsNew;
        public async override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _gridWarnings = GetTemplateChild("GridWarnings") as Grid;
            if (_gridWarnings != null)
            {
                _gridWarnings.Visibility = StringToVisibility(Warnings);
            }

            _lblMessage = GetTemplateChild("LblMessage") as TextBlock;
            if (_lblMessage != null)
                _lblMessage.Visibility = StringToVisibility(Message);

            _lblWhatIsNew = GetTemplateChild("LblWhatIsNew") as TextBlock;
            if (_lblWhatIsNew != null)
                _lblWhatIsNew.Visibility = StringToVisibility(WhatIsNew);

        }


        private static Visibility StringToVisibility(string value)
        {
            return string.IsNullOrEmpty(value)
                                               ? Visibility.Collapsed
                                               : Visibility.Visible;
        }
    }
}
