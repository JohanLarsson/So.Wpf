using System.Security.Permissions;
using System.Windows.Threading;

namespace So.Wpf.Misc
{
    /// <summary>
    /// http://kentb.blogspot.se/2008/04/dispatcher-frames.html
    /// </summary>
    public static class DispatcherUtil
    {
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents(this Dispatcher dispatcher)
        {
            var frame = new DispatcherFrame();
            dispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        private static object ExitFrame(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}