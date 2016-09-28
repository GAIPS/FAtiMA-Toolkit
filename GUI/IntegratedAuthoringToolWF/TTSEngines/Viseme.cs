namespace IntegratedAuthoringToolWF.TTSEngines
{
	//http://talkingmoose.ca/3d-sapi-viseme-ids-planning-for-lip-sync-mouth-shapes-in-3d
	//http://www.annosoft.com/docs/Visemes12.html
	//http://www.annosoft.com/docs/Visemes17.html
	/// <summary>
	/// SAPI standard visemes
	/// </summary>
	public enum Viseme : sbyte
	{
		Unknown = -1,
		Silence=0,

		AxAhUh = 1,
		Aa=2,
		Ao=3,
		EyEhAe=4,
		Er=5,
		YIyIhIx=6,
		WUwU = 7,
		Ow=8,
		Aw=9,
		Oy=10,
		Ay=11,
		H=12,
		R=13,
		L=14,
		SZTs=15,
		ShChJhZh=16,
		ThDh=17,
		FV=18,
		DTDxN=19,
		KGNg=20,
		PBM=21
	}
}