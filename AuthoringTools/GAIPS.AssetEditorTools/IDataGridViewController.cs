using System;
using System.Collections;
using System.Collections.Generic;
using Equin.ApplicationFramework;

namespace GAIPS.AssetEditorTools
{
	public interface IDataGridViewController
	{
		IList GetElements();
        object AddElement();
		object EditElement(object elementToEdit);
        object DuplicateElement(object elementToDuplicate);
		uint RemoveElements(IEnumerable<object> elementsToRemove);
	}
}