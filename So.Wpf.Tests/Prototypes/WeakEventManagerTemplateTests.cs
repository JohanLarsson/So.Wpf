namespace So.Wpf.Tests.Prototypes
{
    using System;
    using System.Windows;
    using NUnit.Framework;

    public class WeakEventManagerTemplateTests
    {
        ////public string ClassName { get; private set; }
        ////public Type SourceType { get; private set; }
        ////public string EventHandlerType { get; private set; }
        ////public string EventName { get; private set; }
        [Test]
        public void WriteCodeTest()
        {
            var template = new WeakEventManagerTemplate();
            RoutedEvent routedEvent = FrameworkElement.SizeChangedEvent;
            Console.WriteLine(template.WriteCode("So.Wpf.Misc", routedEvent));
        }
    }
}
