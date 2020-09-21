using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DirectX.Capture;
using System.Drawing.Imaging;

namespace Camera
{
    public partial class Form1 : Form
    {
        public DirectX.Capture.Filter Camera;
        public DirectX.Capture.Capture CaptureInfo;
        public DirectX.Capture.Filters CamContainer;
        Image capturaImagem;
        public Form1()
        {
            InitializeComponent();
        }
        public void AtualizaImagem(PictureBox frame)
        {
            try
            {
                capturaImagem = frame.Image;
                this.pncamera.Image = capturaImagem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            CamContainer = new DirectX.Capture.Filters();
            try
            {
                int no_of_cam = CamContainer.VideoInputDevices.Count;

                for (int i = 0; i < no_of_cam; i++)
                {
                    try
                    {
                        // obtém o dispositivo de entrada do vídeo
                        Camera = CamContainer.VideoInputDevices[i];

                        // inicializa a Captura usando o dispositivo
                        CaptureInfo = new DirectX.Capture.Capture(Camera, null);

                        // Define a janela de visualização do vídeo
                        CaptureInfo.PreviewWindow = this.pncamera;

                        // Capturando o tratamento de evento
                        CaptureInfo.FrameCaptureComplete += AtualizaImagem;

                        // Captura o frame do dispositivo
                        CaptureInfo.CaptureFrame();

                        // Se o dispositivo foi encontrado e inicializado então sai sem checar o resto
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                try {
                    CaptureInfo.CaptureFrame();
                }
                catch (Exception ex) {
                    MessageBox.Show(this,ex.Message);
                }
            }
            catch { }
        }
    }
}
