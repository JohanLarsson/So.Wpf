﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JLN.Controls
{
    /// <summary>
    /// http://wpftutorial.net/DataGrid.html
    /// </summary>
    public class CellTemplateDataGrid : DataGrid
    {
        public DataTemplateSelector CellTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(CellTemplateSelectorProperty); }
            set { SetValue(CellTemplateSelectorProperty, value); }
        }

        public static readonly DependencyProperty CellTemplateSelectorProperty =
            DependencyProperty.Register("Selector", typeof(DataTemplateSelector), typeof(CellTemplateDataGrid),
            new FrameworkPropertyMetadata(null));



        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Cancel = true;
            Columns.Add(new DataGridTemplateColumn
            {
                Header = e.Column.Header,
                CellTemplateSelector = CellTemplateSelector,
            });
        }
    }
}
