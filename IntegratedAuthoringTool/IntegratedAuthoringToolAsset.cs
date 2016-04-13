using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GAIPS.Serialization;
using IntegratedAuthoringTool.DTOs;
using RolePlayCharacter;

namespace IntegratedAuthoringTool
{
    [Serializable]
    public class IntegratedAuthoringToolAsset : ICustomSerialization
    {
        private IList<CharacterSourceDTO> CharacterSources { get; set; }
        private IList<RolePlayCharacterAsset> Characters { get; set; }

        public string ScenarioName { get; set; }
        
        public static IntegratedAuthoringToolAsset LoadFromFile(string filename)
        {
            IntegratedAuthoringToolAsset iat;

            using (var f = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                var serializer = new JSONSerializer();
                iat = serializer.Deserialize<IntegratedAuthoringToolAsset>(f);
            }

            iat.Characters = new List<RolePlayCharacterAsset>();
            foreach (var source in iat.CharacterSources)
            {
                var character = RolePlayCharacterAsset.LoadFromFile(source.Source);
                iat.Characters.Add(character);
            }

            return iat;
        }

        public IntegratedAuthoringToolAsset()
        {
            this.CharacterSources = new List<CharacterSourceDTO>();
            this.Characters = new List<RolePlayCharacterAsset>();
        }

        public IEnumerable<CharacterSourceDTO> GetAllCharacterSources()
        {
            return this.CharacterSources;
        }

        public IEnumerable<RolePlayCharacterAsset> GetAllCharacters()
        {
            return this.Characters;
        } 

        public void AddCharacter(string filename)
        {
            var character = RolePlayCharacterAsset.LoadFromFile(filename);

            if (this.Characters.Any(c => c.CharacterName == character.CharacterName))
            {
                throw new Exception("A character with the same name already exists.");
            }

            this.Characters.Add(character);
            this.CharacterSources.Add(new CharacterSourceDTO {Name = character.CharacterName, Source = filename});
        }

        public void RemoveCharacters(IList<string> charactersToRemove)
        {
            foreach (var characterName in charactersToRemove)
            {
                var characterSource = CharacterSources.FirstOrDefault(c => c.Name == characterName);
                var character = Characters.FirstOrDefault(c => c.CharacterName == characterName);
                if (characterSource != null && character != null)
                {
                    this.CharacterSources.Remove(characterSource);
                    this.Characters.Remove(character);
                }
                else
                {
                    throw new Exception("Invalid Operation");
                }
            }   
        }

        #region Serialization
        public void SaveToFile(Stream file)
        {
            var serializer = new JSONSerializer();
            serializer.Serialize(file, this);
        }

        public void GetObjectData(ISerializationData dataHolder)
        {
            dataHolder.SetValue("ScenarioName", ScenarioName);
            if (CharacterSources.Any())
            {
                dataHolder.SetValue("Characters", CharacterSources.ToArray());
            }
        }

        public void SetObjectData(ISerializationData dataHolder)
        {
            ScenarioName = dataHolder.GetValue<string>("ScenarioName");
            var charArray = dataHolder.GetValue<CharacterSourceDTO[]>("Characters");
            if (charArray != null)
            {
                CharacterSources = new List<CharacterSourceDTO>(charArray);
            }
        }
        #endregion

    }
}
