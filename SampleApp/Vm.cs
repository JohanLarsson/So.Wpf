using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.Browser;

namespace SampleApp
{
    public class Vm
    {
        private readonly BrowserVm _browserVm = new BrowserVm();
        private List<string> _scrollItems = Enumerable.Range(0,50).Select(x=>"Item " + x).ToList();

        public Browser.BrowserVm BrowserVm
        {
            get { return _browserVm; }
        }

        public List<string> ScrollItems
        {
            get { return _scrollItems; }
            set { _scrollItems = value; }
        }
    }
}
