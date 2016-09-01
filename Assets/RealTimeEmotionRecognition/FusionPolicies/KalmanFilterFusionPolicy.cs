using System.Collections.Generic;
using System.Linq;

namespace RealTimeEmotionRecognition.FusionPolicies
{
    public class KalmanFilterFusionPolicy : IFusionPolicy
    {
        public string PolicyName { get { return "Khalman"; } }

        public IEnumerable<Classifier> Classifiers { get; private set; }

        public Dictionary<string, KalmanFilter.KalmanFilter> KalmanFilters { get; private set; }

        public KalmanFilterFusionPolicy(IEnumerable<Classifier> classifiers)
        {
            this.Classifiers = classifiers;
            this.KalmanFilters = new Dictionary<string, KalmanFilter.KalmanFilter>();

            foreach(var classifier in classifiers)
            {
                var affectiveVariables = classifier.Asset.GetRecognizedAffectiveVariables();
                foreach (var av in affectiveVariables)
                {
                    if(!this.KalmanFilters.ContainsKey(av))
                    {
                        this.KalmanFilters[av] = new KalmanFilter.KalmanFilter(av);
                    }

                    this.KalmanFilters[av].AddClassifier(classifier);
                }

            }
        }

        public AffectiveInformation Fuse(IEnumerable<AugmentedAffectiveInformation> affectiveInformation)
        {
            float score = 0.0f;
            float totalWeight = 0.0f;

            if (affectiveInformation.Count() == 0)
            {
                return null;
            }

            var result = new AffectiveInformation();
            result.Name = affectiveInformation.FirstOrDefault().Name;
            
            if(!this.KalmanFilters.ContainsKey(result.Name))
            {
                return null;
            }
            var kalmanFilter = this.KalmanFilters[result.Name];

            kalmanFilter.ProcessObservations(affectiveInformation);
            result.Score = (float) kalmanFilter.X;

            return result;
        }
    }
}
