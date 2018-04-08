using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.IO;
using Finisar.SQLite; // db

namespace Project_1_1
{
    public partial class Form1 : Form
    {
        string LoadFaces;
        Image<Gray, byte> TrainedFace = null, unknownFace = null;
        private VideoCapture _capture;
        private CascadeClassifier _cascadeClassifier;
        private CascadeClassifier trainCascadeClassifier;
        List<Image<Gray, byte>> trainingImagesHolder = new List<Image<Gray, byte>>();
        List<string> labelsHolder = new List<string>();
        int[] integerLablesHolder;
        int numberOfLables, CountTrainedFaces;
        MEigenRecognizer myRecognizer;
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;

        public Form1()
        {
            

            InitializeComponent();
            //intialize the eigen recognizer to be used later 
            myRecognizer = new MEigenRecognizer("E:/Project_1_1/bin/Debug/holdRecFilePath/thephotos.yml");

            //Now get the saved imagesin the TrainedFaces Folder
            try
            {
                //First get the lables of the images in the training set where the lables are the names of users
                //string getLabels = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
                // numberOfLables = Convert.ToInt16(Labels[0]);
                List<string> image = new List<string> ();
                CountTrainedFaces = image.Count;
                sqlite_conn = new SQLiteConnection("Data Source=E:/Project_1_1/Project.db ; Version =3;");

                sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                //create command
                sqlite_cmd.CommandText = "select image_path from studentsFaces ;";
                //datareader to read from db
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    image.Add(sqlite_datareader.GetString(0));
                }
                sqlite_cmd.ExecuteNonQuery();
                //close connections
                sqlite_conn.Close();
                sqlite_datareader.Close();

                string[] Labels = new string[image.Count];
                for (int i = 0; i < image.Count; i++)
                {
                    Labels[i] = image[i];
                }

                // sqlite_conn.Close();
                //Now Load the images from the TrainedFaces Folder to our List that the images to be used in the code later
                //And the labels to our List that holds the lables to be used later .
                for (int i = 0; i <= image.Count ; i++)
                {
                    LoadFaces = "face" + i + ".bmp";
                    trainingImagesHolder.Add(new Image<Gray, byte>(image[i]));
                    labelsHolder.Add(Labels[i]);
                }

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                MessageBox.Show("Nothing in binary database, please add at least a face"
                   + " (Simply train the prototype with the Add Face Button).", "Triained faces load",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Run();


        }

        private void Run()
        {
            try
            {
                _capture = new VideoCapture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Application.Idle += ProcessFrame;

        }
        private void ProcessFrame(object sender, EventArgs e)
        {

            _cascadeClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.2.0.2682/opencv/data/haarcascades/haarcascade_frontalface_default.xml");
            using (var imageFrame = _capture.QueryFrame().ToImage<Bgr, Byte>())
            {

                if (imageFrame != null)
                {
                    var grayframe = imageFrame.Convert<Gray, byte>();
                    var faces = _cascadeClassifier.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                    foreach (var face in faces)
                    {
                        imageFrame.Draw(face, new Bgr(Color.BurlyWood), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                    }
                }

                imgCamUser.Image = imageFrame;

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Test_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < labelsHolder.ToArray().Length; i++)
            {
                string[] arrayLablesHolder = labelsHolder.ToArray();
                //  integerLablesHolder[i] = Int32.Parse(arrayLablesHolder[i]);
                Console.WriteLine(arrayLablesHolder[i]);
            }
        }

        private void recognizeTheFace_Click(object sender, EventArgs e)
        {
            trainCascadeClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.2.0.2682/opencv/data/haarcascades/haarcascade_frontalface_default.xml");
            using (var currentFrame = _capture.QueryFrame().ToImage<Bgr, Byte>())
            {

                if (currentFrame != null)
                {
                    var grayframe = currentFrame.Convert<Gray, byte>();
                    var faces = trainCascadeClassifier.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                    foreach (var face in faces)
                    {
                        grayframe.Draw(face, new Gray(1.523), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                    }
                    unknownFace = grayframe;
                }
            }
            int lable = myRecognizer.RecognizeUser(unknownFace);
            Console.Write(lable);
            MessageBox.Show(""
                  + lable, "Detected Face",
                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void AddaFaceToTrainingSet_Click(object sender, EventArgs e)
        {
            try
            {
                trainCascadeClassifier = new CascadeClassifier("C:/Emgu/emgucv-windesktop 3.2.0.2682/opencv/data/haarcascades/haarcascade_frontalface_default.xml");
                using (var currentFrame = _capture.QueryFrame().ToImage<Bgr, Byte>())
                {

                    if (currentFrame != null)
                    {
                        var grayframe = currentFrame.Convert<Gray, byte>();
                        var faces = trainCascadeClassifier.DetectMultiScale(grayframe, 1.1, 10, Size.Empty); //the actual face detection happens here
                        foreach (var face in faces)
                        {
                            grayframe.Draw(face, new Gray(1.523), 3); //the detected face(s) is highlighted here using a box that is drawn around it/them

                        }
                        grayframe = grayframe.Resize(100, 100, Inter.Cubic);
                        TrainedFace = grayframe;
                    }

                }
                trainingImagesHolder.Add(TrainedFace);
                labelsHolder.Add(textBox1.Text);

                //Show face added in gray scale
                imageBox1.Image = TrainedFace;

                //Write the number of triained faces in a file text for further load
                //   File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImagesHolder.ToArray().Length.ToString() + "%");

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_conn.Open();
                MessageBox.Show("insert into studentsFaces values (" + int.Parse(STUid.Text) + ",'" + Application.StartupPath + "\\TrainedFaces" + LoadFaces + "')");
                sqlite_cmd.CommandText = "insert into studentsFaces values (" + int.Parse(STUid.Text) +",'"+ Application.StartupPath + "\\TrainedFaces" + LoadFaces +"')";

                sqlite_conn.Close();

                //Write the labels of triained faces in a file text for further load
               /* for (int i = 1; i < trainingImagesHolder.ToArray().Length + 1; i++)
                {
                    trainingImagesHolder.ToArray()[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
                    File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labelsHolder.ToArray()[i - 1] + "%");
                }*/

                MessageBox.Show(textBox1.Text + "´s face detected and added :)", "Training OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Enable the face detection first", "Training Fail",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //----------------------------------------------------------------------------------------------------------------
            //Now after we added the new images and lables to our training set we will train our eigen recognizer
            integerLablesHolder = new int[labelsHolder.ToArray().Length];
            for (int i = 0; i < labelsHolder.ToArray().Length; i++)
            {
                string[] arrayLablesHolder = labelsHolder.ToArray();
                // integerLablesHolder[i]= Convert.ToInt32(arrayLablesHolder[i]);
                Int32.TryParse(arrayLablesHolder[i], out integerLablesHolder[i]);

            }
            myRecognizer.TrainRecognizer(trainingImagesHolder.ToArray(), integerLablesHolder);
        }


    }
}