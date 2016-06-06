namespace simple_db
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tableView = new System.Windows.Forms.ListView();
            this.tableList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.runQuery = new System.Windows.Forms.Button();
            this.queryField = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableView
            // 
            this.tableView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableView.FullRowSelect = true;
            this.tableView.GridLines = true;
            this.tableView.Location = new System.Drawing.Point(12, 136);
            this.tableView.Name = "tableView";
            this.tableView.Size = new System.Drawing.Size(573, 202);
            this.tableView.TabIndex = 0;
            this.tableView.UseCompatibleStateImageBehavior = false;
            this.tableView.View = System.Windows.Forms.View.Details;
            // 
            // tableList
            // 
            this.tableList.FormattingEnabled = true;
            this.tableList.Location = new System.Drawing.Point(91, 12);
            this.tableList.Name = "tableList";
            this.tableList.Size = new System.Drawing.Size(121, 21);
            this.tableList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose Table";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.runQuery);
            this.groupBox1.Controls.Add(this.queryField);
            this.groupBox1.Location = new System.Drawing.Point(15, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 91);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Query to database";
            // 
            // runQuery
            // 
            this.runQuery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.runQuery.Location = new System.Drawing.Point(526, 31);
            this.runQuery.Name = "runQuery";
            this.runQuery.Size = new System.Drawing.Size(38, 37);
            this.runQuery.TabIndex = 1;
            this.runQuery.UseVisualStyleBackColor = true;
            // 
            // queryField
            // 
            this.queryField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.queryField.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.queryField.Location = new System.Drawing.Point(6, 19);
            this.queryField.Multiline = true;
            this.queryField.Name = "queryField";
            this.queryField.Size = new System.Drawing.Size(514, 66);
            this.queryField.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 350);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableList);
            this.Controls.Add(this.tableView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Databse";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView tableView;
        private System.Windows.Forms.ComboBox tableList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button runQuery;
        private System.Windows.Forms.TextBox queryField;
    }
}

