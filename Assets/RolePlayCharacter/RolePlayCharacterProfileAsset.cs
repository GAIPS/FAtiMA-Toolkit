using System;
using AssetManagerPackage;
using AssetPackage;
using CommeillFaut;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using GAIPS.Rage;
using SocialImportance;

namespace RolePlayCharacter
{
	[Serializable]
	public class RolePlayCharacterProfileAsset : LoadableAsset<RolePlayCharacterProfileAsset>
	{
		private string _emotionalAppraisalAssetSource = null;
		private string _emotionalDecisionMakingAssetSource = null;
		private string _socialImportanceAssetSource = null;
		private string _commeillFautAssetSource = null;

		/// <summary>
		/// The name of the character
		/// </summary>
		public string CharacterName { get; set; }

		/// <summary>
		/// An identifier for the embodiment that is used by the character
		/// </summary>
		public string BodyName { get; set; }

		/// <summary>
		/// The source being used for the Emotional Appraisal Asset
		/// </summary>
		public string EmotionalAppraisalAssetSource
		{
			get { return ToAbsolutePath(_emotionalAppraisalAssetSource); }
			set { _emotionalAppraisalAssetSource = ToRelativePath(value); }
		}

		/// <summary>
		/// The source being used for the Emotional Decision Making Asset
		/// </summary>
		public string EmotionalDecisionMakingSource
		{
			get { return ToAbsolutePath(_emotionalDecisionMakingAssetSource); }
			set { _emotionalDecisionMakingAssetSource = ToRelativePath(value); }
		}

		/// <summary>
		/// The source being used for the Social Importance Asset
		/// </summary>
		public string SocialImportanceAssetSource
		{
			get { return ToAbsolutePath(_socialImportanceAssetSource); }
			set { _socialImportanceAssetSource = ToRelativePath(value); }
		}

		public string CommeillFautAssetSource
		{
			get { return ToAbsolutePath(_commeillFautAssetSource); }
			set { _commeillFautAssetSource = ToRelativePath(value); }
		}

		protected override string OnAssetLoaded()
		{
			return null;
		}

		protected override void OnAssetPathChanged(string oldpath)
		{
			_emotionalAppraisalAssetSource = ToRelativePath(AssetFilePath,
				ToAbsolutePath(oldpath, _emotionalAppraisalAssetSource));

			_emotionalDecisionMakingAssetSource = ToRelativePath(AssetFilePath,
				ToAbsolutePath(oldpath, _emotionalDecisionMakingAssetSource));

			_socialImportanceAssetSource = ToRelativePath(AssetFilePath,
				ToAbsolutePath(oldpath, _socialImportanceAssetSource));

			_commeillFautAssetSource = ToRelativePath(AssetFilePath,
				ToAbsolutePath(oldpath, _commeillFautAssetSource));
		}

		public RolePlayCharacterAsset BuildRPCFromProfile()
		{
			EmotionalAppraisalAsset ea = Loader(_emotionalAppraisalAssetSource, () => new EmotionalAppraisalAsset("Agent"));
			ea.SetPerspective(CharacterName);
			EmotionalDecisionMakingAsset edm = Loader(_emotionalDecisionMakingAssetSource,()=> new EmotionalDecisionMakingAsset());
			SocialImportanceAsset si = Loader(_socialImportanceAssetSource, () => new SocialImportanceAsset());

			CommeillFautAsset cfa = Loader(_commeillFautAssetSource, () => new CommeillFautAsset());

			return new RolePlayCharacterAsset(ea,edm,si,cfa);
		}

		private T Loader<T>(string path, Func<T> generateDefault) where T : LoadableAsset<T>
		{
			if (string.IsNullOrEmpty(path))
				return generateDefault();

			return LoadableAsset<T>.LoadFromFile(ToAbsolutePath(path));
		}
	}
}