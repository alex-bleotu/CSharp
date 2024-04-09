
namespace OJTI_2022
{
    partial class Vizualizare
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.viewPage = new System.Windows.Forms.TabPage();
            this.resetButton = new System.Windows.Forms.Button();
            this.filtersComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.dateTimerPicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mapComboBox = new System.Windows.Forms.ComboBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.roadPage = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.viewPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.roadPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.viewPage);
            this.tabControl1.Controls.Add(this.roadPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1229, 683);
            this.tabControl1.TabIndex = 0;
            // 
            // viewPage
            // 
            this.viewPage.Controls.Add(this.resetButton);
            this.viewPage.Controls.Add(this.filtersComboBox);
            this.viewPage.Controls.Add(this.label3);
            this.viewPage.Controls.Add(this.pictureBox);
            this.viewPage.Controls.Add(this.dateTimerPicker);
            this.viewPage.Controls.Add(this.label2);
            this.viewPage.Controls.Add(this.label1);
            this.viewPage.Controls.Add(this.mapComboBox);
            this.viewPage.Controls.Add(this.userLabel);
            this.viewPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewPage.Location = new System.Drawing.Point(4, 41);
            this.viewPage.Name = "viewPage";
            this.viewPage.Padding = new System.Windows.Forms.Padding(3);
            this.viewPage.Size = new System.Drawing.Size(1221, 638);
            this.viewPage.TabIndex = 0;
            this.viewPage.Text = "Vezi Harta";
            this.viewPage.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.Location = new System.Drawing.Point(121, 535);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(244, 69);
            this.resetButton.TabIndex = 17;
            this.resetButton.Text = "Resetare filtru";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // filtersComboBox
            // 
            this.filtersComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filtersComboBox.FormattingEnabled = true;
            this.filtersComboBox.Items.AddRange(new object[] {
            "Niciun filtru",
            "Valoarea < 20",
            "20 <= Valoarea <= 40",
            "Valoarea > 40"});
            this.filtersComboBox.Location = new System.Drawing.Point(37, 445);
            this.filtersComboBox.Name = "filtersComboBox";
            this.filtersComboBox.Size = new System.Drawing.Size(460, 40);
            this.filtersComboBox.TabIndex = 16;
            this.filtersComboBox.SelectedIndexChanged += new System.EventHandler(this.filtersComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 390);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 32);
            this.label3.TabIndex = 15;
            this.label3.Text = "Filtru";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::OJTI_2022.Properties.Resources.default_harta;
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(543, 84);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(640, 480);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 14;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            // 
            // dateTimerPicker
            // 
            this.dateTimerPicker.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimerPicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimerPicker.Location = new System.Drawing.Point(37, 298);
            this.dateTimerPicker.Name = "dateTimerPicker";
            this.dateTimerPicker.Size = new System.Drawing.Size(460, 37);
            this.dateTimerPicker.TabIndex = 13;
            this.dateTimerPicker.ValueChanged += new System.EventHandler(this.dateTimerPicker_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 32);
            this.label2.TabIndex = 12;
            this.label2.Text = "Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "Harti";
            // 
            // mapComboBox
            // 
            this.mapComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapComboBox.FormattingEnabled = true;
            this.mapComboBox.Location = new System.Drawing.Point(37, 154);
            this.mapComboBox.Name = "mapComboBox";
            this.mapComboBox.Size = new System.Drawing.Size(460, 40);
            this.mapComboBox.TabIndex = 10;
            this.mapComboBox.Text = "Selecteaza o harta";
            this.mapComboBox.SelectedIndexChanged += new System.EventHandler(this.mapComboBox_SelectedIndexChanged);
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLabel.Location = new System.Drawing.Point(31, 23);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(0, 42);
            this.userLabel.TabIndex = 9;
            // 
            // roadPage
            // 
            this.roadPage.Controls.Add(this.pictureBox2);
            this.roadPage.Location = new System.Drawing.Point(4, 41);
            this.roadPage.Name = "roadPage";
            this.roadPage.Padding = new System.Windows.Forms.Padding(3);
            this.roadPage.Size = new System.Drawing.Size(1221, 638);
            this.roadPage.TabIndex = 1;
            this.roadPage.Text = "Traseu";
            this.roadPage.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = global::OJTI_2022.Properties.Resources.default_harta;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1215, 632);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            // 
            // Vizualizare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 683);
            this.Controls.Add(this.tabControl1);
            this.Name = "Vizualizare";
            this.Text = "Vizualizare";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Vizualizare_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.viewPage.ResumeLayout(false);
            this.viewPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.roadPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage viewPage;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.ComboBox filtersComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.DateTimePicker dateTimerPicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox mapComboBox;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.TabPage roadPage;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}