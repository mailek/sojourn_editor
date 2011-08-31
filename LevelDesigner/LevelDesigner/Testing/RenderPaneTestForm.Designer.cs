namespace LevelDesigner
{
    partial class RenderPaneTestForm
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
            ServiceManager serviceManager2 = new ServiceManager();
            this.rdr_renderPane = new RenderPane();
            this.prp_properties = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // rdr_renderPane
            // 
            this.rdr_renderPane.Location = new System.Drawing.Point(2, 2);
            this.rdr_renderPane.Name = "rdr_renderPane";
            this.rdr_renderPane.Services = serviceManager2;
            this.rdr_renderPane.Size = new System.Drawing.Size(781, 547);
            this.rdr_renderPane.TabIndex = 18;
            this.rdr_renderPane.Text = "button2";
            // 
            // prp_properties
            // 
            this.prp_properties.Location = new System.Drawing.Point(789, 46);
            this.prp_properties.Name = "prp_properties";
            this.prp_properties.Size = new System.Drawing.Size(318, 339);
            this.prp_properties.TabIndex = 19;
            // 
            // RenderPaneTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 551);
            this.Controls.Add(this.prp_properties);
            this.Controls.Add(this.rdr_renderPane);
            this.Name = "RenderPaneTestForm";
            this.Text = "RenderPaneTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private RenderPane rdr_renderPane;
        private System.Windows.Forms.PropertyGrid prp_properties;
    }
}