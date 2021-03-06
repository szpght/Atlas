﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Atlas.UI.Events;

namespace Atlas.UI
{
    public class AtlasTabControl : TabControl
    {
        private TabPanel TabPanel { get; set; }

        private Button ScrollLeftButton { get; set; }
        private Button ScrollRightButton { get; set; }
        private ScrollViewer ScrollView { get; set; }

        public event EventHandler<TabCloseEventArgs> BeforeTabClosed;
        public event EventHandler TabClosed;

        public event EventHandler TabPanelDoubleClick;

        static AtlasTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AtlasTabControl), new FrameworkPropertyMetadata(typeof(AtlasTabControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            TabPanel = GetTemplateChild("PART_TabPanel") as TabPanel;

            ScrollLeftButton = GetTemplateChild("PART_ScrollLeft") as Button;
            ScrollRightButton = GetTemplateChild("PART_ScrollRight") as Button;
            ScrollView = GetTemplateChild("PART_Scroller") as ScrollViewer;

            if (TabPanel != null)
                TabPanel.MouseLeftButtonDown += TabPanel_MouseLeftButtonDown;

            if (ScrollLeftButton != null)
                ScrollLeftButton.Click += ScrollLeftButton_Click;

            if(ScrollRightButton != null)
                ScrollRightButton.Click += ScrollRightButton_Click;
        }

        private void ScrollRightButton_Click(object sender, RoutedEventArgs e)
        {
            ScrollView.ScrollToHorizontalOffset(ScrollView.HorizontalOffset + 25);
        }

        private void ScrollLeftButton_Click(object sender, RoutedEventArgs e)
        {
            ScrollView.ScrollToHorizontalOffset(ScrollView.HorizontalOffset - 25);
        }

        private void TabPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                TabPanelDoubleClick?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveTabItem(AtlasTabItem tabItem)
        {
            var beforeClosedArgs = new TabCloseEventArgs();
            BeforeTabClosed?.Invoke(this, beforeClosedArgs);

            if (!beforeClosedArgs.Cancel)
            {
                Items.Remove(tabItem);
                TabClosed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
