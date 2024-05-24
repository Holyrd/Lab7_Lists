using System;
using System.Collections.Generic;

public class CustomList
{
	private double[] items;
	private int count;

	public CustomList()
	{
		items = new double[0];
		count = 0;
	}

	private CustomList(double[] items)
	{
		this.items = items;
		count = items.Length;
	}

	private void Append(double item)
	{
		EnsureCapacity(count + 1);
		items[count] = item;
		count++;
	}

	public void Prepend(double item)
	{
		EnsureCapacity(count + 1);
		Array.Copy(items, 0, items, 1, count);
		items[0] = item;
		count++;
	}

	public double this[int index]
	{
		get
		{
			if (index < 0 || index >= count)
				throw new IndexOutOfRangeException("Index out of range");
			return items[index];
		}
		private set
		{
			if (index < 0 || index >= count)
				throw new IndexOutOfRangeException("Index out of range");
			items[index] = value;
		}
	}

	public int Count
	{
		get { return count; }
	}

	public void RemoveAt(int index)
	{
		if (index < 0 || index >= count)
			throw new IndexOutOfRangeException("Index out of range");
		Array.Copy(items, index + 1, items, index, count - index - 1);
		count--;
	}

	private void Insert(int index, double item)
	{
		if (index < 0 || index > count)
			throw new IndexOutOfRangeException("Index out of range");
		EnsureCapacity(count + 1);
		Array.Copy(items, index, items, index + 1, count - index);
		items[index] = item;
		count++;
	}

	public override string ToString()
	{
		double[] result = new double[count];
		Array.Copy(items, result, count);
		return string.Join(", ", result);
	}

	private void EnsureCapacity(int min)
	{
		if (items.Length < min)
		{
			int newCapacity = items.Length == 0 ? 4 : items.Length * 2;
			if (newCapacity < min) newCapacity = min;
			double[] newItems = new double[newCapacity];
			Array.Copy(items, newItems, count);
			items = newItems;
		}
	}

	public double GetAverage()
	{
		double sum = 0;
		for (int i = 0; i < count; i++)
		{
			sum += items[i];
		}
		return sum / count;
	}

	public double? FindFirstGreaterThanAverage()
	{
		double average = GetAverage();
		for (int i = 0; i < count; i++)
		{
			if (items[i] > average)
			{
				return items[i];
			}
		}
		return null;
	}

	public double SumGreaterThan(double value)
	{
		double sum = 0;
		for (int i = 0; i < count; i++)
		{
			if (items[i] > value)
			{
				sum += items[i];
			}
		}
		return sum;
	}

	public CustomList GetLessThanAverage()
	{
		double average = GetAverage();
		CustomList newList = new CustomList();
		for (int i = 0; i < count; i++)
		{
			if (items[i] < average)
			{
				newList.Append(items[i]);
			}
		}
		return newList;
	}

	public void RemoveEvenIndexed()
	{
		CustomList temp = new CustomList();
		double[] revArr = new double[count];
		for (int i = 0; i < count; i++)
		{
			revArr[i] = items[i];
		}
		Array.Reverse(revArr);
		for (int i = 0; i < count; i++)
		{
			if (i % 2 != 0)
			{
				temp.Prepend(revArr[i]);
			}
		}
		this.items = temp.items;
		this.count = temp.count;
	}
}

public class Program
{
	public static void Main(string[] args)
	{
		CustomList myList = new CustomList();
		myList.Prepend(3.0);
		myList.Prepend(4.0);
		myList.Prepend(6.0);
		myList.Prepend(1.0);
		myList.Prepend(5.0);
		myList.Prepend(8.0);

		Console.WriteLine("Original List: " + myList); 

		double? firstGreaterThanAvg = myList.FindFirstGreaterThanAverage();
		Console.WriteLine("First element greater than average: "+ myList.GetAverage()+" -> " + (firstGreaterThanAvg.HasValue ? firstGreaterThanAvg.Value.ToString() : "None"));

		double sumGreaterThan = myList.SumGreaterThan(5);
		Console.WriteLine("Sum of elements greater than 2.9: " + sumGreaterThan);

		CustomList lessThanAverageList = myList.GetLessThanAverage();
		Console.WriteLine("Elements less than average: " + myList.GetAverage() + " -> " + lessThanAverageList);

		myList.RemoveEvenIndexed();
		Console.WriteLine("After removing elements at even positions: " + myList);
	}
}
