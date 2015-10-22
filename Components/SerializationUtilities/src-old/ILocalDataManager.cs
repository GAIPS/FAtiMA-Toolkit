using System;

/// <summary>
/// Interface for the manager that handles all local data storage
/// </summary>
public interface ILocalDataManager
{
	bool ContainsData(Type type);
	bool ContainsData<T>() where T: class;
	
	object GetDataObject(Type type);
	T GetDataObject<T>() where T: class;
	
	void RegisterDataObject(object obj);
	
	void SaveState();
}