using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JLN.Controls.Tests
{
    class OcExtTests
    {
        [Test, Explicit("Deadlocks")]
        public async Task InvokeAsyncTest()
        {
            var ints = new ObservableCollection<int>();
            await ints.InvokeAsync(() => ints.Add(3));
            Assert.AreEqual(3, ints.Single());
        }

        [Test, Explicit("Deadlocks")]
        public async Task AddRangeAsyncTest()
        {
            var ints = new ObservableCollection<int> { 1, 2, 3 };
            await ints.AddRangeAsync(Enumerable.Range(4, 6)); //Hangs
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5, 6, 7, 8 }, ints);
        }

        [Test]
        public void InvokeAddRangeTest()
        {
            var ints = new ObservableCollection<int>();
            ints.InvokeAddRange(Enumerable.Range(1, 2));
            CollectionAssert.AreEqual(new[] { 1, 2 }, ints);
        }

        [Test]
        public void InvokeAddTest()
        {
            var ints = new ObservableCollection<int>();
            ints.InvokeAdd(1);
            CollectionAssert.AreEqual(new[] { 1 }, ints);
        }

        [Test]
        public void InvokeRemoveTest()
        {
            var ints = new ObservableCollection<int> { 1, 2, 1 };
            ints.InvokeRemove(1);
            CollectionAssert.AreEqual(new[] { 2, 1 }, ints);
        }

        [Test]
        public void InvokeClearTest()
        {
            var ints = new ObservableCollection<int> { 1, 2, 1 };
            ints.InvokeClear();
            CollectionAssert.AreEqual(new int[] { }, ints);
        }
    }
}
