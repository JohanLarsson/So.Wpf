using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using JLN.Controls.Misc;
using MS.Internal;
using NUnit.Framework;
using GridViewColumn = JLN.Controls.AttachedProperties.GridViewColumn;

namespace JLN.Controls.Tests
{
    internal class GridViewColumnWitdhTests
    {
        [Test, RequiresSTA]
        public void OneStarSizedTest()
        {
            var view = new GridView();
            var lw = new ListView();
            var gvc = new System.Windows.Controls.GridViewColumn();
            lw.View = view;
            var dependencyObjects = view.LogicalAncestors().ToArray();

        }

    }
}
