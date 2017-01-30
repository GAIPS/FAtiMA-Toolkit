using System;
using System.Collections.Generic;
using System.Linq;
using IntegratedAuthoringTool.DTOs;
using Utilities;
using WellFormedNames;
using System.Security.Cryptography;

namespace IntegratedAuthoringTool
{

    /// <summary>
    /// Represents a dialogue action
    /// </summary>
    public class DialogStateAction
    {
        
        public Guid Id { get; private set; }
        public Name CurrentState { get; private set; }
        public Name NextState { get; private set; }
        public Name[] Meanings { get; private set; }
        public Name[] Styles { get; private set; }
        public string Utterance { get; private set; }
        public string UtteranceId { get; set;}

        /// <summary>
        /// Creates a new instance of a dialogue action from the corresponding DTO
        /// </summary>
        public DialogStateAction(DialogueStateActionDTO dto)
        {
	        this.Id = dto.Id == Guid.Empty?Guid.NewGuid() : dto.Id;
	        this.CurrentState = Name.BuildName(dto.CurrentState);
	        this.Meanings = dto.Meaning.Select(s => (Name) s).ToArray();
	        this.Styles = dto.Style.Select(s => (Name) s).ToArray();
	        this.NextState = Name.BuildName(dto.NextState);
            this.Utterance = dto.Utterance;
            this.UtteranceId = dto.UtteranceId;
        }

       
        public Name BuildSpeakAction()
	    {
		    return BuildSpeakAction(CurrentState, NextState, Meanings, Styles);
	    }

	    public static Name PackageList(Name packageRoot, IList<Name> elements)
	    {
		    switch (elements.Count)
		    {
			    case 0:
				    return Name.NIL_SYMBOL;
			    case 1:
				    return elements[0];
		    }

		    return Name.BuildName(elements.Prepend(packageRoot));
	    }

		public static Name BuildSpeakAction(Name currentState, Name nextState, IList<Name> meanings, IList<Name> styles)
	    {
		    return Name.BuildName((Name)IATConsts.DIALOG_ACTION_KEY, currentState, nextState, 
                PackageList((Name)IATConsts.MEANINGS_PACKAGING_NAME,meanings), 
                PackageList((Name)IATConsts.STYLES_PACKAGING_NAME,styles));
	    }

        /// <summary>
        /// Creates a DTO from the dialogue action
        /// </summary>
		public DialogueStateActionDTO ToDTO()
        {
            return new DialogueStateActionDTO
            {
                Id = this.Id,
                CurrentState = this.CurrentState.ToString(),
                NextState = this.NextState.ToString(),
                Meaning = this.Meanings.Select(s => s.ToString()).ToArray(),
                Style = this.Styles.Select(s => s.ToString()).ToArray(),
                Utterance = this.Utterance,
                UtteranceId = this.UtteranceId
            };
        }
    }

	public static class DialogStateActionDTOExtention
	{
		public static Name GetMeaningName(this DialogueStateActionDTO dto)
		{
			return DialogStateAction.PackageList((Name)IATConsts.MEANINGS_PACKAGING_NAME, dto.Meaning.Select(d => (Name) d).ToArray());
		}

		public static Name GetStylesName(this DialogueStateActionDTO dto)
		{
			return DialogStateAction.PackageList((Name)IATConsts.STYLES_PACKAGING_NAME, dto.Style.Select(d => (Name)d).ToArray());
		}
	}
}
