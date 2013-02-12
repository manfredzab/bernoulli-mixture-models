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

namespace BernoulliMixtureModels
{
    public class DigitData
    {
        public int Label;

        public byte[] Image;
        public bool[] BinaryImage;

        public int Height;
        public int Width;
    }
}
