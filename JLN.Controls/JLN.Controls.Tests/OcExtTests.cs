using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JLN.Controls.Tests
{
    class OcExtTests
    {
        [Test]
        public async Task AddRangeAsyncTest()
        {
            var ints = new ObservableCollection<int> {1, 2, 3};
            await ints.AddRangeAsync(Enumerable.Range(4, 6));
            CollectionAssert.AreEqual(new[]{1,2,3,4,5,6,7,8},ints);
        }
        [Test]
        public void AddRangeTest()
        {
            var ints = new ObservableCollection<int> ();
            ints.AddRange(Enumerable.Range(1, 2));
            CollectionAssert.AreEqual(new[] { 1, 2}, ints);
        }
        [Test, RequiresSTA]
        public async Task InvokeAsyncTest()
        {
            var ints = new ObservableCollection<int>();
            await ints.InvokeAsync(() => ints.Add(3));
            Assert.AreEqual(3,ints.Single());
        }
    }
}
