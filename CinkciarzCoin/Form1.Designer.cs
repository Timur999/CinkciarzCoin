using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace CinkciarzCoin
{
    partial class Form1 
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;




        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.labelMaxSpread = new System.Windows.Forms.Label();
            this.textBoxMaxSpread = new System.Windows.Forms.TextBox();
            this.labelmeanExchangeRate = new System.Windows.Forms.Label();
            this.textboxmeanExchangeRate = new System.Windows.Forms.TextBox();
            this.labelnumberOfGenerationPerSec = new System.Windows.Forms.Label();
            this.textBoxnumberOfGenerationPerSec = new System.Windows.Forms.TextBox();
            this.chartRateCoin = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonRecord = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartRateCoin)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(22, 13);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(22, 42);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelMaxSpread
            // 
            this.labelMaxSpread.AutoSize = true;
            this.labelMaxSpread.Location = new System.Drawing.Point(19, 90);
            this.labelMaxSpread.Name = "labelMaxSpread";
            this.labelMaxSpread.Size = new System.Drawing.Size(82, 13);
            this.labelMaxSpread.TabIndex = 2;
            this.labelMaxSpread.Text = "Set max Spread";
            // 
            // textBoxMaxSpread
            // 
            this.textBoxMaxSpread.Location = new System.Drawing.Point(22, 106);
            this.textBoxMaxSpread.Name = "textBoxMaxSpread";
            this.textBoxMaxSpread.Size = new System.Drawing.Size(100, 20);
            this.textBoxMaxSpread.TabIndex = 3;
            this.textBoxMaxSpread.Text = maxSpread.ToString();
            this.textBoxMaxSpread.TextChanged += new System.EventHandler(this.setValueTextBox_TextChanged);
            this.textBoxMaxSpread.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateTextBox_KeyPress);
            // 
            // labelmeanExchangeRate
            // 
            this.labelmeanExchangeRate.AutoSize = true;
            this.labelmeanExchangeRate.Location = new System.Drawing.Point(22, 133);
            this.labelmeanExchangeRate.Name = "labelmeanExchangeRate";
            this.labelmeanExchangeRate.Size = new System.Drawing.Size(105, 13);
            this.labelmeanExchangeRate.TabIndex = 4;
            this.labelmeanExchangeRate.Text = "Mean exchange rate";
            // 
            // textboxmeanExchangeRate
            // 
            this.textboxmeanExchangeRate.Location = new System.Drawing.Point(22, 150);
            this.textboxmeanExchangeRate.Name = "textboxmeanExchangeRate";
            this.textboxmeanExchangeRate.Size = new System.Drawing.Size(100, 20);
            this.textboxmeanExchangeRate.TabIndex = 5;
            this.textboxmeanExchangeRate.Text = meanExchangeRate.ToString();
            this.textboxmeanExchangeRate.TextChanged += new System.EventHandler(this.setValueTextBox_TextChanged);
            this.textboxmeanExchangeRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.validateTextBox_KeyPress);
            // 
            // labelnumberOfGenerationPerSec
            // 
            this.labelnumberOfGenerationPerSec.AutoSize = true;
            this.labelnumberOfGenerationPerSec.Location = new System.Drawing.Point(19, 184);
            this.labelnumberOfGenerationPerSec.Name = "labelnumberOfGenerationPerSec";
            this.labelnumberOfGenerationPerSec.Size = new System.Drawing.Size(152, 13);
            this.labelnumberOfGenerationPerSec.TabIndex = 6;
            this.labelnumberOfGenerationPerSec.Text = "Number of generations per sec";
            // 
            // textBoxnumberOfGenerationPerSec
            // 
            this.textBoxnumberOfGenerationPerSec.Location = new System.Drawing.Point(22, 201);
            this.textBoxnumberOfGenerationPerSec.Name = "textBoxnumberOfGenerationPerSec";
            this.textBoxnumberOfGenerationPerSec.Size = new System.Drawing.Size(100, 20);
            this.textBoxnumberOfGenerationPerSec.TabIndex = 7;
            this.textBoxnumberOfGenerationPerSec.Text = numberOfGenerationPerSec.ToString();
            this.textBoxnumberOfGenerationPerSec.TextChanged += new System.EventHandler(this.setValueTextBox_TextChanged);
            this.textBoxnumberOfGenerationPerSec.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.notNullValidateTextBox_KeyPress);
            // 
            // chartRateCoin
            // 
            this.chartRateCoin.BackColor = System.Drawing.Color.Transparent;
            this.chartRateCoin.CausesValidation = false;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "CinkciarzCoinChartArea";
            this.chartRateCoin.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartRateCoin.Legends.Add(legend1);
            this.chartRateCoin.Location = new System.Drawing.Point(286, 13);
            this.chartRateCoin.Name = "chartRateCoin";
            series1.ChartArea = "CinkciarzCoinChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.LabelForeColor = System.Drawing.Color.DodgerBlue;
            series1.Legend = "Legend1";
            series1.MarkerBorderWidth = 5;
            series1.Name = "Purchase rate";
            series2.ChartArea = "CinkciarzCoinChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.LabelForeColor = System.Drawing.Color.DarkOrange;
            series2.Legend = "Legend1";
            series2.MarkerBorderWidth = 5;
            series2.Name = "Sales rate";
            this.chartRateCoin.Series.Add(series1);
            this.chartRateCoin.Series.Add(series2);
            this.chartRateCoin.Size = new System.Drawing.Size(461, 360);
            this.chartRateCoin.TabIndex = 8;
            this.chartRateCoin.Text = "CinkciarzCoinChart";
            // 
            // buttonRecord
            // 
            this.buttonRecord.Location = new System.Drawing.Point(131, 13);
            this.buttonRecord.Name = "buttonRecord";
            this.buttonRecord.Size = new System.Drawing.Size(75, 23);
            this.buttonRecord.TabIndex = 9;
            this.buttonRecord.Text = "Record";
            this.buttonRecord.UseVisualStyleBackColor = true;
            this.buttonRecord.Click += new System.EventHandler(this.buttonRecording_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(131, 42);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Visible = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonRecord);
            this.Controls.Add(this.chartRateCoin);
            this.Controls.Add(this.textBoxnumberOfGenerationPerSec);
            this.Controls.Add(this.labelnumberOfGenerationPerSec);
            this.Controls.Add(this.textboxmeanExchangeRate);
            this.Controls.Add(this.labelmeanExchangeRate);
            this.Controls.Add(this.textBoxMaxSpread);
            this.Controls.Add(this.labelMaxSpread);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.chartRateCoin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private float maxSpread = 0.4f;
        private float meanExchangeRate = 3.5f;
        private int numberOfGenerationPerSec = 1;
        private bool isGenerate = false;
        private bool isRecording = false;

        private Button buttonStart;
        private Button buttonStop;
        private Button buttonSave;
        private Button buttonRecord;

        private Label labelMaxSpread;
        private TextBox textBoxMaxSpread;
        private Label labelmeanExchangeRate;
        private TextBox textboxmeanExchangeRate;
        private Label labelnumberOfGenerationPerSec;
        private TextBox textBoxnumberOfGenerationPerSec;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRateCoin;

        private Thread ThreadGenerateCoinRates;

        private List<Coin> ListOfRecordedCinkciarzCoins = new List<Coin>();

    }
}

