using NUnit.Framework;
using ObjectValidator.Common;
using System.Collections;
using System.Collections.Generic;

namespace UnitTest.Common
{
    [TestFixture]
    public class ObservableCollection_Test
    {
        [Test]
        public void Test_ObservableCollection_IsReadOnly()
        {
            var list = new ObservableCollection<int>();
            Assert.False(list.IsReadOnly);
        }

        [Test]
        public void Test_ObservableCollection_Count()
        {
            var list = new ObservableCollection<int>();
            Assert.AreEqual(0, list.Count);
            list.Add(3);
            Assert.AreEqual(1, list.Count);
            list.Add(3);
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void Test_ObservableCollection_Index()
        {
            var list = new ObservableCollection<int>() { 4, 5, 6 };
            Assert.AreEqual(4, list[0]);
            Assert.AreEqual(5, list[1]);
            Assert.AreEqual(6, list[2]);

            list.Add(3);
            Assert.AreEqual(4, list.Count);
            list.CollectionChanged += (o, e) =>
            {
                Assert.AreSame(list, o);
                Assert.AreEqual(NotifyCollectionChangedAction.Replace, e.Action);
                Assert.IsNotNull(e.NewItems);
                Assert.AreEqual(1, e.NewItems.Count);
                Assert.AreEqual(7, e.NewItems[0]);
                Assert.IsNotNull(e.OldItems);
                Assert.AreEqual(1, e.OldItems.Count);
                Assert.AreEqual(5, e.OldItems[0]);
            };
            list[1] = 7;
            Assert.AreEqual(7, list[1]);
        }

        [Test]
        public void Test_ObservableCollection_Remove()
        {
            var list = new ObservableCollection<int>() { 4, 5, 6 };
            Assert.AreEqual(3, list.Count);
            list.Remove(4);
            Assert.AreEqual(2, list.Count);
            CollectionAssert.DoesNotContain(list, 4);
            var collection = list as ICollection<int>;
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection.Remove(5));
            Assert.AreEqual(1, collection.Count);
            CollectionAssert.DoesNotContain(list, 5);

            list = new ObservableCollection<int>() { 4, 5, 6 };
            list.CollectionChanged += (o, e) =>
            {
                Assert.AreSame(list, o);
                Assert.AreEqual(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.IsNull(e.NewItems);
                Assert.IsNotNull(e.OldItems);
                Assert.AreEqual(1, e.OldItems.Count);
                Assert.AreEqual(5, e.OldItems[0]);
            };
            list.RemoveAt(1);
            Assert.AreEqual(2, list.Count);
            CollectionAssert.DoesNotContain(list, 5);
        }

        [Test]
        public void Test_ObservableCollection_Enumerator()
        {
            var list = new ObservableCollection<int>() { 4, 5, 6 };
            Assert.IsNotNull(list.GetEnumerator());
            var enumerable = list as IEnumerable;
            Assert.IsNotNull(enumerable);
            Assert.IsNotNull(enumerable.GetEnumerator());
        }

        [Test]
        public void Test_ObservableCollection_Add()
        {
            var list = new ObservableCollection<int>();
            Assert.AreEqual(0, list.Count);
            list.Add(3);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(3, list[0]);
            list.Add(4);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(4, list[1]);
            list.Insert(0, 5);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(5, list[0]);
            Assert.AreEqual(3, list[1]);
            Assert.AreEqual(4, list[2]);
            list.Insert(2, 6);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(5, list[0]);
            Assert.AreEqual(3, list[1]);
            Assert.AreEqual(6, list[2]);
            Assert.AreEqual(4, list[3]);
            list.CollectionChanged += (o, e) =>
            {
                Assert.AreSame(list, o);
                Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
                Assert.IsNull(e.OldItems);
                Assert.IsNotNull(e.NewItems);
                Assert.AreEqual(1, e.NewItems.Count);
                Assert.AreEqual(7, e.NewItems[0]);
            };
            list.Add(7);
            list = new ObservableCollection<int>() { 3 };
            list.CollectionChanged += (o, e) =>
            {
                Assert.AreSame(list, o);
                Assert.AreEqual(NotifyCollectionChangedAction.Add, e.Action);
                Assert.IsNull(e.OldItems);
                Assert.IsNotNull(e.NewItems);
                Assert.AreEqual(1, e.NewItems.Count);
                Assert.AreEqual(7, e.NewItems[0]);
            };
            list.Insert(0, 7);
        }

        [Test]
        public void Test_ObservableCollection_IndexOf()
        {
            var list = new ObservableCollection<int>() { 6, 5, 8 };
            Assert.AreEqual(0, list.IndexOf(6));
            Assert.AreEqual(1, list.IndexOf(5));
            Assert.AreEqual(2, list.IndexOf(8));
        }

        [Test]
        public void Test_ObservableCollection_Clear()
        {
            var list = new ObservableCollection<int>() { 6, 5, 8 };
            Assert.AreEqual(3, list.Count);
            list.CollectionChanged += (o, e) =>
            {
                Assert.AreSame(list, o);
                Assert.AreEqual(NotifyCollectionChangedAction.Reset, e.Action);
                Assert.IsNull(e.NewItems);
                Assert.IsNotNull(e.OldItems);
                Assert.AreEqual(3, e.OldItems.Count);
                Assert.AreEqual(6, e.OldItems[0]);
                Assert.AreEqual(5, e.OldItems[1]);
                Assert.AreEqual(8, e.OldItems[2]);
            };
            list.Clear();
        }

        [Test]
        public void Test_ObservableCollection_Contains()
        {
            var list = new ObservableCollection<int>() { 6, 5, 8 };
            Assert.AreEqual(3, list.Count);
            Assert.True(list.Contains(6));
            Assert.True(list.Contains(5));
            Assert.True(list.Contains(8));
            Assert.False(list.Contains(9));
        }

        [Test]
        public void Test_ObservableCollection_CopyTo()
        {
            var list = new ObservableCollection<int>() { 6, 5, 8 };
            var array = new int[5];
            list.CopyTo(array, 1);
            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(6, array[1]);
            Assert.AreEqual(5, array[2]);
            Assert.AreEqual(8, array[3]);
            Assert.AreEqual(0, array[4]);
        }
    }
}