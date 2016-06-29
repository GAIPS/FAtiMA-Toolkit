namespace RealTimeEmotionRecognition
{
    public class Classifier 
    {
        public IAffectRecognitionAsset Asset { get; set; }
        public float Weight { get; set; }

        public override int GetHashCode()
        {
            return Asset.Name.GetHashCode();
        }
    }
}
