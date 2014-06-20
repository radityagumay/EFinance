using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EFI.ViewModels;
using EFI.Events;
using EFI.Framework;
using MicroIoc;
using Microsoft.Practices.Prism.Events;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EFI.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        #region Members
        private BaseEvent baseEvent;

        /// <summary>
        /// Swipe Gesture
        /// http://depblog.weblogs.us/2013/07/30/facebook-like-settings-pane-with-gestures-windows-phone/
        /// </summary>
        private readonly VisualStates _visualStates;

        private double _dragDistanceToOpen = 75.0;
        private double _dragDistanceToClose = 305.0;
        private double _dragDistanceNegative = -75.0;

        private FrameworkElement _feContainer;

        private bool _isSettingsOpen = false;
        #endregion

        #region View Model
        private MainPageViewModel _viewModel
        {
            get { return (this.DataContext as MainPageViewModel); }
        }
        #endregion

        #region Constructor
        public MainPage()
        {
            InitializeComponent();
            IMicroIocContainer container =
                (IMicroIocContainer)Application.Current.Resources["Container"];
            this.DataContext = container.TryResolve<MainPageViewModel>();

            IEventAggregator eventAggregator = container.Resolve<IEventAggregator>();
            baseEvent = eventAggregator.GetEvent<BaseEvent>();

            _visualStates = new VisualStates(this);

            _feContainer = this.Container as FrameworkElement;

            this.Loaded += MainPage_Loaded;
        }
        #endregion

        #region Method
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.OnLoadedCommand.Execute(e);
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (_isSettingsOpen)
                CloseSettings();
            else
                OpenSettings();
        }

        private void OpenSettings()
        {
            var trans = _feContainer.GetHorizontalOffset().Transform;
            trans.Animate(trans.X, 380, TranslateTransform.XProperty, 300, 0, new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            });

            _isSettingsOpen = true;
        }

        private void CloseSettings()
        {
            var trans = _feContainer.GetHorizontalOffset().Transform;
            trans.Animate(trans.X, 0, TranslateTransform.XProperty, 300, 0, new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            });

            _isSettingsOpen = false;
        }

        private void ResetLayoutRoot()
        {
            if (!_isSettingsOpen)
                _feContainer.SetHorizontalOffset(0.0);
            else
                _feContainer.SetHorizontalOffset(380.0);
        }

        private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal && e.HorizontalChange > 0 && !_isSettingsOpen)
            {
                double offset = _feContainer.GetHorizontalOffset().Value + e.HorizontalChange;
                if (offset > _dragDistanceToOpen)
                    this.OpenSettings();
                else
                    _feContainer.SetHorizontalOffset(offset);
            }

            if (e.Direction == System.Windows.Controls.Orientation.Horizontal && e.HorizontalChange < 0 && _isSettingsOpen)
            {
                double offsetContainer = _feContainer.GetHorizontalOffset().Value + e.HorizontalChange;
                if (offsetContainer < _dragDistanceToClose)
                    this.CloseSettings();
                else
                    _feContainer.SetHorizontalOffset(offsetContainer);
            }
        }

        private void GestureListener_DragCompleted(object sender, DragCompletedGestureEventArgs e)
        {
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal && e.HorizontalChange > 0 && !_isSettingsOpen)
            {
                if (e.HorizontalChange < _dragDistanceToOpen)
                    this.ResetLayoutRoot();
                else
                    this.OpenSettings();
            }

            if (e.Direction == System.Windows.Controls.Orientation.Horizontal && e.HorizontalChange < 0 && _isSettingsOpen)
            {
                if (e.HorizontalChange > _dragDistanceNegative)
                    this.ResetLayoutRoot();
                else
                    this.CloseSettings();
            }
        }


        #region Visual States
        /// <summary>
        /// An internal helper class for handling MainPage visual state transitions.
        /// </summary>
        private class VisualStates
        {
            #region Predefined Visual States
            // Settings State
            public const string ShowSettings = "ShowSettings";
            public const string HideSettings = "HideSettings";
            #endregion

            #region Fields
            private readonly Control _control;
            private string _settingsStates = VisualStates.HideSettings;
            #endregion

            #region Properties
            /// <summary>
            /// Change the route panel visual state.
            /// </summary>
            public string SettingsStates
            {
                get { return _settingsStates; }
                set
                {
                    if (_settingsStates != value)
                    {
                        _settingsStates = value;
                        VisualStateManager.GoToState(_control, value, true);
                    }
                }
            }
            #endregion

            #region Ctor
            /// <summary>
            /// Initializes a new instance of this class.
            /// </summary>
            /// <param name="control">The target control.</param>
            public VisualStates(Control control)
            {
                _control = control;
            }
            #endregion
        }
        #endregion

        #endregion
    }
}