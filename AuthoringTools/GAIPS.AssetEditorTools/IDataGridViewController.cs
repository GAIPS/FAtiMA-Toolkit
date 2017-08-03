using System.Collections;
using System.Collections.Generic;

namespace GAIPS.AssetEditorTools
{
	public interface IDataGridViewController
	{
		IList GetElements();
		object AddElement();
		IEnumerable<object> EditElements(IEnumerable<object> elementsToEdit);
		uint RemoveElements(IEnumerable<object> elementsToRemove);
	}
}