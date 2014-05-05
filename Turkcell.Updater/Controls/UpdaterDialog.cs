using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Turkcell.Updater.Controls
{
    /// <summary>
    ///     CustomDialog class which is used showing <see cref="Update" /> and <see cref="Message" /> details.
    /// </summary>
    public class UpdaterDialog : ContentControl
    {
        /*
         * A Popup will automatically change orientation if you add it as a child of some element on your page, 
         * for example LayoutRoot. Then the Popup will rotate along with its parent. When you programmatically create a Popup in C#, 
         * it is by default independent of everything else on the page and you have to detect orientation change and rotate it yourself.
         * The remedy is to make it part of the visual tree of your page by making it the child of some element.
         */

        private const int ButtonsRowHeight = 100;

        /// <summary>
        ///     The height of the system tray in pixels when the page
        ///     is in portrait mode.
        /// </summary>
        private const double PortraitStatusBarHeight = 32.0;

        /// <summary>
        ///     The width of the system tray in pixels when the page
        ///     is in landscape mode.
        /// </summary>
        private const double LandscapeStatusBarWidth = 72.0;

        /// <summary>
        ///     Identifies the Title dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof (string), typeof (UpdaterDialog),
                                        new PropertyMetadata(string.Empty, null));


        /// <summary>
        ///     Identifies the PositiveButtonText dependency property.
        /// </summary>
        public static readonly DependencyProperty PositiveButtonTextProperty =
            DependencyProperty.Register("PositiveButtonText", typeof (string), typeof (UpdaterDialog),
                                        new PropertyMetadata(string.Empty, null));

        /// <summary>
        ///     Identifies the PositiveButtonText dependency property.
        /// </summary>
        public static readonly DependencyProperty NegativeButtonTextProperty =
            DependencyProperty.Register("NegativeButtonText", typeof (string), typeof (UpdaterDialog),
                                        new PropertyMetadata(string.Empty, null));


        /// <summary>
        ///     Identifies the PositiveButtonCommand.
        /// </summary>
        public static readonly DependencyProperty PositiveButtonCommandProperty =
            DependencyProperty.Register("PositiveButtonCommand", typeof (ICommand), typeof (UpdaterDialog),
                                        new PropertyMetadata(null, null));


        /// <summary>
        ///     Identifies the PositiveButtonCommand.
        /// </summary>
        public static readonly DependencyProperty NegativeButtonCommandProperty =
            DependencyProperty.Register("NegativeButtonCommand", typeof (ICommand), typeof (UpdaterDialog),
                                        new PropertyMetadata(null, null));

        private static readonly double ScreenHeight = Application.Current.Host.Content.ActualHeight;
        private static readonly double ScreenWidth = Application.Current.Host.Content.ActualWidth;


        private readonly Popup _popup;

        private PhoneApplicationFrame _frame;
        private Button _negativeButton;
        private PhoneApplicationPage _page;
        private Button _positiveButton;
        private ScrollViewer _scrollContent;
        private Grid _transparentContainer;
        private TextBlock _txtTitle;

        /// <summary>
        /// </summary>
        public UpdaterDialog()
        {
            IsCancellable = true;
            DefaultStyleKey = typeof (UpdaterDialog);
            Background = new SolidColorBrush(Colors.Black);
            _popup = new Popup();
        }

        internal Boolean IsCancellable { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the Positive Button's Content property.
        /// </summary>
        public string PositiveButtonText
        {
            get { return (string) GetValue(PositiveButtonTextProperty); }
            set { SetValue(PositiveButtonTextProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the Positive Button's Content property.
        /// </summary>
        public string NegativeButtonText
        {
            get { return (string) GetValue(NegativeButtonTextProperty); }
            set { SetValue(NegativeButtonTextProperty, value); }
        }

        /// <summary>
        ///     Sets the <see cref="ICommand" /> to invoke when the Positive button tapped.
        /// </summary>
        public ICommand PositiveButtonCommand
        {
            get { return (ICommand) GetValue(PositiveButtonCommandProperty); }
            set { SetValue(PositiveButtonCommandProperty, value); }
        }

        /// <summary>
        ///     Sets the <see cref="ICommand" /> to invoke when the Positive button tapped.
        /// </summary>
        public ICommand NegativeButtonCommand
        {
            get { return (ICommand) GetValue(NegativeButtonCommandProperty); }
            set { SetValue(NegativeButtonCommandProperty, value); }
        }

        /// <summary>
        ///     When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call
        ///     <see
        ///         cref="M:System.Windows.Controls.Control.ApplyTemplate" />
        ///     . In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate()
        {
            if (_negativeButton != null)
            {
                _negativeButton.Tap -= NegativeButtonTap;
            }

            if (_positiveButton != null)
            {
                _positiveButton.Tap -= NegativeButtonTap;
            }

            base.OnApplyTemplate();

            _txtTitle = GetTemplateChild("TxtTitle") as TextBlock;
            _transparentContainer = GetTemplateChild("TransparentContainer") as Grid;
            _scrollContent = GetTemplateChild("ScrollContent") as ScrollViewer;

            _positiveButton = GetTemplateChild("BtnPositive") as Button;
            _negativeButton = GetTemplateChild("BtnNegative") as Button;

            if (_negativeButton != null)
            {
                _negativeButton.Tap += NegativeButtonTap;
            }

            if (_positiveButton != null)
            {
                _positiveButton.Tap += NegativeButtonTap;
            }

            if (_transparentContainer != null && _page != null)
            {
                _transparentContainer.Height = _page.ActualHeight;
                _transparentContainer.Width = _page.ActualWidth;
            }

            if (String.IsNullOrEmpty(Title) && _txtTitle != null)
            {
                _txtTitle.Visibility = Visibility.Collapsed;
            }

            if (String.IsNullOrEmpty(PositiveButtonText) && _positiveButton != null)
            {
                _positiveButton.Visibility = Visibility.Collapsed;
            }

            AdjustPopupPosition();
        }

        /// <summary>
        ///     Fired when the dialog is dismissed because of Back button press.
        /// </summary>
        internal event EventHandler<EventArgs> Dismissed;

        /// <summary>
        ///     Called when the popup is dismissed by a BackKey press.
        /// </summary>
        protected virtual void OnDismissed()
        {
            EventHandler<EventArgs> handler = Dismissed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void NegativeButtonTap(object sender, GestureEventArgs e)
        {
            CloseDialog();
        }

        private void CleanAsync()
        {
            try
            {
                if (_page != null)
                {
                    _page.BackKeyPress -= PageBackKeyPress;
                    _page.OrientationChanged -= _page_OrientationChanged;
                }

                if (_negativeButton != null)
                {
                    _negativeButton.Tap -= NegativeButtonTap;
                }

                if (_positiveButton != null)
                {
                    _positiveButton.Tap -= NegativeButtonTap;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        ///     Makes contents of the <see cref="UpdaterDialog" /> instance visible with an overlaying <see cref="Popup" /> control.
        /// </summary>
        public void Show()
        {
            if (_popup.IsOpen)
                return;
            ApplyTemplate();
            _frame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (_frame != null)
            {
                _page = _frame.Content as PhoneApplicationPage;
                if (_page != null)
                {
                    _page.BackKeyPress -= PageBackKeyPress;
                    _page.BackKeyPress += PageBackKeyPress;
                    _page.OrientationChanged -= _page_OrientationChanged;
                    _page.OrientationChanged += _page_OrientationChanged;

                    _popup.Child = this;
                    _popup.Height = _frame.ActualHeight;
                    _popup.Width = _frame.ActualWidth;
                    _popup.IsOpen = true;
                    _popup.Visibility = Visibility.Visible;
                }
            }
        }

        private void _page_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            AdjustPopupPosition();
        }

        private Transform GetTransform()
        {
            if (_page != null)
            {
                PageOrientation orientation = _page.Orientation;

                switch (orientation)
                {
                    case PageOrientation.LandscapeLeft:
                    case PageOrientation.Landscape:
                        return new CompositeTransform {Rotation = 90, TranslateX = ScreenWidth};
                    case PageOrientation.LandscapeRight:
                        return new CompositeTransform {Rotation = -90, TranslateY = ScreenHeight};
                }
            }
            return null;
        }

        private void AdjustPopupPosition()
        {
            if (_scrollContent != null)
            {
                _scrollContent.MaxHeight = _page.ActualHeight - ButtonsRowHeight;
                if ((PageOrientation.Landscape & _page.Orientation) > 0)
                    _scrollContent.MinHeight = _page.ActualHeight - ButtonsRowHeight;
                else
                    _scrollContent.ClearValue(MinHeightProperty);
            }

            if (_transparentContainer != null)
            {
                _transparentContainer.RenderTransform = GetTransform();
                _transparentContainer.Width = _page.ActualWidth;
                _transparentContainer.Height = _page.ActualHeight;
            }

            if (SystemTray.IsVisible && _page != null && SystemTray.GetOpacity(_page) > 0)
            {
                switch (_page.Orientation)
                {
                    case PageOrientation.Portrait:
                    case PageOrientation.PortraitUp:
                    case PageOrientation.PortraitDown:
                        _popup.VerticalOffset = PortraitStatusBarHeight;
                        _popup.HorizontalOffset = 0;
                        break;
                    case PageOrientation.LandscapeLeft:
                        _popup.HorizontalOffset = 0;
                        _popup.VerticalOffset = LandscapeStatusBarWidth;
                        break;
                    case PageOrientation.LandscapeRight:
                        _popup.VerticalOffset = 0;
                        _popup.HorizontalOffset = 0;
                        break;
                }
            }
        }

        private void PageBackKeyPress(object sender, CancelEventArgs e)
        {
            if (_popup.IsOpen && IsCancellable)
            {
                e.Cancel = true;
                CloseDialog();
                OnDismissed();
            }
            else if (_popup.IsOpen && !IsCancellable)
            {
                e.Cancel = true;
            }
        }

        private void CloseDialog()
        {
            _popup.IsOpen = false;
            CleanAsync();
        }
    }
}