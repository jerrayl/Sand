using System.Collections.Generic;
using Godot;

public static class Helpers
{
	public static List<T> GetChildrenOfType<T>(Node root) where T : class
	{
		var result = new List<T>();
		Recurse(root, result);
		return result;

		void Recurse(Node node, List<T> list)
		{
			foreach (Node child in node.GetChildren())
			{
				if (child is T tChild)
					list.Add(tChild);

				Recurse(child, list);
			}
		}
	}
}
