﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wasapiex;
using WavRWLib2;
using System.IO;
using System.ComponentModel;

namespace PlayPcmWin
{
    public partial class MainWindow : Window
    {
        private WasapiCS wasapi;

        string m_wavFilePath;
        WavData m_wavData;

        const int DEFAULT_OUTPUT_LATENCY_MS = 100;

        public MainWindow()
        {
            InitializeComponent();

            int hr = 0;
            wasapi = new WasapiCS();
            hr = wasapi.Init();
            textBoxLog.Text += string.Format("wasapi.Init() {0:X8}\r\n", hr);
            textBoxLatency.Text = string.Format("{0}", DEFAULT_OUTPUT_LATENCY_MS);

            Closed += new EventHandler(MainWindow_Closed);

            CreateDeviceList();
        }

        private void CreateDeviceList() {
            int hr;

            int selectedIndex = -1;
            if (0 < listBoxDevices.Items.Count) {
                selectedIndex = listBoxDevices.SelectedIndex;
            }

            listBoxDevices.Items.Clear();

            hr = wasapi.DoDeviceEnumeration();
            textBoxLog.Text += string.Format("wasapi.DoDeviceEnumeration() {0:X8}\r\n", hr);

            int nDevices = wasapi.GetDeviceCount();
            for (int i = 0; i < nDevices; ++i) {
                listBoxDevices.Items.Add(wasapi.GetDeviceName(i));
            }

            buttonDeselect.IsEnabled = false;
            buttonPlay.IsEnabled = false;
            buttonStop.IsEnabled = false;
            buttonRefer.IsEnabled = true;
            menuItemFileOpen.IsEnabled = true;
            textBoxLatency.IsEnabled = true;
            
            if (0 < nDevices) {
                if (0 <= selectedIndex && selectedIndex < listBoxDevices.Items.Count) {
                    listBoxDevices.SelectedIndex = selectedIndex;
                } else {
                    listBoxDevices.SelectedIndex = 0;
                }

                buttonDeviceSelect.IsEnabled = true;
                buttonInspectDevice.IsEnabled = true;
            } else {
                buttonDeviceSelect.IsEnabled = false;
                buttonInspectDevice.IsEnabled = false;
            }
        }

        void MainWindow_Closed(object sender, EventArgs e) {
            wasapi.Stop();
            wasapi.Unsetup();
            wasapi.Term();
            wasapi = null;

            Application.Current.Shutdown(0);
        }

