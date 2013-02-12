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
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace BernoulliMixtureModels
{
    public class Utils
    {
        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern int DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr hdcDst, int xDst, int yDst, int w, int h, IntPtr hdcSrc, int xSrc, int ySrc, int rop);
        private const int SRCCOPY = 0x00CC0020;

        public static void SplashBitmap(Bitmap bitmap, int x, int y)
        {
            IntPtr hbm = bitmap.GetHbitmap();
            IntPtr sdc = GetDC(IntPtr.Zero);
            IntPtr hdc = CreateCompatibleDC(sdc);

            SelectObject(hdc, hbm);
            BitBlt(sdc, x, y, bitmap.Width, bitmap.Height, hdc, 0, 0, SRCCOPY);

            DeleteDC(hdc);
            ReleaseDC(IntPtr.Zero, sdc);
            DeleteObject(hbm);
        }


        public static Bitmap MonochromeByteArrayToBitmap(byte[] inputRawData, int width, int height)
        {
            Bitmap outputBitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            BitmapData bmpData = outputBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, outputBitmap.PixelFormat);

            int inputByteCount = width * height;
            int outputByteCount = inputByteCount << 2;

            byte[] outputRawData = new byte[outputByteCount];

            for (int i = 0; i < inputByteCount; i++)
            {
                byte value = (byte)(255 - inputRawData[i]);

                outputRawData[4 * i] = value;
                outputRawData[4 * i + 1] = value;
                outputRawData[4 * i + 2] = value;
            }

            System.Runtime.InteropServices.Marshal.Copy(outputRawData, 0, bmpData.Scan0, outputByteCount);

            outputBitmap.UnlockBits(bmpData);

            return outputBitmap;
        }


        public static Bitmap BinaryBoolArrayToBitmap(bool[] inputRawData, int width, int height)
        {
            Bitmap outputBitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            BitmapData bmpData = outputBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, outputBitmap.PixelFormat);

            int inputByteCount = width * height;
            int outputByteCount = inputByteCount << 2;

            byte[] outputRawData = new byte[outputByteCount];

            for (int i = 0; i < inputByteCount; i++)
            {
                byte value = inputRawData[i] ? (byte)0 : (byte)255;

                outputRawData[4 * i] = value;
                outputRawData[4 * i + 1] = value;
                outputRawData[4 * i + 2] = value;
            }

            System.Runtime.InteropServices.Marshal.Copy(outputRawData, 0, bmpData.Scan0, outputByteCount);

            outputBitmap.UnlockBits(bmpData);

            return outputBitmap;
        }


        public static Bitmap MonochromeDoubleArrayToBitmap(double[] inputRawData)
        {
            int width = (int)Math.Sqrt(inputRawData.Length);
            int height = width;

            Bitmap outputBitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            BitmapData bmpData = outputBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, outputBitmap.PixelFormat);

            int inputByteCount = width * height;
            int outputByteCount = inputByteCount << 2;

            byte[] outputRawData = new byte[outputByteCount];

            for (int i = 0; i < inputByteCount; i++)
            {
                byte value = (byte)(255.0 * (1.0 - inputRawData[i]));

                outputRawData[4 * i] = value;
                outputRawData[4 * i + 1] = value;
                outputRawData[4 * i + 2] = value;
            }

            System.Runtime.InteropServices.Marshal.Copy(outputRawData, 0, bmpData.Scan0, outputByteCount);

            outputBitmap.UnlockBits(bmpData);

            return outputBitmap;
        }

        public static double[] BitmapToMonochromeDoubleArray(Bitmap inputBitmap)
        {
            double[] result = new double[inputBitmap.Height * inputBitmap.Width];

            BitmapData bmpData = inputBitmap.LockBits(new Rectangle(0, 0, inputBitmap.Width, inputBitmap.Height), ImageLockMode.WriteOnly, inputBitmap.PixelFormat);

            int inputByteCount = inputBitmap.Width * inputBitmap.Height << 2;
            byte[] rawBmpData = new byte[inputByteCount];
            
            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, rawBmpData, 0, inputByteCount);

            inputBitmap.UnlockBits(bmpData);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (double)(255 - rawBmpData[4 * i]) / 255.0;
            }

            return result;
        }

        public static bool[] BitmapToBinaryBoolArray(Bitmap inputBitmap)
        {
            bool[] result = new bool[inputBitmap.Height * inputBitmap.Width];

            BitmapData bmpData = inputBitmap.LockBits(new Rectangle(0, 0, inputBitmap.Width, inputBitmap.Height), ImageLockMode.WriteOnly, inputBitmap.PixelFormat);

            int inputByteCount = inputBitmap.Width * inputBitmap.Height << 2;
            byte[] rawBmpData = new byte[inputByteCount];

            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, rawBmpData, 0, inputByteCount);

            inputBitmap.UnlockBits(bmpData);

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (255 - rawBmpData[4 * i]) > 127;
            }

            return result;
        }
    }
}
