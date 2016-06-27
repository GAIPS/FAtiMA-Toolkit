using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace RealTimeEmotionRecognition.FusionPolicies.KalmanFilter
{
    public class KalmanFilter
    {
        public string Name { get; private set; }
        private Dictionary<Classifier, int> ClassifierIndexes { get; set;}
        private List<Classifier> Classifiers { get; set; }
        public double X { get; private set; }
        private double Q { get; set; }
        private double P { get; set; }
        private Matrix<double> H { get; set; }
        private Matrix<double> H_T { get; set; }
        private Matrix<double> Z { get; set; }
        private Matrix<double> R { get; set; }

        private bool Initialized { get; set; }

        public KalmanFilter(string name)
        {
            this.Name = name;
            this.Classifiers = new List<Classifier>();
            this.ClassifierIndexes = new Dictionary<Classifier, int>();
            this.Initialized = false;

            //prediction error initialized as maximum
            this.P = 1.0f;
            this.X = 0.0f;
            this.Q = 0.05f; //process noise covariance
        }

        public void AddClassifier(Classifier classifier)
        {
            this.Classifiers.Add(classifier);

            this.ClassifierIndexes[classifier] = this.Classifiers.Count-1;
        }

        private void Initialize()
        {
            float totalWeight = 0.0f;

            this.Initialized = true;

            this.H = Matrix<double>.Build.Dense(this.ClassifierIndexes.Count,1);
            this.R = Matrix<double>.Build.DenseDiagonal(this.ClassifierIndexes.Count, 0.0f);
            //observations are initialized as 0
            this.Z = Matrix<double>.Build.Dense(this.ClassifierIndexes.Count, 1, 0.0);

            int i = 0;
            foreach(var classifier in this.Classifiers)
            {
                totalWeight += classifier.Weight;
                this.H[i, 0] = classifier.Weight;
                this.R[i, i] = 0.1f;
            }

            this.H = this.H / totalWeight;
            this.H_T = this.H.Transpose();
        }

        public void ProcessObservations(IEnumerable<AugmentedAffectiveInformation> observations)
        {
            if(!this.Initialized)
            {
                this.Initialize();
            }

            float totalWeights = 0.0f;
            float y;

            //Kalman prediction
            //in our model A is the identity matrix, so no need to update X^
            //this.PredictedX = this.PredictedX;
            //P is calculated based only on the previous value and Q
            this.P += this.Q;

            //if no observation is received by the classifier, use 0 as the default value and higher value for R
            this.Z *= 0;
            this.R = Matrix<double>.Build.DenseDiagonal(this.Classifiers.Count, 1.0f);


            //get observations
            foreach(var observation in observations)
            {
                var index = this.ClassifierIndexes[observation.Classifier];

                this.Z[index, 0] = observation.Score;
                this.R[index, index] = 0.1f;
            }

            //Kalman update
            var S = this.H * this.P * this.H_T + this.R;
            var K = this.P * this.H_T * S.Inverse();
            var Y = this.Z - this.H * this.X;

            this.X = (this.X + K * Y)[0,0];

            var p_aux = (1 - K * this.H) * this.P;

            this.P = p_aux[0, 0];

        }
    }
}
