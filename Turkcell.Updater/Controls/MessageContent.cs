using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Turkcell.Updater.Controls
{
    /// <summary>
    /// Control to display properties of a <see cref="Message"/> instance.
    /// </summary>
    public class MessageContent : Control
    {
        /// <summary>
        /// Creates an instance of <see cref="MessageContent"/>
        /// </summary>
        public MessageContent()
        {
            DefaultStyleKey = typeof(MessageContent);
        }

        private Image _image;

        /// <summary>
        /// Gets or sets the Message.
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
            DependencyProperty.Register("Body", typeof(string), typeof(MessageContent), new PropertyMetadata(string.Empty, null));

        /// <summary>
        /// Gets or sets the ImageUrl.
        /// </summary>
        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        /// <summary>
        /// Identifies the ImageUrl dependency property.
        /// </summary>
        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.Register("ImageUrl", typeof(string), typeof(MessageContent), new PropertyMetadata(string.Empty, ImageUrlChangedChanged));

        private static void ImageUrlChangedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var messageContent = (MessageContent)d;
            if (messageContent._image != null && !String.IsNullOrEmpty((string)e.NewValue))
                messageContent._image.Source = new BitmapImage(new Uri((string)e.NewValue));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _image = GetTemplateChild("Image") as Image;
            if (_image != null)
            {
                if (!String.IsNullOrEmpty(ImageUrl))
                    _image.Source = new BitmapImage(new Uri(ImageUrl));
                _image.ImageFailed += _image_ImageFailed;
            }
        }

        void _image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            _image.Visibility = Visibility.Collapsed;            
        }
    }
}
