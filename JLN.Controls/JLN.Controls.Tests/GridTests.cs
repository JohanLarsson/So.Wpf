using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NUnit.Framework;

namespace JLN.Controls.Tests
{
    class GridTests
    {
        [Test, RequiresSTA]
        public void ColumnsTest()
        {
            var grid = new Grid();
            JLN.Controls.AttachedProperties.Grid.SetRows(grid,"*;*;*");
        }
    }
}
