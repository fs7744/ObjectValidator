using ObjectValidator.Common;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.Common
{
    public class ObservableCollection_Test
    {
        [Fact]
        public void Test_ObservableCollection_IsReadOnly()
        {
            var list = new ObservableCollection<int>();
            Assert.False(list.IsReadOnly);
        }

        [Fact]
        public void Test_ObservableCollection_Count()
        {
            var list = new ObservableCollection<int>();
            Assert.Equal(0, list.Count);
            list.Add(3);
            Assert.Equal(1, list.Count);
            list.Add(3);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void Test_ObservableCollection_Index()
        {
            var list = new ObservableCollection<int>() { 4, 5, 6 };
            Assert.Equal(4, list[0]);
            Assert.Equal(5, list[1]);
            Assert.Equal(6, list[2]);

            list.Add(3);
            Assert.Equal(4, list.Count);
            list.CollectionChanged += (o, e) =>
            {
                Assert.Same(list, o);
                Assert.Equal(NotifyCollectionChangedAction.Replace, e.Action);
                Assert.NotNull(e.NewItems);
                Assert.Equal(1, e.NewItems.Count);
                Assert.Equal(7, e.NewItems[0]);
                Assert.NotNull(e.OldItems);
                Assert.Equal(1, e.OldItems.Count);
                Assert.Equal(5, e.OldItems[0]);
            };
            list[1] = 7;
            Assert.Equal(7, list[1]);
        }

        [Fact]
        public void Test_ObservableCollection_Remove()
        {
            var list = new ObservableCollection<int>() { 4, 5, 6 };
            Assert.Equal(3, list.Count);
            list.Remove(4);
            Assert.Equal(2, list.Count);
            Assert.DoesNotContain(4, list);
            var collection = list as ICollection<int>;
            Assert.NotNull(collection);
            Assert.True(collection.Remove(5));
            Assert.Equal(1, collection.Count);
            Assert.DoesNotContain(5, list);

            list = new ObservableCollection<int>() { 4, 5, 6 };
            list.CollectionChanged += (o, e) =>
            {
                Assert.Same(list, o);
                Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
                Assert.Null(e.NewItems);
                Assert.NotNull(e.OldItems);
                Assert.Equal(1, e.OldItems.Count);
                Assert.Equal(5, e.OldItems[0]);
            };
            list.RemoveAt(1);
            Assert.Equal(2, list.Count);
            Assert.DoesNotContain(5, list);
        }

        [Fact]
        public void Test_ObservableCollection_Enumerator()
        {
            var list = new ObservableCollection<int>() { 4, 5, 6 };
            Assert.NotNull(list.GetEnumerator());
            var enumerable = list as IEnumerable;
            Assert.NotNull(enumerable);
            Assert.NotNull(enumerable.GetEnumerator());
        }

        [Fact]
        public void Test_ObservableCollection_Add()
        {
            var list = new ObservableCollection<int>();
            Assert.Equal(0, list.Count);
            list.Add(3);
            Assert.Equal(1, list.Count);
            Assert.Equal(3, list[0]);
            list.Add(4);
            Assert.Equal(2, list.Count);
            Assert.Equal(4, list[1]);
            list.Insert(0, 5);
            Assert.Equal(3, list.Count);
            Assert.Equal(5, list[0]);
            Assert.Equal(3, list[1]);
            Assert.Equal(4, list[2]);
            list.Insert(2, 6);
            Assert.Equal(4, list.Count);
            Assert.Equal(5, list[0]);
            Assert.Equal(3, list[1]);
            Assert.Equal(6, list[2]);
            Assert.Equal(4, list[3]);
            list.CollectionChanged += (o, e) =>
            {
                Assert.Same(list, o);
                Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
                Assert.Null(e.OldItems);
                Assert.NotNull(e.NewItems);
                Assert.Equal(1, e.NewItems.Count);
                Assert.Equal(7, e.NewItems[0]);
            };
            list.Add(7);
            list = new ObservableCollection<int>() { 3 };
            list.CollectionChanged += (o, e) =>
            {
                Assert.Same(list, o);
                Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
                Assert.Null(e.OldItems);
                Assert.NotNull(e.NewItems);
                Assert.Equal(1, e.NewItems.Count);
                Assert.Equal(7, e.NewItems[0]);
            };
            list.Insert(0, 7);
        }

        [Fact]
        public void Test_ObservableCollection_IndexOf()
        {
            var list = new ObservableCollection<int>() { 6, 5, 8 };
            Assert.Equal(0, list.IndexOf(6));
            Assert.Equal(1, list.IndexOf(5));
            Assert.Equal(2, list.IndexOf(8));
        }

        [Fact]
        public void Test_ObservableCollection_Clear()
        {
            var list = new ObservableCollection<int>() { 6, 5, 8 };
            Assert.Equal(3, list.Count);
            list.CollectionChanged += (o, e) =>
            {
                Assert.Same(list, o);
                Assert.Equal(NotifyCollectionChangedAction.Reset, e.Action);
                Assert.Null(e.NewItems);
                Assert.NotNull(e.OldItems);
                Assert.Equal(3, e.OldItems.Count);
                Assert.Equal(6, e.OldItems[0]);
                Assert.Equal(5, e.OldItems[1]);
                Assert.Equal(8, e.OldItems[2]);
            };
            list.Clear();
        }

        [Fact]
        public void Test_ObservableCollection_Contains()
        {
            var list = new ObservableCollection<int>() { 6, 5, 8 };
            Assert.Equal(3, list.Count);
            Assert.True(list.Contains(6));
            Assert.True(list.Contains(5));
            Assert.True(list.Contains(8));
            Assert.False(list.Contains(9));
        }

        [Fact]
        public void Test_ObservableCollection_CopyTo()
        {
            var list = new ObservableCollection<int>() { 6, 5, 8 };
            var array = new int[5];
            list.CopyTo(array, 1);
            Assert.Equal(0, array[0]);
            Assert.Equal(6, array[1]);
            Assert.Equal(5, array[2]);
            Assert.Equal(8, array[3]);
            Assert.Equal(0, array[4]);
        }
    }
}