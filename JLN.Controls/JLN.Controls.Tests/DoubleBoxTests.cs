using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JLN.Controls.Tests
{
    class DoubleBoxTests
    {
        [Test, RequiresSTA]
        public void IncreaseTest()
        {
            var doubleBox = new DoubleBox();
            doubleBox.Text = "3.14";
            doubleBox.CaretIndex = 1;

        }
    }
}
