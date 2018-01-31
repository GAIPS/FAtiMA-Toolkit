/*
 * Copyright 2017 Open University of the Netherlands (OUNL)
 *
 * Authors: Kiavash Bahreini, Wim van der Vegt.
 * Organization: Open University of the Netherlands (OUNL).
 * Project: The RAGE project
 * Project URL: http://rageproject.eu.
 * Task: T2.3 of the RAGE project; Development of assets for emotion detection. 
 * 
 * For any questions please contact: 
 *
 * Kiavash Bahreini via kiavash.bahreini [AT] ou [DOT] nl
 * and/or
 * Wim van der Vegt via wim.vandervegt [AT] ou [DOT] nl
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * This project has received funding from the European Union’s Horizon
 * 2020 research and innovation programme under grant agreement No 644187.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace MultimodalEmotionDetectionWF
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;

    using Accord.Video;
    using Accord.Video.DirectShow;
    using Accord.Video.FFMPEG;

    using AssetManagerPackage;

    using AssetPackage;
    using Ionic.Zip;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using System.Globalization;

    public partial class Form1 : Form
    {
        #region Fields
        const String CKPlusPlath = @"C:\Virtual Machines\CK+";
        const String face3 = @".\Kiavash1.jpg";


        Boolean busy = false;
        Int32 counter = 0;
        
        Boolean facedetected = false;
        List<RECT> Faces = new List<RECT>();
        List<POINT> Landmarks = new List<POINT>();
        private EmotionDetectionAsset EDA { get; set; }

        /// <summary>
        /// The dlib supported PixelFormats for load_bmp() in image_loader.h.
        /// </summary>
        PixelFormat[] supported =
            {
            PixelFormat.Format1bppIndexed,
            PixelFormat.Format4bppIndexed,
            PixelFormat.Format8bppIndexed,
            PixelFormat.Format24bppRgb
        };
        Stopwatch sw = new Stopwatch();
        private VideoCaptureDevice videoSource;

        #endregion Fields

#if WIN64

        const String wrapper = @".\dlibwrapper_x64.dll";

#else

        const String wrapper = @".\dlibwrapper.dll";

#endif

        #region Constructors

        public Form1(EmotionDetectionAsset eda)
        {
            this.EDA = eda;
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        ///// <summary>
        ///// Detect faces.
        ///// </summary>
        /////
        ///// <param name="img">          The image. </param>
        ///// <param name="length">       The length. </param>
        ///// <param name="faces">        [out] The faces. </param>
        ///// <param name="facecount">    [out] The facecount. </param>
        //[DllImport(wrapper,
        //    CharSet = CharSet.Auto,
        //    EntryPoint = "DetectFaces",
        //    SetLastError = true,
        //    CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DetectFaces(
        //    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1)] byte[] img,
        //    Int32 length,
        //    out IntPtr faces,
        //    out int facecount);

        ///// <summary>
        ///// Detect landmarks.
        ///// </summary>
        /////
        ///// <param name="face">         The face. </param>
        ///// <param name="landmarks">    [out] The landmarks. </param>
        ///// <param name="markcount">    [out] The markcount. </param>
        //[DllImport(wrapper,
        //    CharSet = CharSet.Auto,
        //    EntryPoint = "DetectLandmarks",
        //    SetLastError = true,
        //    CallingConvention = CallingConvention.Cdecl)]
        //public static extern void DetectLandmarks(RECT face, out IntPtr landmarks, out int markcount);

        ///// <summary>
        ///// Init database.
        ///// </summary>
        /////
        ///// <param name="lpFileName">   Filename of the file. </param>
        /////
        ///// <returns>
        ///// An int.
        ///// </returns>
        //[DllImport(wrapper,
        //    CharSet = CharSet.Auto,
        //    EntryPoint = "InitDatabase",
        //    SetLastError = true,
        //    CallingConvention = CallingConvention.Cdecl)]
        //public static extern int InitDatabase([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        ///// <summary>
        ///// Init detector.
        ///// </summary>
        //[DllImport(wrapper,
        //    CharSet = CharSet.Auto,
        //    EntryPoint = "InitDetector",
        //    SetLastError = true,
        //    CallingConvention = CallingConvention.Cdecl)]
        //public static extern void InitDetector();

        /// <summary>
        /// Draw faces and landmarks.
        /// </summary>
        public void DrawFacesAndLandmarks(Image img)
        {
            using (Graphics g = Graphics.FromImage(img))
            {
                Int32 cnt = 0;

                foreach (KeyValuePair<EmotionDetectionAsset.RECT, List<EmotionDetectionAsset.POINT>> kvp in EDA.Faces)
                {
                    //! Faces
                    //
                    g.DrawRectangle(
                        new Pen(new SolidBrush(Color.Black)),
                        new Rectangle(kvp.Key.Left, kvp.Key.Top, kvp.Key.Right - kvp.Key.Left, kvp.Key.Bottom - kvp.Key.Top));

                    //! Face Id
                    // .
                    g.DrawString(
                        String.Format("Face_{0}", cnt++),
                        new Font(FontFamily.GenericSansSerif, 10.0f, FontStyle.Bold),
                        new SolidBrush(Color.Yellow),
                        new PointF(kvp.Key.Left, kvp.Key.Top));

                    //! Angles.
                    //
                    foreach (EmotionDetectionAsset.POINT vector in this.EDA.Vectors)
                    {
                        g.DrawLine(new Pen(
                            new SolidBrush(Color.Blue)),
                            kvp.Value[vector.X].X, kvp.Value[vector.X].Y,
                            kvp.Value[vector.Y].X, kvp.Value[vector.Y].Y);
                    }

                    //! Landmarks.
                    //
                    foreach (EmotionDetectionAsset.POINT p in kvp.Value)
                    {
                        g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(p.X - 2, p.Y - 2, 5, 5));
                        //g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(p.X - 1, p.Y - 1, 3, 3));
                    }
                }
            }

            this.Refresh();
        }

        /// <summary>
        /// Arguments to String.
        /// </summary>
        ///
        /// <param name="args"> A variable-length parameters list containing
        ///                     arguments. </param>
        ///
        /// <returns>
        /// A String.
        /// </returns>
        private static String ArgsToString(params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                return String.Empty;
            }
            else
            {
                return String.Join(";", args.Select(p => p.ToString()).ToArray());
            }
        }

        /// <summary>
        /// Event handler. Called by button1 for click events.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            //Debug.Print(String.Empty);

            //using (Timing t = new Timing("Initialize"))
            //{
            //    InitDetector();

            //    InitDatabase(database);

            //    InitDatabase(database);

            //    label1.Text = t.ElapsedMsg;
            //}

            //! Has to be a BMP as dlib only supports reading BMP from a stream.
            //
        }

        /// <summary>
        /// Event handler. Called by button2 for click events.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        [Obsolete]
        private void button2_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = (Bitmap)Bitmap.FromFile(face3);

            //pictureBox1.Image = bmp;

            //DetectFacesInBitmap((Bitmap)pictureBox1.Image);
            MessageBox.Show("Obsoleted");
        }

        /// <summary>
        /// Event handler. Called by button3 for click events.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        [Obsolete]
        private void button3_Click(object sender, EventArgs e)
        {
            //DetectLandmarksInFaces();

            MessageBox.Show("Obsoleted");
        }

        /// <summary>
        /// Event handler. Called by button4 for click events.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        private void button4_Click(object sender, EventArgs e)
        {
            InitChart();

            //! VideoCaptureDevice fails with a Additional information: Object is currently in use elsewhere. GDI+ error.
            //
            //! Enumerate video devices
            //
            //FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //// Create video source
            //videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

            ////videoSource.DesiredFrameRate = 5;

            //// Set NewFrame event handler
            //videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);

            //// start the video source
            //videoSource.Start();

            // change the capture time frame
            //this.webCamCapture1.TimeToCapture_milliseconds = 20;

            // start the video capture. let the control handle the
            // frame numbers.
            
            this.webCamCapture1.Start(0);
            
        }

        /// <summary>
        /// Event handler.Called by button5 for click events.
        /// </summary>
        ///
        /// <param name = "sender" > Source of the event. </param>
        /// <param name="e">        Event information. </param>
        private void button5_Click(object sender, EventArgs e)
        {
            InitChart();

            //! Save both spike detection and averaging.
            //
            Int32 avg = (this.EDA.Settings as EmotionDetectionAssetSettings).Average;
            Boolean spike = (this.EDA.Settings as EmotionDetectionAssetSettings).SuppressSpikes;

            //(eda.Settings as EmotionDetectionAssetSettings).GrayScale = true;

            //! Temporary switch of both spike detection and averaging.
            //
            (this.EDA.Settings as EmotionDetectionAssetSettings).Average = 1;
            (this.EDA.Settings as EmotionDetectionAssetSettings).SuppressSpikes = false;

            pictureBox1.Image = Image.FromFile(face3);

            // First pixel of Kiavash1.bmp
            // R 229
            // G 198
            // B 154
            ProcessImageIntoEmotions(pictureBox1.Image, true);

            //! Restore of both spike detection and averaging.
            //
            (this.EDA.Settings as EmotionDetectionAssetSettings).Average = avg;
            (this.EDA.Settings as EmotionDetectionAssetSettings).SuppressSpikes = spike;
        }

        /// <summary>
        /// Event handler.Called by button6 for click events.
        /// </summary>
        ///
        /// <param name = "sender" > Source of the event. </param>
        /// <param name="e">        Event information. </param>
        private void button6_Click(object sender, EventArgs e)
        {
            InitChart();

            //! Create instance of video reader
            VideoFileReader reader = new VideoFileReader();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //! Open video file
                reader.Open(openFileDialog1.FileName);

                String xlsx = Path.ChangeExtension(openFileDialog1.FileName, ".xlsx");

                if (File.Exists(xlsx))
                {
                    File.Delete(xlsx);
                }

                Image img;

                Int32 rofs;
                Int32 cofs;

                using (ExcelPackage package = new ExcelPackage(new FileInfo(xlsx)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Emotions");

                    rofs = 1;
                    cofs = 1;

                    worksheet.Cells[rofs, cofs].Value = "Time";
                    worksheet.Cells[rofs, cofs].Style.Font.Bold = true;
                    worksheet.Column(cofs).Style.Numberformat.Format = "[h]:mm:ss";
                    cofs++;

                    worksheet.Cells[rofs, cofs].Value = "Msec";
                    worksheet.Cells[rofs, cofs].Style.Font.Bold = true;
                    worksheet.Column(cofs).Style.Numberformat.Format = "000";
                    worksheet.Column(cofs).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    cofs++;

                    worksheet.Cells[rofs, cofs].Value = "Stamp";
                    worksheet.Cells[rofs, cofs].Style.Font.Bold = true;
                    worksheet.Column(cofs).Style.Numberformat.Format = "000.000";
                    worksheet.Column(cofs).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    cofs++;

                    foreach (String emotion in this.EDA.Emotions)
                    {
                        worksheet.Cells[rofs, cofs].Value = emotion;
                        worksheet.Cells[rofs, cofs].Style.Font.Bold = true;
                        worksheet.Column(cofs).Style.Numberformat.Format = "0.00";

                        cofs++;
                    }

                    rofs++;
                    cofs = 1;

                    for (Int32 i = 0; i < reader.FrameCount; i++)
                    {
                        //if (i == 5000)
                        //{
                        //    //reader.Close();
                        //    break;
                        //}

                        img = reader.ReadVideoFrame();

                        TimeSpan ts = TimeSpan.FromMilliseconds((1000.0 * i) / reader.FrameRate);

                        label1.Text = ts.ToString("G");

                        //if (ts.Seconds == 52)
                        //{
                        //    Debugger.Break();
                        //}
                        //
                        //if (i == reader.FrameCount - 5)
                        //{
                        //    Debugger.Break();
                        //}

                        if (img != null)
                        {
                            pictureBox1.Image = (Bitmap)img;

                            //pictureBox1.Refresh();

                            ProcessImageIntoEmotions(pictureBox1.Image, i % 10 == 0);

                            worksheet.Cells[rofs, cofs].Value = ts.TotalSeconds / (24 * 60 * 60);
                            // String.Format("{0}:{1}:{2}", ts.Hours, ts.Minutes, ts.Seconds);
                            cofs++;

                            worksheet.Cells[rofs, cofs].Value = ts.Milliseconds;
                            cofs++;

                            worksheet.Cells[rofs, cofs].Value = ts.TotalSeconds/* + ts.Milliseconds / 1000.0*/;
                            cofs++;

                            foreach (String emotion in this.EDA.Emotions)
                            {
                                //for (Int32 j = 0; j < eda.Faces.Count; j++)
                                if (this.EDA.Faces.Count > 0)
                                {
                                    //String emo = String.Format("{0:0.00}", eda[0, emotion]);

                                    worksheet.Cells[rofs, cofs].Value = this.EDA[0, emotion];
                                    if (this.EDA[0, emotion] > 0.8)
                                    {
                                        worksheet.Cells[rofs, cofs].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[rofs, cofs].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                                    }
                                    else if (this.EDA[0, emotion] > 0.4)
                                    {
                                        worksheet.Cells[rofs, cofs].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[rofs, cofs].Style.Fill.BackgroundColor.SetColor(Color.LightCoral);
                                    }

                                    cofs++;
                                }
                            }

                            rofs++;
                            cofs = 1;

                            //! Dispose the frame when it is no longer required, but not the last frame.
                            //
                            if (i < reader.FrameCount - 1)
                            {
                                img.Dispose();
                            }
                        }
                    }

                    package.Save();
                }

                reader.Close();
            }

            foreach (String emotion in this.EDA.Emotions)
            {
                Messages.unsubscribe(subscriptions[emotion]);
            }
        }

        ///// <summary>
        ///// Detect faces in bitmap.
        ///// </summary>
        /////
        ///// <param name="bmp">  The bitmap. </param>
        //[Obsolete]
        //private void DetectFacesInBitmap(Bitmap bmp)
        //{
        //    Debug.Print("{0}", pictureBox1.Image.PixelFormat);
        //
        //    //Bitmap clone = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        //    //using (Graphics gr = Graphics.FromImage(clone))
        //    //{
        //    //    gr.DrawImage(pictureBox1.Image, new Rectangle(0, 0, clone.Width, clone.Height));
        //    //}
        //
        //    //! This is code to make sure dlib accepts the image.
        //    //! Dlib's image_load.h / load_bmp() only supports 1,4,8 or 24 bits images (so no 16 or 32 bit ones).
        //    //
        //    Byte[] raw = (!supported.Contains(bmp.PixelFormat)) ?
        //        bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format24bppRgb).ToByteArray() :
        //        bmp.ToByteArray();
        //
        //    //http://stackoverflow.com/questions/209258/dllimport-int-how-to-do-this-if-it-can-be-done
        //    //http://www.codeproject.com/Questions/187293/Interoperation-C-and-native-Win-C-code-arrays-of
        //
        //    using (Timing t = new Timing("DetectFaces"))
        //    {
        //        int facecount = 0;
        //        IntPtr faces = IntPtr.Zero;
        //
        //        DetectFaces(raw, raw.Length, out faces, out facecount);
        //
        //        //Debug.Print("Detected: {0} face(s)", facecount);
        //        //Debug.Print("SizeOf(RECT)={0}", Marshal.SizeOf(new RECT()));
        //
        //        Faces.Clear();
        //
        //        //RECT[] ManagedRectArray = new RECT[facecount];
        //
        //        if (facecount != 0)
        //        {
        //            IntPtr[] pIntPtrArray = new IntPtr[facecount];
        //
        //            Marshal.Copy(faces, pIntPtrArray, 0, facecount);
        //
        //            for (Int32 i = 0; i < facecount; i++)
        //            {
        //                Faces.Add((RECT)Marshal.PtrToStructure(pIntPtrArray[i], typeof(RECT)));
        //
        //                Marshal.FreeCoTaskMem(pIntPtrArray[i]);
        //
        //                Debug.Print("Face {0} @ {1}", i, Faces[i].ToString());
        //            }
        //
        //            Marshal.FreeCoTaskMem(faces);
        //        }
        //
        //        label1.Text = t.ElapsedMsg;
        //    }
        //
        //    using (Graphics g1 = Graphics.FromImage(pictureBox1.Image))
        //    {
        //        foreach (RECT r in Faces)
        //        {
        //            g1.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top));
        //        }
        //    }
        //
        //    this.Refresh();
        //}

        ///// <summary>
        ///// (This method is obsolete) detect landmarks in faces.
        ///// </summary>
        //[Obsolete]
        //private void DetectLandmarksInFaces()
        //{
        //    foreach (RECT r in Faces)
        //    {
        //        using (Timing t = new Timing("DetectFaces"))
        //        {
        //            int markcount = 0;
        //            IntPtr landmarks = IntPtr.Zero;
        //
        //            eda.DetectLandmarks(r, out landmarks, out markcount);

        //            //Debug.Print("Detected: {0} Landmark Part(s)", markcount);
        //            //Debug.Print("SizeOf(POINT)={0}", Marshal.SizeOf(new POINT()));

        //            Landmarks.Clear();

        //            //POINT[] ManagedPointArray = new POINT[markcount];

        //            IntPtr[] pIntPtrArray = new IntPtr[markcount];

        //            Marshal.Copy(landmarks, pIntPtrArray, 0, markcount);

        //            for (Int32 i = 0; i < markcount; i++)
        //            {
        //                Landmarks.Add((POINT)Marshal.PtrToStructure(pIntPtrArray[i], typeof(POINT)));

        //                Marshal.FreeCoTaskMem(pIntPtrArray[i]);

        //                // Debug.Print("Landmark Point {0} @ {1}", i, Landmarks[i].ToString());
        //            }

        //            Marshal.FreeCoTaskMem(landmarks);

        //            label1.Text = t.ElapsedMsg;
        //        }

        //        using (Graphics g1 = Graphics.FromImage(pictureBox1.Image))
        //        {
        //            foreach (Point p in Landmarks)
        //            {
        //                g1.FillRectangle(new SolidBrush(Color.Red), new Rectangle(p.X - 1, p.Y - 1, 3, 3));
        //            }
        //        }
        //    }

        //    this.Refresh();
        //}

        /// <summary>
        /// Handler, called when the emotion update event.
        /// </summary>
        ///
        /// <param name="message">      The message. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        private void EmotionUpdateEventHandler(String message, params object[] parameters)
        {
            //! Check parameters.
            //
            if (parameters.Length == 1 && parameters[0] is EmotionDetectionAsset.EmotionEventArgs)
            {
                //! Make the call thread-safe (ie. run on UI Thread).
                //
                Invoke(new MethodInvoker(delegate
                {
                    EmotionUpdateEventHandler(message, parameters[0] as EmotionDetectionAsset.EmotionEventArgs);
                }
                ));
            }
        }

        /// <summary>
        /// Handler, called when the emotion update event.
        /// </summary>
        ///
        /// <param name="message">  The message. </param>
        /// <param name="e">        Emotion event information. </param>
        private void EmotionUpdateEventHandler(String message, EmotionDetectionAsset.EmotionEventArgs e)
        {
            this.EDA.Log(Severity.Verbose, "EmotionUpdateEventHandler({0}: [{1}->{2:0.00}])", message.PadRight(16), e.face, e.value);

            if (e.face == 0)
            {
                //! The following code fails to update the chart (it does not notice the changes).
                //
                // chart2.Series[message].Points[0].YValues[0] = e.value;

                //! So replace the DataPoint.
                DataPoint dp = chart2.Series[message].Points[0];
                dp.YValues[0] = e.value;
                chart2.Series[message].Points[0] = dp;

                chart2.ChartAreas[0].AxisY.Maximum = Math.Max(chart2.ChartAreas[0].AxisY.Maximum, e.value);

                chart2.Update();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // stop the video capture
            if (this.webCamCapture1 != null)
            {
                this.webCamCapture1.Stop();
            }

            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.Stop();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set the image capture size
            this.webCamCapture1.CaptureHeight = this.pictureBox1.Height;
            this.webCamCapture1.CaptureWidth = this.pictureBox1.Width;

            AssetManager.Instance.Bridge = new Bridge();

            //eda.Initialize(@".\", database);

            //eda.ParseRules(File.ReadAllLines(furia));

            (this.EDA.Settings as EmotionDetectionAssetSettings).SuppressSpikes = true;

            foreach (String emotion in this.EDA.Emotions)
            {
                subscriptions.Add(emotion, Messages.subscribe(emotion, EmotionUpdateEventHandler));
            }

            //listView1.SetDoubleBuffered(true);
        }

        Dictionary<String, String> subscriptions = new Dictionary<String, String>();

        /// <summary>
        /// Init chart.
        /// </summary>
        private void InitChart()
        {
            chart1.Series.Clear();

            chart1.ChartAreas[0].AxisX.Maximum = 100;

            foreach (String emotion in this.EDA.Emotions)
            {
                chart1.Series.Add(emotion).ChartType = SeriesChartType.FastLine;
            }

            chart2.Series.Clear();

            foreach (String emotion in this.EDA.Emotions)
            {
                chart2.Series.Add(emotion).Points.AddY(0.5);
            }
        }

        /// <summary>
        /// Process the image into emotions.
        /// </summary>
        ///
        /// <param name="img">      The image. </param>
        /// <param name="redetect"> True to redetect. </param>
        private void ProcessImageIntoEmotions(Image img, Boolean redetect)
        {
            //! Skipping does not seem to work properly
            //
            if (redetect)
            {
                facedetected = this.EDA.ProcessImage(img);

                if (facedetected)
                {
                    counter++;

                    //Debug.WriteLine(String.Format("{0} Face(s detected.", eda.Faces.Count));

                    //! Process detect faces.
                    //
                    if (this.EDA.ProcessFaces())
                    {
                        //! Show Detection Results.
                        //
                        DrawFacesAndLandmarks(img);

                        UpdateOutputTable();
                    }

                    UpdateChart();
                }
            }
        }

        /// <summary>
        /// Updates the chart.
        /// </summary>
        private void UpdateChart()
        {
            //! Plot averaged data...
            //
            foreach (String emotion in this.EDA.Emotions)
            {
                chart1.Series[emotion].Points.AddY(this.EDA[0, emotion]);

                //! Moved to Message Event Handler.
                // 
                //DataPoint dp = chart2.Series[emotion].Points[0];
                //dp.YValues[0] = eda[0, emotion];
                //chart2.Series[emotion].Points[0] = dp;

                chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;

                if (chart1.Series[emotion].Points.Count > 100)
                {
                    chart1.Series[emotion].Points.RemoveAt(0);
                }
            }

            chart1.Update();
        }

        /// <summary>
        /// Updates the output table.
        /// </summary>
        private void UpdateOutputTable()
        {
            listView1.BeginUpdate();
            {
                if (this.EDA.ProcessLandmarks())
                {
                    foreach (KeyValuePair<EmotionDetectionAsset.RECT, List<EmotionDetectionAsset.POINT>> kvp in this.EDA.Faces)
                    {
                        //! Process detected landmarks into emotions.
                        //
                        while (listView1.Columns.Count - 1 > this.EDA.Faces.Count)
                        {
                            listView1.Columns.RemoveAt(1);
                        }

                        while (listView1.Columns.Count - 1 < this.EDA.Faces.Count)
                        {
                            listView1.Columns.Add(String.Empty).Width = 80;
                        }

                        for (Int32 i = 1; i < listView1.Columns.Count; i++)
                        {
                            listView1.Columns[i].Text = String.Format("Face_{0}", i - 1);
                        }
                    }

                    if (listView1.Items.Count == 0)
                    {
                        foreach (String emotion in this.EDA.Emotions)
                        {
                            listView1.Items.Add(emotion);//.SubItems.Clear();
                        }
                    }

                    //! Show Result for First Detected Face.
                    //
                    Int32 ndx = 0;

                    foreach (String emotion in this.EDA.Emotions)
                    {
                        for (Int32 i = 0; i < this.EDA.Faces.Count; i++)
                        {
                            String emo = String.Format("{0:0.00}", this.EDA[i, emotion]);

                            if (listView1.Items[ndx].SubItems.Count < i + 2)
                            {
                                listView1.Items[ndx].SubItems.Add(emo);
                            }
                            else
                            {
                                if (emo != listView1.Items[ndx].SubItems[i + 1].Text)
                                {
                                    listView1.Items[ndx].SubItems[i + 1].Text = emo;
                                }
                            }
                        }

                        ndx++;
                    }
                }
            }

            listView1.EndUpdate();
        }

        /// <summary>
        /// Event handler. Called by video for new frame events.
        /// </summary>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        New frame event information. </param>
        private void video_NewFrame(object sender, NewFrameEventArgs e)
        {
            if (!busy)
            {
                busy = true;

                this.pictureBox1.Image = (Bitmap)e.Frame.Clone();

                //ProcessImageIntoEmotions(this.pictureBox1.Image, true);
            }

            busy = false;
        }

        private void webCamCapture1_ImageCaptured(object source, WebCam_Capture.WebcamEventArgs e)
        {
            this.pictureBox1.Image = e.WebCamImage;

            if(this.timerProcessWebCamFacialEmotion.Enabled == false)
            {
                this.timerProcessWebCamFacialEmotion.Enabled = true;
                this.timerProcessWebCamFacialEmotion.Start();
            }

            //ProcessImageIntoEmotions(this.pictureBox1.Image, true);

            //this.webCamCapture1.Stop();
        }

        #endregion Methods

        #region Nested Types

        /// <summary>
        /// A point.
        /// 
        /// <see cref="http://www.pinvoke.net/default.aspx/Structures/RECT.html"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            #region Constructors

            /// <summary>
            /// Constructor.
            /// </summary>
            ///
            /// <param name="x">    The x coordinate. </param>
            /// <param name="y">    The y coordinate. </param>
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            /// <summary>
            /// Constructor.
            /// </summary>
            ///
            /// <param name="pt">   The point. </param>
            public POINT(System.Drawing.Point pt)
                : this(pt.X, pt.Y)
            {
                //
            }

            #endregion Constructors

            #region Methods

            /// <summary>
            /// Implicit cast that converts the given POINT to a Point.
            /// </summary>
            ///
            /// <param name="p">    The Point to process. </param>
            ///
            /// <returns>
            /// The result of the operation.
            /// </returns>
            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            /// <summary>
            /// Implicit cast that converts the given System.Drawing.Point to a POINT.
            /// </summary>
            ///
            /// <param name="p">    The Point to process. </param>
            ///
            /// <returns>
            /// The result of the operation.
            /// </returns>
            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }

            /// <summary>
            /// Returns the fully qualified type name of this instance.
            /// </summary>
            ///
            /// <returns>
            /// A <see cref="T:System.String" />
            ///  containing a fully qualified type name.
            /// </returns>
            public override string ToString()
            {
                return string.Format(CultureInfo.CurrentCulture, "{{X={0},Y={1}}}", X, Y);
            }

            #endregion Methods
        }

        /// <summary>
        /// A rectangle.
        /// 
        /// <see cref="http://www.pinvoke.net/default.aspx/Structures/RECT.html"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            #region Constructors

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(System.Drawing.Rectangle r)
                : this(r.Left, r.Top, r.Right, r.Bottom)
            {
            }

            #endregion Constructors

            #region Methods

            //public int X
            //{
            //    get { return Left; }
            //    set { Right -= (Left - value); Left = value; }
            //}
            //public int Y
            //{
            //    get { return Top; }
            //    set { Bottom -= (Top - value); Top = value; }
            //}
            ///// <summary>
            ///// Gets or sets the height.
            ///// </summary>
            /////
            ///// <value>
            ///// The height.
            ///// </value>
            //public int Height
            //{
            //    get { return Bottom - Top; }
            //    set { Bottom = value + Top; }
            //}
            ///// <summary>
            ///// Gets or sets the width.
            ///// </summary>
            /////
            ///// <value>
            ///// The width.
            ///// </value>
            //public long Width
            //{
            //    get { return Right - Left; }
            //    set { Right = value + Left; }
            //}
            ///// <summary>
            ///// Gets or sets the location.
            ///// </summary>
            /////
            ///// <value>
            ///// The location.
            ///// </value>
            //public System.Drawing.Point Location
            //{
            //    get { return new System.Drawing.Point(Left, Top); }
            //    set { X = value.X; Y = value.Y; }
            //}
            ///// <summary>
            ///// Gets or sets the size.
            ///// </summary>
            /////
            ///// <value>
            ///// The size.
            ///// </value>
            //public System.Drawing.Size Size
            //{
            //    get { return new System.Drawing.Size(Width, Height); }
            //    set { Width = value.Width; Height = value.Height; }
            //}
            /// <summary>
            /// Implicit cast that converts the given RECT to a Rectangle.
            /// </summary>
            ///
            /// <param name="r">    The rectangle to compare to this object. </param>
            ///
            /// <returns>
            /// The result of the operation.
            /// </returns>
            public static implicit operator System.Drawing.Rectangle(RECT r)
            {
                return new System.Drawing.Rectangle(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
            }

            /// <summary>
            /// Implicit cast that converts the given System.Drawing.Rectangle to a RECT.
            /// </summary>
            ///
            /// <param name="r">    The rectangle to compare to this object. </param>
            ///
            /// <returns>
            /// The result of the operation.
            /// </returns>
            public static implicit operator RECT(System.Drawing.Rectangle r)
            {
                return new RECT(r);
            }

            ///// <summary>
            ///// Equality operator.
            ///// </summary>
            /////
            ///// <param name="r1">   The first RECT. </param>
            ///// <param name="r2">   The second RECT. </param>
            /////
            ///// <returns>
            ///// The result of the operation.
            ///// </returns>
            //public static bool operator ==(RECT r1, RECT r2)
            //{
            //    return r1.Equals(r2);
            //}
            ///// <summary>
            ///// Inequality operator.
            ///// </summary>
            /////
            ///// <param name="r1">   The first RECT. </param>
            ///// <param name="r2">   The second RECT. </param>
            /////
            ///// <returns>
            ///// The result of the operation.
            ///// </returns>
            //public static bool operator !=(RECT r1, RECT r2)
            //{
            //    return !r1.Equals(r2);
            //}
            ///// <summary>
            ///// Indicates whether this instance and a specified object are equal.
            ///// </summary>
            /////
            ///// <param name="r">    The rectangle to compare to this object. </param>
            /////
            ///// <returns>
            ///// true if <paramref name="obj" />
            /////  and this instance are the same type and represent the same value; otherwise, false.
            ///// </returns>
            //public bool Equals(RECT r)
            //{
            //    return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            //}
            ///// <summary>
            ///// Indicates whether this instance and a specified object are equal.
            ///// </summary>
            /////
            ///// <param name="obj">  The object to compare with the current instance. </param>
            /////
            ///// <returns>
            ///// true if <paramref name="obj" />
            /////  and this instance are the same type and represent the same value; otherwise, false.
            ///// </returns>
            //public override bool Equals(object obj)
            //{
            //    if (obj is RECT)
            //        return Equals((RECT)obj);
            //    else if (obj is System.Drawing.Rectangle)
            //        return Equals(new RECT((System.Drawing.Rectangle)obj));
            //    return false;
            //}
            ///// <summary>
            ///// Returns the hash code for this instance.
            ///// </summary>
            /////
            ///// <returns>
            ///// A 32-bit signed integer that is the hash code for this instance.
            ///// </returns>
            //public override int GetHashCode()
            //{
            //    return ((System.Drawing.Rectangle)this).GetHashCode();
            //}
            /// <summary>
            /// Returns the fully qualified type name of this instance.
            /// </summary>
            ///
            /// <returns>
            /// A <see cref="T:System.String" />
            ///  containing a fully qualified type name.
            /// </returns>
            public override string ToString()
            {
                return string.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
            }

            #endregion Methods
        }

        public class Timing : IDisposable
        {
            #region Fields

            private bool disposedValue = false; // To detect redundant calls
            String msg = String.Empty;
            Stopwatch sw = new Stopwatch();

            #endregion Fields

            #region Constructors

            public Timing(String msg)
            {
                this.msg = msg;

                sw.Reset();
                sw.Start();
            }

            #endregion Constructors

            #region Properties

            public String ElapsedMsg
            {
                get
                {
                    return String.Format("{0}: {1} ms", msg, sw.ElapsedMilliseconds);
                }
            }

            #endregion Properties

            #region Methods

            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~Timing() {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            //   Dispose(false);
            // }
            // This code added to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
                // TODO: uncomment the following line if the finalizer is overridden above.
                // GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        sw.Stop();
                        Debug.Print(ElapsedMsg);
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                    // TODO: set large fields to null.

                    disposedValue = true;
                }
            }

            #endregion Methods
        }

        #endregion Nested Types

        private void button7_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@".\CKplus");

            using (ZipFile zip = new ZipFile(Path.Combine(CKPlusPlath, "Emotion_labels.zip")))
            {
                foreach (ZipEntry ze in zip)
                {
                    if (!ze.IsDirectory)
                    {
                        using (FileStream fs = new FileStream(Path.Combine(@".\CKplus", Path.GetFileName(ze.FileName)), FileMode.Create))
                        {
                            ze.Extract(fs);
                            fs.Close();
                        }
                    }
                }
            }

            using (ZipFile zip = new ZipFile(Path.Combine(CKPlusPlath, "extended-cohn-kanade-images.zip")))
            {
                foreach (ZipEntry ze in zip)
                {
                    if (!ze.IsDirectory)
                    {
                        String tag = Path.Combine(@".\CKplus",
    String.Format("{0}_emotion.txt", Path.GetFileNameWithoutExtension(ze.FileName)));

                        if (File.Exists(tag))
                        {
                            using (FileStream fs = new FileStream(
                                Path.Combine(@".\CKplus", Path.GetFileName(ze.FileName)),
                                FileMode.Create))
                            {
                                ze.Extract(fs);
                                fs.Close();
                            }
                        }
                    }
                }
            }

            using (ZipFile zip = new ZipFile(Path.Combine(CKPlusPlath, "extended-cohn-kanade-images.zip")))
            {
                foreach (ZipEntry ze in zip)
                {
                    if (!ze.IsDirectory)
                    {
                        String tag = Path.Combine(@".\CKplus",
                            String.Format("{0}_emotion.txt", Path.GetFileNameWithoutExtension(ze.FileName)));

                        if (!File.Exists(tag) &&
                            Path.GetFileNameWithoutExtension(ze.FileName).EndsWith("_00000001"))
                        {
                            using (FileStream fs = new FileStream(
                                Path.Combine(@".\CKplus", Path.GetFileName(ze.FileName)),
                                FileMode.Create))
                            {
                                ze.Extract(fs);
                                fs.Close();
                            }

                            File.WriteAllText(tag, "0");
                        }
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //! Save both spike detection and averaging.
            // 
            Int32 avg = (this.EDA.Settings as EmotionDetectionAssetSettings).Average;
            Boolean spike = (this.EDA.Settings as EmotionDetectionAssetSettings).SuppressSpikes;

            //Int32 mismatches = 0;

            Dictionary<String, Int32> cnts = new Dictionary<String, Int32>();

            cnts.Add("Neutral", 0);
            cnts.Add("Anger", 0);
            cnts.Add("Contempt", 0);       // Not present in Fuzzy rules.
            cnts.Add("Disgust", 0);
            cnts.Add("Fear", 0);
            cnts.Add("Happy", 0);
            cnts.Add("Sad", 0);            //Sadness in CK+ documentation
            cnts.Add("Surprise", 0);

            //! Temporary switch of both spike detection and averaging.
            // 
            (this.EDA.Settings as EmotionDetectionAssetSettings).Average = 1;
            (this.EDA.Settings as EmotionDetectionAssetSettings).SuppressSpikes = false;

            String xlsx = @".\CKplus\ckplus.xlsx";

            if (File.Exists(xlsx))
            {
                File.Delete(xlsx);
            }

            Int32 rofs;
            Int32 cofs;

            using (ExcelPackage package = new ExcelPackage(new FileInfo(xlsx)))
            {
                ExcelWorksheet ws1 = package.Workbook.Worksheets.Add("Emotions");
                ExcelWorksheet ws2 = package.Workbook.Worksheets.Add("CK+");

                rofs = 1;
                cofs = 1;

                ws1.Cells[rofs, cofs++].Value = "CkImage";
                ws1.Cells[rofs, cofs++].Value = "CkEmotion";
                ws1.Cells[rofs, cofs++].Value = "RageEmotions";
                ws1.Cells[rofs, cofs++].Value = "RageScores";

                for (Int32 i = 0; i < 8; i++)
                {
                    ws1.Cells[rofs, cofs + 1 + i].Value = CK2Emotion(i);
                }

                cofs = 1;
                rofs++;

                foreach (String fn in Directory.EnumerateFiles(@".\CKplus", "*.png"))
                {
                    cofs = 1;

                    InitChart();

                    pictureBox1.Image = Image.FromFile(fn);

                    ws1.Cells[rofs, cofs++].Value = Path.GetFileNameWithoutExtension(fn);

                    String txt = Path.Combine(@".\CKplus", String.Format("{0}_emotion.txt", Path.GetFileNameWithoutExtension(fn)));

                    String score = File.ReadAllText(txt).Trim();

                    Int32 emo = (Int32)Double.Parse(score, CultureInfo.InvariantCulture);

                    String emotion = String.Empty;

                    emotion = CK2Emotion(emo);

                    cnts[emotion] += 1;

                    ws1.Cells[rofs, cofs++].Value = emotion;

                    ProcessImageIntoEmotions(pictureBox1.Image, true);

                    Application.DoEvents();
                    Thread.Sleep(0);

                    //for (Int32 j = 0; j < eda.Faces.Count; j++)
                    if (this.EDA.Faces.Count > 0)
                    {
                        String ee = String.Empty;
                        Double vv = 0;

                        foreach (String em in this.EDA.Emotions)
                        {
                            if (this.EDA[0, em] > vv)
                            {
                                ee = em;
                                vv = this.EDA[0, em];
                            }
                        }

                        if (vv > .4)
                        {
                            ws1.Cells[rofs, cofs++].Value = ee;
                            ws1.Cells[rofs, cofs++].Value = vv;

                            ws1.Cells[rofs, cofs + emo + 1].Value = emotion.Equals(ee, StringComparison.OrdinalIgnoreCase) ? 1 : 0;

                            cofs -= 2;
                            if (!emotion.Equals(ee, StringComparison.OrdinalIgnoreCase))
                            {
                                ws1.Cells[rofs, cofs].Style.Font.Color.SetColor(Color.Red);
                            }

                            rofs++;
                        }
                        else
                        {
                            rofs++;
                        }
                    }
                    else
                    {
                        rofs++;
                    }
                }

                Int32 max = rofs;

                rofs = 1;
                cofs = 1;

                ws2.Cells[rofs, cofs++].Value = "Emotion";
                ws2.Cells[rofs, cofs++].Value = "Images";
                ws2.Cells[rofs, cofs++].Value = "Correct";
                ws2.Cells[rofs, cofs++].Value = "Percentage";

                foreach (KeyValuePair<String, Int32> kvp in cnts)
                {
                    rofs++;
                    cofs = 1;

                    ws2.Cells[rofs, cofs++].Value = kvp.Key;
                    ws2.Cells[rofs, cofs++].Value = kvp.Value;

                    String cells = "FGHIJKLM";

                    ws2.Cells[rofs, cofs++].Formula = String.Format("=SUM(Emotions!{0}2:Emotions!{0}{1})", cells[rofs - 2], max - 1);

                    String fmt = String.Format("={0}{1}/{2}{1}", "C", rofs, "B");
                    ws2.Cells[rofs, cofs++].Formula = fmt;
                    ws2.Cells[rofs, cofs - 1].Style.Numberformat.Format = "#%";
                }
                package.Save();
            }

            //! Restore of both spike detection and averaging.
            // 
            (this.EDA.Settings as EmotionDetectionAssetSettings).Average = avg;
            (this.EDA.Settings as EmotionDetectionAssetSettings).SuppressSpikes = spike;
        }

        private static string CK2Emotion(Int32 emo)
        {
            switch (emo)
            {
                case 0:
                    return "Neutral";
                case 1:
                    return "Anger";
                case 2:
                    return "Contempt";       // 
                case 3:
                    return "Disgust";
                case 4:
                    return "Fear";
                case 5:
                    return "Happy";
                case 6:
                    return "Sad";            //Sadness in CK+ documentation
                case 7:
                    return "Surprise";
            }

            return "error";
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void timerProcessWebCamFacialEmotion_Tick(object sender, EventArgs e)
        {
            ProcessImageIntoEmotions(this.pictureBox1.Image, true);
        }
    }
}