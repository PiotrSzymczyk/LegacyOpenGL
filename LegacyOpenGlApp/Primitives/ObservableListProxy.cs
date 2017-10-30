using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace LegacyOpenGlApp.Primitives
{
	public class ObservableListProxy<T> : IList<T>, INotifyCollectionChanged
	{
		public event NotifyCollectionChangedEventHandler CollectionChanged;
		
		private Func<IList<T>> _ListAccessor { get; }

		public T this[int index]
		{
			get => _ListAccessor.Invoke()[index];
			set
			{
				if (index < 0 || index >= Count)
					throw new IndexOutOfRangeException("The specified index is out of range.");
				var oldItem = _ListAccessor.Invoke()[index];
				_ListAccessor.Invoke()[index] = value;
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
			}
		}

		public int Count => _ListAccessor.Invoke().Count;

		public bool IsReadOnly => _ListAccessor.Invoke().IsReadOnly;


		public ObservableListProxy(Func<IList<T>> accessor)
		{
			_ListAccessor = accessor;
		}


		private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
		{
			CollectionChanged?.Invoke(this, args);
		}

		public int IndexOf(T item)
		{
			return _ListAccessor.Invoke().IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			_ListAccessor.Invoke().Insert(index, item);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= Count)
				throw new IndexOutOfRangeException("The specified index is out of range.");
			var oldItem = _ListAccessor.Invoke()[index];
			_ListAccessor.Invoke().RemoveAt(index);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItem, index));
		}

		public void Add(T item)
		{
			_ListAccessor.Invoke().Add(item);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
		}

		public void Clear()
		{
			_ListAccessor.Invoke().Clear();
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public bool Contains(T item)
		{
			return _ListAccessor.Invoke().Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_ListAccessor.Invoke().CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			var result = _ListAccessor.Invoke().Remove(item);
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
			return result;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _ListAccessor.Invoke().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void CopyTo(Array array, int index)
		{
			((IList)_ListAccessor.Invoke()).CopyTo(array, index);
		}
	}
}
