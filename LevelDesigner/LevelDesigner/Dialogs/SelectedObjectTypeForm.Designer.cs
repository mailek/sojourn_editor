namespace LevelDesigner
{
    partial class SelectedObjectTypeForm
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
            this.cmb_typeSelect = new System.Windows.Forms.ComboBox();
            this.grp_type = new System.Windows.Forms.GroupBox();
            this.btn_select = new System.Windows.Forms.Button();
            this.grp_type.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_typeSelect
            // 
            this.cmb_typeSelect.FormattingEnabled = true;
            this.cmb_typeSelect.Location = new System.Drawing.Point(6, 19);
            this.cmb_typeSelect.Name = "cmb_typeSelect";
            this.cmb_typeSelect.Size = new System.Drawing.Size(183, 21);
            this.cmb_typeSelect.TabIndex = 0;
            // 
            // grp_type
            // 
            this.grp_type.Controls.Add(this.cmb_typeSelect);
            this.grp_type.Location = new System.Drawing.Point(12, 12);
            this.grp_type.Name = "grp_type";
            this.grp_type.Size = new System.Drawing.Size(195, 48);
            this.grp_type.TabIndex = 1;
            this.grp_type.TabStop = false;
            this.grp_type.Text = "Select Object Type";
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(69, 66);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(75, 38);
            this.btn_select.TabIndex = 2;
            this.btn_select.Text = "Select";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // SelectedObjectTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 107);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.grp_type);
            this.Name = "SelectedObjectTypeForm";
            this.Text = "SelectedObjectTypeForm";
            this.grp_type.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_typeSelect;
        private System.Windows.Forms.GroupBox grp_type;
        private System.Windows.Forms.Button btn_select;
    }
}