namespace So.Wpf.AttachedProperties
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// http://www.codeproject.com/Articles/39244/Scroll-Synchronization
    /// </summary>
    public class ScrollSynchronizer : DependencyObject
    {
        /// <summary>
        /// Identifies the attached property ScrollGroup
        /// </summary>
        public static readonly DependencyProperty ScrollGroupProperty = DependencyProperty.RegisterAttached(
            "ScrollGroup",
            typeof(string),
            typeof(ScrollSynchronizer),
            new PropertyMetadata("ScrollGroup1", OnScrollGroupChanged));

        /// <summary>
        /// List of all registered scroll viewers.
        /// </summary>
        private static readonly Dictionary<string, HashSet<ScrollViewer>> ScrollViewers = new Dictionary<string, HashSet<ScrollViewer>>();

        /// <summary>
        /// Contains the latest horizontal scroll offset for each scroll group.
        /// </summary>
        private static readonly Dictionary<string, double> HorizontalScrollOffsets = new Dictionary<string, double>();

        /// <summary>
        /// Contains the latest vertical scroll offset for each scroll group.
        /// </summary>
        private static readonly Dictionary<string, double> VerticalScrollOffsets = new Dictionary<string, double>();

        /// <summary>
        /// Sets the value of the attached property ScrollGroup.
        /// </summary>
        /// <param name="obj">Object on which the property should be applied.</param>
        /// <param name="scrollGroup">Value of the property.</param>
        public static void SetScrollGroup(DependencyObject obj, string scrollGroup)
        {
            obj.SetValue(ScrollGroupProperty, scrollGroup);
        }

        /// <summary>
        /// Gets the value of the attached property ScrollGroup.
        /// </summary>
        /// <param name="obj">Object for which the property should be read.</param>
        /// <returns>Value of the property StartTime</returns>
        public static string GetScrollGroup(DependencyObject obj)
        {
            return (string)obj.GetValue(ScrollGroupProperty);
        }

        /// <summary>
        /// Occurs, when the ScrollGroupProperty has changed.
        /// </summary>
        /// <param name="d">The DependencyObject on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void OnScrollGroupChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as ScrollViewer;
            if (scrollViewer == null)
            {
                return;
            }
            if (!string.IsNullOrEmpty((string)e.OldValue))
            {
                // Remove scrollviewer
                HashSet<ScrollViewer> scrollViewers;
                if (ScrollViewers.TryGetValue((string)e.NewValue, out scrollViewers))
                {
                    scrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged;
                    scrollViewers.Remove(scrollViewer);
                }
            }

            if (!string.IsNullOrEmpty((string)e.NewValue))
            {
                HashSet<ScrollViewer> groupScrollViewers;
                if (ScrollViewers.TryGetValue((string)e.NewValue, out groupScrollViewers))
                {
                    if (!groupScrollViewers.Any())
                    {
                        AddFirst((string)e.NewValue, scrollViewer);
                        scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                        return;
                    }
                    if (groupScrollViewers.Add(scrollViewer))
                    {
                        scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;

                        // If group already exists, set scrollposition of new scrollviewer to the scrollposition of the group
                        scrollViewer.ScrollToHorizontalOffset(HorizontalScrollOffsets[(string)e.NewValue]);
                        scrollViewer.ScrollToVerticalOffset(VerticalScrollOffsets[(string)e.NewValue]);
                    }
                }
                else
                {
                    AddFirst((string)e.NewValue, scrollViewer);
                    scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                }
            }
        }

        private static void AddFirst(string groupName, ScrollViewer scrollViewer)
        {
            ScrollViewers.Add(groupName, new HashSet<ScrollViewer> { scrollViewer });
            HorizontalScrollOffsets.Add(groupName, scrollViewer.HorizontalOffset);
            VerticalScrollOffsets.Add(groupName, scrollViewer.VerticalOffset);
        }

        /// <summary>
        /// Occurs, when the scroll offset of one scrollviewer has changed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">EventArgs of the event.</param>
        private static void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0 || e.HorizontalChange != 0)
            {
                var changedScrollViewer = sender as ScrollViewer;
                Scroll(changedScrollViewer);
            }
        }

        /// <summary>
        /// Scrolls all scroll viewers of a group to the position of the selected scroll viewer.
        /// </summary>
        /// <param name="changedScrollViewer">Sroll viewer, that specifies the current position of the group.</param>
        private static void Scroll(ScrollViewer changedScrollViewer)
        {
            var group = ScrollViewers.Single(x => x.Value.Contains(changedScrollViewer)).Key;
            VerticalScrollOffsets[group] = changedScrollViewer.VerticalOffset;
            HorizontalScrollOffsets[group] = changedScrollViewer.HorizontalOffset;

            foreach (var scrollViewer in ScrollViewers[group].Where(x => !ReferenceEquals(x, changedScrollViewer)))
            {
                scrollViewer.ScrollToVerticalOffset(changedScrollViewer.VerticalOffset);
                scrollViewer.ScrollToHorizontalOffset(changedScrollViewer.HorizontalOffset);
            }
        }
    }
}
