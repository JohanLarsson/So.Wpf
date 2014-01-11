namespace So.Wpf.Tests
{
    using System.Globalization;
    using Controls;
    using NUnit.Framework;
    public class DoubleBoxTests
    {
        [RequiresSTA]
        [TestCase("100", 3, true, "101", 3)]
        [TestCase("100", 2, true, "110", 2)]
        [TestCase("100", 1, true, "200", 1)]
        [TestCase("100", 0, true, "1100", 1)]
        [TestCase("400", 3, false, "399", 3)]
        [TestCase("400", 2, false, "390", 2)]
        [TestCase("400", 1, false, "300", 1)]
        [TestCase("400", 0, false, "-600", 1)]
        [TestCase("1.1", 0, true, "11.1", 1)]
        [TestCase("1.1", 1, true, "2.1", 1)]
        [TestCase("1.1", 2, true, "2.1", 2)]
        [TestCase("1.1", 3, false, "1.0", 3)]
        public void Update(string text, int caretIndex, bool increase, string expectedText, int expectedCaretIndex)
        {
            var doubleBox = new NumericBox
            {
                Text = text,
                CaretIndex = caretIndex
            };
            Assert.AreEqual(caretIndex, doubleBox.CaretIndex);
            doubleBox.Update(increase, CultureInfo.InvariantCulture);
            Assert.AreEqual(expectedText, doubleBox.Text);
            Assert.AreEqual(expectedCaretIndex, doubleBox.CaretIndex);
        }
    }
}
