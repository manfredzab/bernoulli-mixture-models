// Author:   Manfredas Zabarauskas, 2012 
// E-mail:   manfredas@zabarauskas.com
// Website:  http://zabarauskas.com
// Tutorial: http://blog.zabarauskas.com/expectation-maximization-tutorial
// Note:     Most of the UI/util/file parsing code is a write-once-read-never hack; only the 
//           ExpectationMaximization.cs code should be used for reference.
//
//           Handwritten digits taken from MNIST database: http://yann.lecun.com/exdb/mnist/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace BernoulliMixtureModels
{
    public class ExpectationMaximization
    {
        private const int K = 4; // Digits 0 - 9
        private readonly int D;
        private readonly int N;

        private double[] _pi;
        private double[,] _mu;

        private double[,] _x;
        private double[,] _z;

        private Random random = new Random();

        public ExpectationMaximization(int D, List<DigitData> digitData)
        {
            // Initialize D (dimensions) and N (number of images to be clustered)
            this.D = D;
            this.N = digitData.Count;

            // Initialize pi_1, ..., pi_K (mixing coefficients) to uniform values
            _pi = new double[K];
            for (int k = 0; k < K; k++)
            {
                _pi[k] = 1.0 / (double)K;
            }

            // Initialize mu_1, ..., mu_K (cluster's pixel distributions)
            _mu = new double[K, D];
            for (int k = 0; k < K; k++)
            {
                double normalizationFactor = 0.0;
                for (int i = 0; i < D; i++)
                {
                    _mu[k, i] = (random.NextDouble() * 0.5) + 0.25;
                    
                    normalizationFactor += _mu[k, i];
                }

                for (int i = 0; i < D; i++)
                {
                    _mu[k, i] /= normalizationFactor;
                }
            }

            // Initialize x_1, ..., x_N (input images)
            _x = new double[N, D];
            for (int n = 0; n < N; n++)
            {
                for (int i = 0; i < D; i++)
                {
                    _x[n, i] = digitData[n].BinaryImage[i] ? 1.0 : 0.0;
                }
            }

            // Initialize z_1, ..., z_N (latent variable over cluster membership of x_1, ... x_N)
            _z = new double[N, K];
        }


        public void PerformEMStep(int repetitions = 1)
        {
            for (int i = 0; i < repetitions; i++)
            {
                ExpectationStep();
                MaximizationStep();
            }
        }


        private void ExpectationStep()
        {
            for (int n = 0; n < N; n++)
            {
                double normalizationFactor = 0.0;

                for (int k = 0; k < K; k++)
                {
                    _z[n, k] = ExpectationSubstep(n, k);

                    normalizationFactor += _z[n, k];
                }

                // Re-normalize z[n, k]
                for (int k = 0; k < K; k++)
                {
                    if (normalizationFactor > 0.0)
                    {
                        _z[n, k] /= normalizationFactor;
                    }
                    else
                    {
                        _z[n, k] = 1.0 / (float)K;
                    }
                }
            }
        }


        private void MaximizationStep()
        {
            // Update pi_1, ..., pi_k
            for (int k = 0; k < K; k++)
            {
                _pi[k] = Nm(k) / (double)N;
            }

            // Update mu_1, ..., mu_K
            for (int k = 0; k < K; k++)
            {
                double[] averageX_k = AverageX(k);

                for (int i = 0; i < D; i++)
                {
                    _mu[k, i] = averageX_k[i];
                }
            }
        }


        private double ExpectationSubstep(int n, int k)
        {
            double z_nk = _pi[k];

            for (int i = 0; i < D; i++)
            {
                z_nk *= Math.Pow(_mu[k, i], _x[n, i]) * Math.Pow(1.0 - _mu[k, i], 1.0 - _x[n, i]);
            }

            return z_nk;
        }


        private double[] AverageX(int m)
        {
            double[] result = new double[D];

            for (int i = 0; i < D; i++)
            {
                for (int n = 0; n < N; n++)
                {
                    result[i] += _z[n, m] * _x[n, i];
                }
            }

            double currentNm = Nm(m);
            for (int i = 0; i < D; i++)
            {
                result[i] /= currentNm;
            }

            return result;
        }


        private double Nm(int m)
        {
            double result = 0.0;

            for (int n = 0; n < N; n++)
            {
                result += _z[n, m];
            }

            return result;
        }


        // Returns the closest cluster to the binary image
        public int GetCluster(bool[] binaryImage)
        {
            double maxClusterSum = Double.MinValue;
            int maxCluster = -1;

            for (int k = 0; k < K; k++)
            {
                double currentClusterSum = 0.0;
                for (int i = 0; i < D; i++)
                {
                    currentClusterSum += binaryImage[i] ? _mu[k, i] : 1.0 - _mu[k, i];
                }

                if (currentClusterSum > maxClusterSum)
                {
                    maxClusterSum = currentClusterSum;
                    maxCluster = k;
                }
            }

            return maxCluster;
        }

        // Creates a list of bitmaps vizualizing pixel distributions at each cluster
        public List<Bitmap> VisualizeClusterPixelDistributions()
        {
            List<Bitmap> result = new List<Bitmap>();

            for (int k = 0; k < K; k++)
            {
                double[] mu_k = new double[D];
                for (int i = 0; i < D; i++)
                {
                    mu_k[i] = _mu[k, i];
                }

                Bitmap pixelDistributionImage = Utils.MonochromeDoubleArrayToBitmap(mu_k);
                result.Add(pixelDistributionImage);
            }

            return result;
        }
    }
}
