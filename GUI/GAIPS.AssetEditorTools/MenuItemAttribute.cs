using System;
using GAIPS.Rage;

namespace GAIPS.AssetEditorTools
{
	[AttributeUsage(AttributeTargets.Method,AllowMultiple = false,Inherited = true)]
	public class MenuItemAttribute : Attribute
	{
		public readonly string ItemName;

		public int Priority { get; set; } = 0;

		public MenuItemAttribute(string itemName)
		{
			ItemName = itemName;
		}
	}
}