        private void MenuItemFileExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void buttonRefer_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".wav";
            dlg.Filter = "WAVEファイル (.wav)|*.wav";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true) {
                m_wavFilePath = dlg.FileName;
                m_wavData = new WavData();

                bool readSuccess = false;
                using (BinaryReader br = new BinaryReader(File.Open(m_wavFilePath, FileMode.Open))) {
                    readSuccess = m_wavData.ReadRaw(br);
                }
                if (readSuccess) {
                    textBoxPlayFile.Text = m_wavFilePath;

                    buttonDeviceSelect.IsEnabled = true;
                    menuItemFileOpen.IsEnabled = false;

                } else {
                    string s = string.Format("読み込み失敗: {0}\r\n", m_wavFilePath);
                    textBoxLog.Text += s;
                    MessageBox.Show(s);
                }
            }
        }
        
        private static string AssemblyVersion {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        private void MenuItemHelpAbout_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show(
                string.Format("PlayPcmWin バージョン {0}",
                    AssemblyVersion));
        }

        private void MenuItemHelpWeb_Click(object sender, RoutedEventArgs e) {
            try {
                System.Diagnostics.Process.Start("http://code.google.com/p/bitspersampleconv2/wiki/WasapiExclusiveMode");
            } catch (System.ComponentModel.Win32Exception) {
            }
        }

        private void buttonDeviceSelect_Click(object sender, RoutedEventArgs e) {
            int latencyMillisec = Int32.Parse(textBoxLatency.Text);
            if (latencyMillisec <= 0) {
                latencyMillisec = DEFAULT_OUTPUT_LATENCY_MS;
                textBoxLatency.Text = string.Format("{0}", DEFAULT_OUTPUT_LATENCY_MS);
            }

            int hr = wasapi.ChooseDevice(listBoxDevices.SelectedIndex);
            textBoxLog.Text += string.Format("wasapi.ChooseDevice() {0:X8}\r\n", hr);
            if (hr < 0) {
                return;
            }

            hr = wasapi.Setup(m_wavData.SampleRate, m_wavData.BitsPerSample, latencyMillisec);
            textBoxLog.Text += string.Format("wasapi.Setup({0}, {1}, {2}) {3:X8}\r\n",
                m_wavData.SampleRate, m_wavData.BitsPerSample, latencyMillisec, hr);
            if (hr < 0) {
                wasapi.Unsetup();
                textBoxLog.Text += string.Format("wasapi.Unsetup()\r\n");
                CreateDeviceList();
                string s = string.Format("エラー: wasapi.Setup({0}, {1}, {2})失敗。{3:X8}\nこのプログラムのバグか、オーディオデバイスが{0}Hz {1}bit レイテンシー{2}msに対応していないのか、どちらかです。",
                    m_wavData.SampleRate, m_wavData.BitsPerSample, latencyMillisec, hr);
                MessageBox.Show(s);
                return;
            }

            System.Diagnostics.Debug.Assert(0 < m_wavFilePath.Length);
            wasapi.SetOutputData(m_wavData.SampleRawGet());
            textBoxLog.Text += string.Format("wasapi.SetOutputData({0})\r\n", m_wavData.SampleRawGet().Length);

            buttonDeviceSelect.IsEnabled = false;
            buttonDeselect.IsEnabled = true;
            buttonPlay.IsEnabled = true;
            buttonRefer.IsEnabled = false;
            buttonInspectDevice.IsEnabled = false;
            textBoxLatency.IsEnabled = false;
        }

        private void buttonDeviceDeselect_Click(object sender, RoutedEventArgs e) {
            wasapi.Stop();
            wasapi.Unsetup();
            CreateDeviceList();
        }

        BackgroundWorker bw;

        private void buttonPlay_Click(object sender, RoutedEventArgs e) {
            int hr = wasapi.Start();
            textBoxLog.Text += string.Format("wasapi.Start() {0:X8}\r\n", hr);
            if (hr < 0) {
                return;
            }

            wasapi.SetPosFrame(0);
            slider1.Value = 0;
            slider1.Maximum = wasapi.GetTotalFrameNum();
            buttonStop.IsEnabled = true;
            buttonPlay.IsEnabled = false;
            buttonDeselect.IsEnabled = false;

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private void ProgressChanged(object o, ProgressChangedEventArgs args) {
            if (null == wasapi) {
                return;
            }
            slider1.Value = wasapi.GetPosFrame();
            label1.Content = string.Format("{0}/{1}", slider1.Value, slider1.Maximum);
        }
        
        private void RunWorkerCompleted(object o, RunWorkerCompletedEventArgs args) {
            buttonPlay.IsEnabled = true;
            buttonStop.IsEnabled = false;
            buttonDeselect.IsEnabled = true;

            slider1.Value = 0;
            label1.Content = "0/0";

            textBoxLog.Text += string.Format("Play completed.\r\n");
        }

        private void DoWork(object o, DoWorkEventArgs args) {
            Console.WriteLine("DoWork started");

            while (!wasapi.Run(100)) {
                bw.ReportProgress(0);
                System.Threading.Thread.Sleep(1);
            }

            wasapi.Stop();

            Console.WriteLine("DoWork end");
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e) {
            buttonStop.IsEnabled = false;

            wasapi.Stop();
            textBoxLog.Text += string.Format("wasapi.Stop()\r\n");
        }

        private void slider1_MouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                Console.WriteLine("slider1_MouseMove {0}", slider1.Value);
                if (!buttonPlay.IsEnabled) {
                    wasapi.SetPosFrame((int)slider1.Value);
                }
            }
        }

        private void buttonInspectDevice_Click(object sender, RoutedEventArgs e) {
            string dn = wasapi.GetDeviceName(listBoxDevices.SelectedIndex);
            string s = wasapi.InspectDevice(listBoxDevices.SelectedIndex);
            textBoxLog.Text += string.Format("wasapi.InspectDevice()\r\n{0}\r\n{1}\r\n", dn, s);
        }
    }
}
