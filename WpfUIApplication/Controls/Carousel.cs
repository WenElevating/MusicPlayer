using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace WpfUIApplication.Controls
{
    public class Carousel : UserControl
    {
        private List<string> _imageList = [];

        public Carousel()
        {
            Initial();
        }

        private void Initial()
        {
            var commonWidth = 350;

            for (int i = 1; i <= 9; ++i)
            {
                _imageList.Add($"D:\\CloudMusicKnowledge\\{i}.jpg");
            }

            // init grid
            var mainStackPanel = new StackPanel
            {
                Width = commonWidth,
                Height = 200,
                Orientation = Orientation.Vertical,
            };

            var scrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                VerticalScrollBarVisibility = ScrollBarVisibility.Disabled,
                Width = commonWidth,
            };

            var scrollPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Width = commonWidth * _imageList.Count,
            };

            foreach (var img in _imageList)
            {
                var tempImage = new System.Windows.Controls.Image
                {
                    Source = new BitmapImage(new Uri(img)),
                    Width = commonWidth,
                    Style = (Style)FindResource("RadiusImage")
                };
                scrollPanel.Children.Add(tempImage);
            }
            scrollPanel.RenderTransform = new TranslateTransform();

            scrollViewer.Content = scrollPanel;

            #region LoadedEventTrigger
            var loadTrigger = new EventTrigger
            {
                RoutedEvent = LoadedEvent
            };

            var loadKeyFrameAnimation = new DoubleAnimationUsingKeyFrames
            {
                RepeatBehavior = RepeatBehavior.Forever
            };

            for (int i = 1; i <= _imageList.Count; ++i)
            {
                loadKeyFrameAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(-400 * i, TimeSpan.FromSeconds(i * 2)));
            }

            Storyboard.SetTarget(loadKeyFrameAnimation, scrollPanel);
            Storyboard.SetTargetProperty(loadKeyFrameAnimation, new PropertyPath("(StackPanel.RenderTransform).(TranslateTransform.X)"));

            var loadStoryBoard = new Storyboard();
            loadStoryBoard.Children.Add(loadKeyFrameAnimation);

            var loadBeginStoryBoard = new BeginStoryboard();
            loadBeginStoryBoard.Name = "loadBeginBoard";
            loadBeginStoryBoard.Storyboard = loadStoryBoard;

            loadTrigger.Actions.Add(loadBeginStoryBoard);

            scrollPanel.Triggers.Add(loadTrigger);
            #endregion

            var canvas = new Canvas
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(150, 0, 0 ,0)
            };

            for (int i = 0; i < _imageList.Count; ++i)
            {
                var tempBorder = new Border
                {
                    BorderBrush = new SolidColorBrush(Colors.White),
                    BorderThickness = new System.Windows.Thickness(1),
                    Width = 8,
                    Height = 8,
                    CornerRadius = new System.Windows.CornerRadius(4),
                    Background = new SolidColorBrush(Colors.White),
                    Opacity = 0.8,
                    Name = $"PointBorder{i}",
                    Margin = new System.Windows.Thickness(0, 0, 10, 0),
                    Cursor = Cursors.Hand
                };

                #region MouseEnterTrigger
                var mouseTrigger = new EventTrigger
                {
                    RoutedEvent = MouseEnterEvent
                };

                var storyBoard = new Storyboard();

                var anmiation = new DoubleAnimation
                {
                    From = 0.8,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.1)
                };

                Storyboard.SetTargetProperty(anmiation, new PropertyPath(Border.OpacityProperty));
                Storyboard.SetTarget(anmiation, tempBorder);

                storyBoard.Children.Add(anmiation);


                // 展示图片动画
                var showImageAnimation = new DoubleAnimation
                {
                    From = -400 * i,
                    To = -400 * (i + 1),
                    Duration = TimeSpan.FromSeconds(1)
                };

                Storyboard.SetTarget(showImageAnimation, scrollPanel);
                Storyboard.SetTargetProperty(showImageAnimation, new PropertyPath("(StackPanel.RenderTransform).(TranslateTransform.X)"));

                storyBoard.Children.Add(showImageAnimation);

                var beginStoryBoard = new BeginStoryboard
                {
                    Storyboard = storyBoard
                };

                mouseTrigger.Actions.Add(beginStoryBoard);

                tempBorder.Triggers.Add(mouseTrigger);
                #endregion

                #region MouseLeaveTrigger
                var leaveTrigger = new EventTrigger
                {
                    RoutedEvent = MouseLeaveEvent
                };

                var leaveAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 0.8,
                    Duration = TimeSpan.FromSeconds(0.1)
                };

                Storyboard.SetTargetProperty(leaveAnimation, new PropertyPath(Border.OpacityProperty));
                Storyboard.SetTarget(leaveAnimation, tempBorder);

                var leaveStoryBorad = new Storyboard();
                leaveStoryBorad.Children.Add(leaveAnimation);

                var leaveBeginStoryBoard = new BeginStoryboard();
                leaveBeginStoryBoard.Storyboard = leaveStoryBorad;

                leaveTrigger.Actions.Add(leaveBeginStoryBoard);
                tempBorder.Triggers.Add(leaveTrigger);
                #endregion

                Canvas.SetRight(tempBorder, i * 20);
                Canvas.SetTop(tempBorder, -15);
                canvas.Children.Add(tempBorder);
            }

            mainStackPanel.Children.Add(scrollViewer);
            mainStackPanel.Children.Add(canvas);

            this.AddChild(mainStackPanel);
        }
    }
}
