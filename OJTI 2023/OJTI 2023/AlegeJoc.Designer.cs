
namespace OJTI_2023
{
    partial class AlegeJoc
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.scoreLabel2 = new System.Windows.Forms.Label();
            this.guessDataGridView = new System.Windows.Forms.DataGridView();
            this.guessButton = new System.Windows.Forms.Button();
            this.snakeButton = new System.Windows.Forms.Button();
            this.snakeDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.guessDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.snakeDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(51, 62);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(179, 32);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Bine ai venit,";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLabel.Location = new System.Drawing.Point(225, 62);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(93, 32);
            this.userLabel.TabIndex = 1;
            this.userLabel.Text = "label1";
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(114, 201);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(167, 25);
            this.scoreLabel.TabIndex = 2;
            this.scoreLabel.Text = "Top scor ghiceste";
            // 
            // scoreLabel2
            // 
            this.scoreLabel2.AutoSize = true;
            this.scoreLabel2.Location = new System.Drawing.Point(740, 201);
            this.scoreLabel2.Name = "scoreLabel2";
            this.scoreLabel2.Size = new System.Drawing.Size(242, 25);
            this.scoreLabel2.TabIndex = 3;
            this.scoreLabel2.Text = "Top scor Sarpele Educativ";
            // 
            // guessDataGridView
            // 
            this.guessDataGridView.AllowUserToAddRows = false;
            this.guessDataGridView.AllowUserToDeleteRows = false;
            this.guessDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.guessDataGridView.Location = new System.Drawing.Point(12, 264);
            this.guessDataGridView.Name = "guessDataGridView";
            this.guessDataGridView.ReadOnly = true;
            this.guessDataGridView.RowHeadersWidth = 72;
            this.guessDataGridView.RowTemplate.Height = 31;
            this.guessDataGridView.Size = new System.Drawing.Size(514, 234);
            this.guessDataGridView.TabIndex = 4;
            // 
            // guessButton
            // 
            this.guessButton.Location = new System.Drawing.Point(119, 550);
            this.guessButton.Name = "guessButton";
            this.guessButton.Size = new System.Drawing.Size(188, 60);
            this.guessButton.TabIndex = 5;
            this.guessButton.Text = "Ghiceste";
            this.guessButton.UseVisualStyleBackColor = true;
            this.guessButton.Click += new System.EventHandler(this.guessButton_Click);
            // 
            // snakeButton
            // 
            this.snakeButton.Location = new System.Drawing.Point(793, 550);
            this.snakeButton.Name = "snakeButton";
            this.snakeButton.Size = new System.Drawing.Size(189, 60);
            this.snakeButton.TabIndex = 6;
            this.snakeButton.Text = "Sarpe Educativ";
            this.snakeButton.UseVisualStyleBackColor = true;
            this.snakeButton.Click += new System.EventHandler(this.snakeButton_Click);
            // 
            // snakeDataGridView
            // 
            this.snakeDataGridView.AllowUserToAddRows = false;
            this.snakeDataGridView.AllowUserToDeleteRows = false;
            this.snakeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.snakeDataGridView.Location = new System.Drawing.Point(574, 264);
            this.snakeDataGridView.Name = "snakeDataGridView";
            this.snakeDataGridView.ReadOnly = true;
            this.snakeDataGridView.RowHeadersWidth = 72;
            this.snakeDataGridView.RowTemplate.Height = 31;
            this.snakeDataGridView.Size = new System.Drawing.Size(565, 234);
            this.snakeDataGridView.TabIndex = 7;
            // 
            // AlegeJoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 658);
            this.Controls.Add(this.snakeDataGridView);
            this.Controls.Add(this.snakeButton);
            this.Controls.Add(this.guessButton);
            this.Controls.Add(this.guessDataGridView);
            this.Controls.Add(this.scoreLabel2);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.titleLabel);
            this.Name = "AlegeJoc";
            this.Text = "AlegeJoc";
            ((System.ComponentModel.ISupportInitialize)(this.guessDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.snakeDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label scoreLabel2;
        private System.Windows.Forms.DataGridView guessDataGridView;
        private System.Windows.Forms.Button guessButton;
        private System.Windows.Forms.Button snakeButton;
        private System.Windows.Forms.DataGridView snakeDataGridView;
    }
}