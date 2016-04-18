using System;
using System.Collections.Generic;
using System.Linq;
using GAIPS.Rage;
using GAIPS.Serialization;
using IntegratedAuthoringTool.DTOs;
using RolePlayCharacter;

namespace IntegratedAuthoringTool
{
    [Serializable]
    public class IntegratedAuthoringToolAsset : LoadableAsset<IntegratedAuthoringToolAsset>, ICustomSerialization
    {
	    private class CharacterHolder
	    {
		    public string Source;
		    public RolePlayCharacterAsset RPCAsset;
	    }

	    private Dictionary<string, CharacterHolder> m_characterSources;
        //private IList<RolePlayCharacterAsset> Characters { get; set; }

        public string ScenarioName { get; set; }

	    protected override string OnAssetLoaded()
	    {
		    KeyValuePair<string, CharacterHolder> current = new KeyValuePair<string, CharacterHolder>();
		    try
		    {
				foreach (var pair in m_characterSources)
				{
					current = pair;

					if (pair.Value.RPCAsset == null)
					{
						pair.Value.RPCAsset = RolePlayCharacterAsset.LoadFromFile(ToAbsolutePath(pair.Value.Source));
						if (pair.Value.RPCAsset.ErrorOnLoad != null)
							return pair.Value.RPCAsset.ErrorOnLoad;
					}

					if (!string.Equals(pair.Key, pair.Value.RPCAsset.CharacterName))
						return $"Name mismatch. IAT name \"{pair.Key}\" != RPC File Name \"{pair.Value.RPCAsset.CharacterName}\" for file \"{ToAbsolutePath(pair.Value.Source)}\"";

				}
			}
		    catch (Exception)
		    {
			    return $"An error occured when trying to load the RPC \"{current.Key}\" at \"{ToAbsolutePath(current.Value.Source)}\". Please check if the path is correct.";

			}
		    return null;
		}

	    protected override void OnAssetPathChanged(string oldpath)
	    {
		    foreach (var holder in m_characterSources.Values)
		    {
				holder.Source = ToRelativePath(AssetFilePath, ToAbsolutePath(oldpath, holder.Source));
		    }
	    }

	    public IntegratedAuthoringToolAsset()
        {
			m_characterSources=new Dictionary<string, CharacterHolder>();
        }

        public IEnumerable<CharacterSourceDTO> GetAllCharacterSources()
        {
	        return m_characterSources.Select(p => new CharacterSourceDTO() {Name = p.Key, Source = ToAbsolutePath(p.Value.Source)});
        }

	    public RolePlayCharacterAsset GetCharacterAsset(string characterName)
	    {
		    CharacterHolder holder;
		    if (!m_characterSources.TryGetValue(characterName, out holder))
				throw new Exception($"Character \"{characterName}\" not found");

		    return holder.RPCAsset;
	    }

	    public IEnumerable<RolePlayCharacterAsset> GetAllCharacters()
	    {
		    return m_characterSources.Values.Select(h => h.RPCAsset);
	    } 

        public RolePlayCharacterAsset LoadAndAddCharacter(string filename)
        {
            var character = RolePlayCharacterAsset.LoadFromFile(filename);

			if(m_characterSources.ContainsKey(character.CharacterName))
				throw new Exception("A character with the same name already exists.");

			m_characterSources.Add(character.CharacterName,new CharacterHolder() {Source = ToRelativePath(filename),RPCAsset = character});
            return character;
        }

        public void RemoveCharacters(IList<string> charactersToRemove)
        {
            foreach (var characterName in charactersToRemove)
            {
	            m_characterSources.Remove(characterName);
            }   
        }

        #region Serialization

        public void GetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            dataHolder.SetValue("ScenarioName", ScenarioName);
            if (m_characterSources.Count>0)
            {
				dataHolder.SetValue("Characters", m_characterSources.Select(p => new CharacterSourceDTO() { Name = p.Key, Source = ToRelativePath(p.Value.Source) }).ToArray());
            }
        }

        public void SetObjectData(ISerializationData dataHolder, ISerializationContext context)
        {
            ScenarioName = dataHolder.GetValue<string>("ScenarioName");
            var charArray = dataHolder.GetValue<CharacterSourceDTO[]>("Characters");
            if (charArray != null)
				m_characterSources = charArray.ToDictionary(c => c.Name, c => new CharacterHolder() {Source = c.Source });
        }

        #endregion

    }
}
