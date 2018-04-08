namespace Project_1_1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imgCamUser = new Emgu.CV.UI.ImageBox();
            this.AddaFacetoTrainingSet = new System.Windows.Forms.Button();
            this.recognizeTheFace = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.STUid = new System.Windows.Forms.TextBox();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgCamUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // imgCamUser
            // 
            this.imgCamUser.Location = new System.Drawing.Point(1, 0);
            this.imgCamUser.Name = "imgCamUser";
            this.imgCamUser.Size = new System.Drawing.Size(563, 309);
            this.imgCamUser.TabIndex = 2;
            this.imgCamUser.TabStop = false;
            // 
            // AddaFacetoTrainingSet
            // 
            this.AddaFacetoTrainingSet.Location = new System.Drawing.Point(1, 344);
            this.AddaFacetoTrainingSet.Name = "AddaFacetoTrainingSet";
            this.AddaFacetoTrainingSet.Size = new System.Drawing.Size(75, 23);
            this.AddaFacetoTrainingSet.TabIndex = 3;
            this.AddaFacetoTrainingSet.Text = "Add a Face";
            this.AddaFacetoTrainingSet.UseVisualStyleBackColor = true;
            this.AddaFacetoTrainingSet.Click += new System.EventHandler(this.AddaFaceToTrainingSet_Click);
            // 
            // recognizeTheFace
            // 
            this.recognizeTheFace.Location = new System.Drawing.Point(512, 384);
            this.recognizeTheFace.Name = "recognizeTheFace";
            this.recognizeTheFace.Size = new System.Drawing.Size(75, 23);
            this.recognizeTheFace.TabIndex = 4;
            this.recognizeTheFace.Text = "Recognize";
            this.recognizeTheFace.UseVisualStyleBackColor = true;
            this.recognizeTheFace.Click += new System.EventHandler(this.recognizeTheFace_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(622, 386);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(223, 20);
            this.textBox1.TabIndex = 5;
            // 
            // STUid
            // 
            this.STUid.Location = new System.Drawing.Point(152, 384);
            this.STUid.Name = "STUid";
            this.STUid.Size = new System.Drawing.Size(100, 20);
            this.STUid.TabIndex = 7;
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(633, 0);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(202, 309);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 459);
            this.Controls.Add(this.STUid);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.imageBox1);
            this.Controls.Add(this.recognizeTheFace);
            this.Controls.Add(this.AddaFacetoTrainingSet);
            this.Controls.Add(this.imgCamUser);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCamUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imgCamUser;
        private System.Windows.Forms.Button AddaFacetoTrainingSet;
        private System.Windows.Forms.Button recognizeTheFace;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox STUid;
        private Emgu.CV.UI.ImageBox imageBox1;
    }
}

