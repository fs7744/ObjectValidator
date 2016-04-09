using NUnit.Framework;
using ObjectValidator.Common;
using System.Collections.Generic;

namespace UnitTest.Common
{
    [TestFixture]
    public class NotifyCollectionChangedEventArgs_Test
    {
        [Test]
        public void Test_New()
        {
            var args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Add, new List<int> { 1 }, null);
            Assert.AreEqual(NotifyCollectionChangedAction.Add, args.Action);
            Assert.NotNull(args.NewItems);
            Assert.AreEqual(1, args.NewItems.Count);
            CollectionAssert.Contains(args.NewItems, 1);
            Assert.Null(args.OldItems);

            args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Move, new List<int> { 1 }, new List<int> { 3, 4 });
            Assert.AreEqual(NotifyCollectionChangedAction.Move, args.Action);
            Assert.NotNull(args.NewItems);
            Assert.AreEqual(1, args.NewItems.Count);
            CollectionAssert.Contains(args.NewItems, 1);
            Assert.NotNull(args.OldItems);
            Assert.AreEqual(2, args.OldItems.Count);
            CollectionAssert.Contains(args.OldItems, 3);
            CollectionAssert.Contains(args.OldItems, 4);

            args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Remove, null, new List<int> { 3, 4 });
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, args.Action);
            Assert.Null(args.NewItems);
            Assert.NotNull(args.OldItems);
            Assert.AreEqual(2, args.OldItems.Count);
            CollectionAssert.Contains(args.OldItems, 3);
            CollectionAssert.Contains(args.OldItems, 4);

            args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Replace, new List<int> { 1 }, new List<int> { 5 });
            Assert.AreEqual(NotifyCollectionChangedAction.Replace, args.Action);
            Assert.NotNull(args.NewItems);
            Assert.AreEqual(1, args.NewItems.Count);
            CollectionAssert.Contains(args.NewItems, 1);
            Assert.NotNull(args.OldItems);
            Assert.AreEqual(1, args.OldItems.Count);
            CollectionAssert.Contains(args.OldItems, 5);

            args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Reset, null, null);
            Assert.AreEqual(NotifyCollectionChangedAction.Reset, args.Action);
            Assert.Null(args.NewItems);
            Assert.Null(args.OldItems);
        }
    }
}