﻿using HandheldCompanion.Utils;
using System;
using System.Windows.Automation;

namespace HandheldCompanion.Misc
{
    public class ProcessWindow : IDisposable
    {
        private AutomationPropertyChangedEventHandler handler;
        public event EventHandler Refreshed;
        public event EventHandler Closed;

        public AutomationElement Element { get; private set; }
        public readonly int Hwnd;

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (!value.Equals(_Name))
                {
                    _Name = value;
                    Refreshed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private AutomationEventHandler _windowClosedHandler;

        public ProcessWindow(AutomationElement element, bool isPrimary)
        {
            Hwnd = element.Current.NativeWindowHandle;
            Element = element;

            handler = new AutomationPropertyChangedEventHandler(OnPropertyChanged);
            if (element.TryGetCurrentPattern(WindowPattern.Pattern, out object patternObj))
            {
                Automation.AddAutomationPropertyChangedEventHandler(
                    Element,
                    TreeScope.Element,
                    handler,
                    AutomationElement.NameProperty,
                    AutomationElement.BoundingRectangleProperty);

                _windowClosedHandler = OnWindowClosed;
                Automation.AddAutomationEventHandler(
                    WindowPattern.WindowClosedEvent,
                    element,
                    TreeScope.Subtree,
                    _windowClosedHandler);
            }

            RefreshName();
        }

        private void OnWindowClosed(object sender, AutomationEventArgs e)
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        ~ProcessWindow()
        {
            Dispose();
        }

        private void OnPropertyChanged(object sender, AutomationPropertyChangedEventArgs e)
        {
            try
            {
                if (Element != null)
                {
                    if (e.Property == AutomationElement.NameProperty)
                    {
                        RefreshName();
                    }
                    else if (e.Property == AutomationElement.BoundingRectangleProperty)
                    {
                        Refreshed?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
            catch { }
        }

        public void RefreshName()
        {
            try
            {
                string elementName = Element.Current.Name;
                if (!string.IsNullOrEmpty(elementName))
                {
                    Name = elementName;
                }
                else
                {
                    string title = ProcessUtils.GetWindowTitle(Hwnd);
                    if (!string.IsNullOrEmpty(title))
                        Name = title;
                }
            }
            catch { }
        }

        public void Dispose()
        {
            if (Element != null)
            {
                try
                {
                    if (handler != null)
                        Automation.RemoveAutomationPropertyChangedEventHandler(Element, handler);

                    // Remove the WindowClosed event handler
                    Automation.RemoveAutomationEventHandler(
                        WindowPattern.WindowClosedEvent,
                        Element,
                        _windowClosedHandler);
                }
                catch { }

                Element = null;
                handler = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
