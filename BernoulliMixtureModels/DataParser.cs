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
using System.IO;
using System.Drawing;

namespace BernoulliMixtureModels
{
    public class DataParser
    {
        private const int N = 60000;

        public static List<DigitData> ParseDigitData(string fileNameImages, string fileNameLabels)
        {
            List<DigitData> result = new List<DigitData>();

            byte[] rawImageData = File.ReadAllBytes(fileNameImages);
            List<byte[]> imageData = ParseImageBytes(rawImageData);

            byte[] rawLabelData = File.ReadAllBytes(fileNameLabels);
            List<int> labelData = ParseLabelBytes(rawLabelData);

            if (imageData.Count != labelData.Count)
            {
                throw new Exception("Label and image dataset sizes are not equal.");
            }

            List<bool[]> binarizedImageData = BinarizeImages(imageData);

            int imageWidth = ImageWidth(rawImageData);
            int imageHeight = ImageHeight(rawImageData);

            for (int i = 0; i < imageData.Count; i++)
            {
                if (labelData[i] < 4)
                {
                    result.Add(new DigitData()
                    {
                        Label = labelData[i],

                        Image = imageData[i],
                        BinaryImage = binarizedImageData[i],

                        Width = imageWidth,
                        Height = imageHeight
                    });
                }
            }

            return result;
        }


        private static List<bool[]> BinarizeImages(List<byte[]> monochromeImages)
        {
            List<bool[]> result = new List<bool[]>();

            foreach (byte[] monochromeImage in monochromeImages)
            {
                bool[] binaryImage = new bool[monochromeImage.Length];
                for (int i = 0; i < monochromeImage.Length; i++)
                {
                    binaryImage[i] = monochromeImage[i] > 127;
                }

                result.Add(binaryImage);
            }

            return result;
        }

        private static List<byte[]> ParseImageBytes(byte[] imageBytes)
        {
            const int IDX3_MAGIC_NUMBER = 2051;

            List<byte[]> result = new List<byte[]>();

            int magicNumber = SwapEndiannessInt32(BitConverter.ToInt32(imageBytes, 0));
            if (magicNumber != IDX3_MAGIC_NUMBER)
            {
                throw new ArgumentException("Wrong IDX file format (mismatch in magic number).");
            }

            int numberOfImages = SwapEndiannessInt32(BitConverter.ToInt32(imageBytes, 4));
            numberOfImages = N;

            int numberOfRows = ImageHeight(imageBytes);
            int numberOfColumns = ImageWidth(imageBytes);

            // Parse images
            int headerOffset = 16;
            int imageByteCount = numberOfRows * numberOfColumns;
            for (int i = 0; i < numberOfImages; i++)
            {
                int previousImageEnd = headerOffset + (i * imageByteCount);

                byte[] image = new byte[imageByteCount];
                Array.Copy(imageBytes, previousImageEnd, image, 0, imageByteCount);

                result.Add(image);
            }

            return result;
        }

        private static int ImageHeight(byte[] imageBytes)
        {
            return SwapEndiannessInt32(BitConverter.ToInt32(imageBytes, 8));
        }

        private static int ImageWidth(byte[] imageBytes)
        {
            return SwapEndiannessInt32(BitConverter.ToInt32(imageBytes, 12));
        }

        private static List<int> ParseLabelBytes(byte[] labelBytes)
        {
            const int IDX1_MAGIC_NUMBER = 2049;

            List<int> result = new List<int>();

            int magicNumber = SwapEndiannessInt32(BitConverter.ToInt32(labelBytes, 0));
            if (magicNumber != IDX1_MAGIC_NUMBER)
            {
                throw new ArgumentException("Wrong IDX file format (mismatch in magic number).");
            }

            int numberOfLabels = SwapEndiannessInt32(BitConverter.ToInt32(labelBytes, 4));
            numberOfLabels = N;

            // Parse labels
            int headerOffset = 8;
            for (int i = 0; i < numberOfLabels; i++)
            {
                result.Add(labelBytes[headerOffset + i]);
            }

            return result;
        }
        
        private static int SwapEndiannessInt32(int value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;
            var b3 = (value >> 16) & 0xff;
            var b4 = (value >> 24) & 0xff;

            return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
        }
    }
}
