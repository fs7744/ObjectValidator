using ObjectValidator.Common;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.Common
{
    public class NotifyCollectionChangedEventArgs_Test
    {
        [Fact]
        public void Test_New()
        {
            var args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Add, new List<int> { 1 }, null);
            Assert.Equal(NotifyCollectionChangedAction.Add, args.Action);
            Assert.NotNull(args.NewItems);
            Assert.Equal(1, args.NewItems.Count);
            Assert.Contains(1, args.NewItems);
            Assert.Null(args.OldItems);

            args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Move, new List<int> { 1 }, new List<int> { 3, 4 });
            Assert.Equal(NotifyCollectionChangedAction.Move, args.Action);
            Assert.NotNull(args.NewItems);
            Assert.Equal(1, args.NewItems.Count);
            Assert.Contains(1, args.NewItems);
            Assert.NotNull(args.OldItems);
            Assert.Equal(2, args.OldItems.Count);
            Assert.Contains(3, args.OldItems);
            Assert.Contains(4, args.OldItems);

            args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Remove, null, new List<int> { 3, 4 });
            Assert.Equal(NotifyCollectionChangedAction.Remove, args.Action);
            Assert.Null(args.NewItems);
            Assert.NotNull(args.OldItems);
            Assert.Equal(2, args.OldItems.Count);
            Assert.Contains(3, args.OldItems);
            Assert.Contains(4, args.OldItems);

            args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Replace, new List<int> { 1 }, new List<int> { 5 });
            Assert.Equal(NotifyCollectionChangedAction.Replace, args.Action);
            Assert.NotNull(args.NewItems);
            Assert.Equal(1, args.NewItems.Count);
            Assert.Contains(1, args.NewItems);
            Assert.NotNull(args.OldItems);
            Assert.Equal(1, args.OldItems.Count);
            Assert.Contains(5, args.OldItems);

            args = new NotifyCollectionChangedEventArgs<int>(NotifyCollectionChangedAction.Reset, null, null);
            Assert.Equal(NotifyCollectionChangedAction.Reset, args.Action);
            Assert.Null(args.NewItems);
            Assert.Null(args.OldItems);
        }
    }
}