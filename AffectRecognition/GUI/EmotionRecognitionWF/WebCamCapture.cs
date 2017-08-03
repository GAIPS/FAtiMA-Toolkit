/*
 * Copyright 2016 Open University of the Netherlands
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * This project has received funding from the European Union’s Horizon
 * 2020 research and innovation programme under grant agreement No 644187.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace WebCam_Capture
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Summary description for WebCam Capture Control.
    /// </summary>
    [System.Drawing.ToolboxBitmap(typeof(WebCamCapture), "CAMERA.ICO")]
    [Designer("Sytem.Windows.Forms.Design.ParentControlDesigner,System.Design", typeof(System.ComponentModel.Design.IDesigner))]
    public class WebCamCapture : UserControl
    {
        #region Fields

        public const int WM_CAP_CONNECT = 1034;
        public const int WM_CAP_DISCONNECT = 1035;
        public const int WM_CAP_COPY = 1054;
        public const int WM_CAP_DLG_VIDEOCOMPRESSION = WM_CAP_START + 46;
        public const int WM_CAP_DLG_VIDEODISPLAY = WM_CAP_START + 43;
        public const int WM_CAP_DLG_VIDEOFORMAT = WM_CAP_START + 41;
        public const int WM_CAP_DLG_VIDEOSOURCE = WM_CAP_START + 42;
        public const int WM_CAP_GET_FRAME = 1084;
        public const int WM_CAP_GET_VIDEOFORMAT = WM_CAP_START + 44;
        public const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
        public const int WM_CAP_SET_VIDEOFORMAT = WM_CAP_START + 45;
        public const int WM_CAP_START = WM_USER;
        public const int WM_USER = 1024;

        private bool stopped = true;
        private int capHwnd;
        private IContainer components;
        private ulong frameNumber = 0;
        private int height = 240;
        private System.Drawing.Image tempImg;
        private IDataObject tempObj;
        private Timer timer1;
        private int timeToCapture_milliseconds = 20;
        private int width = 320;

        /// <summary>
        /// global variables to make the video capture go faster.
        /// </summary>
        private WebcamEventArgs x = new WebcamEventArgs();

        #endregion Fields

        #region Constructors

        public WebCamCapture()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        /// <summary>
        /// Override the class's finalize method, so we can stop
        /// the video capture on exit
        /// </summary>
        ~WebCamCapture()
        {
            this.Stop();
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// event delegate.
        /// </summary>
        ///
        /// <param name="source">   Source for the. </param>
        /// <param name="e">        Webcam event information. </param>
        public delegate void WebCamEventHandler(object source, WebCam_Capture.WebcamEventArgs e);

        #endregion Delegates

        #region Events

        /// <summary>
        /// fired when a new image is captured.
        /// </summary>
        public event WebCamEventHandler ImageCaptured;

        #endregion Events

        #region Properties

        /// <summary>
        /// The height of the video capture image
        /// </summary>
        public int CaptureHeight
        {
            get
            { return height; }

            set
            { height = value; }
        }

        /// <summary>
        /// The width of the video capture image
        /// </summary>
        public int CaptureWidth
        {
            get
            { return width; }

            set
            { width = value; }
        }

        /// <summary>
        /// The sequence number to start at for the frame number. OPTIONAL
        /// </summary>
        public ulong FrameNumber
        {
            get
            { return frameNumber; }

            set
            { frameNumber = value; }
        }

        /// <summary>
        /// The time intervale between frame captures
        /// </summary>
        public int TimeToCapture_milliseconds
        {
            get
            { return timeToCapture_milliseconds; }

            set
            { timeToCapture_milliseconds = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Capability create capture window a.
        /// </summary>
        ///
        /// <param name="lpszWindowName">   Name of the window. </param>
        /// <param name="dwStyle">          The style. </param>
        /// <param name="X">                global variables to make the video capture go faster. </param>
        /// <param name="Y">                The Y coordinate. </param>
        /// <param name="nWidth">           The width. </param>
        /// <param name="nHeight">          The height. </param>
        /// <param name="hwndParent">       The parent. </param>
        /// <param name="nID">              The identifier. </param>
        ///
        /// <returns>
        /// An int.
        /// </returns>
        [DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindowA")]
        public static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, int hwndParent, int nID);

        /// <summary>
        /// Sends a message.
        /// </summary>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="Msg">      The message. </param>
        /// <param name="wParam">   The parameter. </param>
        /// <param name="lParam">   The parameter. </param>
        ///
        /// <returns>
        /// An int.
        /// </returns>
        [DllImport("user32", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        /// <summary>
        /// Starts the video capture
        /// </summary>
        /// <param name="FrameNumber">the frame number to start at. 
        /// Set to 0 to let the control allocate the frame number</param>
        public void Start(ulong FrameNum)
        {
            try
            {
                //! 1) For safety, call stop, just in case we are already running
                this.Stop();

                //! 2) Setup a capture window
                capHwnd = capCreateCaptureWindowA("WebCap", 0, 0, 0, width, height, this.Handle.ToInt32(), 0);

                //! 3) Connect to the capture device
                Application.DoEvents();
                SendMessage(capHwnd, WM_CAP_CONNECT, 0, 0);
                SendMessage(capHwnd, WM_CAP_SET_PREVIEW, 0, 0);

                //! 4) Set the frame number
                frameNumber = FrameNum;

                //! 5) Set the timer information
                this.timer1.Interval = timeToCapture_milliseconds;
                stopped = false;
                this.timer1.Start();
            }

            catch (Exception e)
            {
                MessageBox.Show("An error ocurred while starting the video capture. Check that your webcamera is connected properly and turned on.\r\n\n" + e.Message);
                this.Stop();
            }
        }

        /// <summary>
        /// Stops the video capture
        /// </summary>
        public void Stop()
        {
            try
            {
                //! 1) stop the timer
                stopped = true;
                this.timer1.Stop();

                //! 2) disconnect from the video source
                Application.DoEvents();
                SendMessage(capHwnd, WM_CAP_DISCONNECT, 0, 0);
            }

            catch (Exception)
            {
                // don't raise an error here.
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WebCamCapture
            // 
            this.Name = "WebCamCapture";
            this.Size = new System.Drawing.Size(342, 252);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Capture the next frame from the video feed
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // pause the timer
                this.timer1.Stop();

                // get the next frame;
                SendMessage(capHwnd, WM_CAP_GET_FRAME, 0, 0);

                // copy the frame to the clipboard
                SendMessage(capHwnd, WM_CAP_COPY, 0, 0);

                // paste the frame into the event args image
                if (ImageCaptured != null)
                {
                    // get from the clipboard
                    tempObj = Clipboard.GetDataObject();
                    tempImg = (System.Drawing.Bitmap)tempObj.GetData(System.Windows.Forms.DataFormats.Bitmap);

                    /*
                    * For some reason, the API is not resizing the video
                    * feed to the width and height provided when the video
                    * feed was started, so we must resize the image here
                    */
                    x.WebCamImage = tempImg.GetThumbnailImage(width, height, null, System.IntPtr.Zero);

                    // raise the event
                    this.ImageCaptured(this, x);
                }

                // restart the timer
                //Application.DoEvents();
                if (!stopped)
                {
                    this.timer1.Start();
                }
            }

            catch (Exception e1)
            {
                MessageBox.Show("An error ocurred while capturing the video image. The video capture will now be terminated.\r\n\n" + e1.Message);
                this.Stop(); // stop the process
            }
        }

        #endregion Methods
    }
}