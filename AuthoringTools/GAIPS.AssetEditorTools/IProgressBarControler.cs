using System.Collections.Generic;

namespace GAIPS.AssetEditorTools
{
	public interface IProgressBarControler
	{
		float Percent { get; set; }
		string Message { get; set; }

		IEnumerable<IProgressBarControler> Split(int amount);
	}
}