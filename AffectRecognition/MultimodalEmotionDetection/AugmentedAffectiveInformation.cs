namespace MultimodalEmotionDetection
{
    public class AugmentedAffectiveInformation : AffectiveInformation
    {
        public Classifier Classifier { get; set; }

        public AugmentedAffectiveInformation(AffectiveInformation affectiveInfo, Classifier classifier)
        {
            this.Name = affectiveInfo.Name;
            this.Score = affectiveInfo.Score;
            this.Classifier = classifier;
        }
    }
}
