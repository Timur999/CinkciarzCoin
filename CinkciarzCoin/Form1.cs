using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CinkciarzCoin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GenerateRate()
        {
            Random rand = new Random();
            isGenerate = true;

            while (true)
            {
                double purchaseRate = rand.NextDouble() * maxSpread + (meanExchangeRate - maxSpread);
                double salesRate = rand.NextDouble() * ((purchaseRate + maxSpread) - meanExchangeRate) + meanExchangeRate;
                this.chartRateCoin.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    chartRateCoin.Series["Purchase rate"].Points.AddY(purchaseRate);
                    chartRateCoin.Series["Sales rate"].Points.AddY(salesRate);
                    AddValueToSeriesLabel(salesRate, purchaseRate);
                });
                if (numberOfGenerationPerSec == 0)
                    Thread.Sleep(1000);
                else
                    Thread.Sleep(1000 / numberOfGenerationPerSec);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;

            ThreadGenerateCoinRates = new Thread(GenerateRate);
            ThreadGenerateCoinRates.IsBackground = true;
            ThreadGenerateCoinRates.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            isGenerate = false;
            if (ListOfRecordedCinkciarzCoins.Count > 0)
            {
                this.buttonSave.Visible = true;
            }

            try
            {
                ThreadGenerateCoinRates.Abort();
            }
            catch (ThreadAbortException ex)
            {
                MessageBox.Show(ex.GetType().Name + " Thread caught ThreadAbortException " + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setValueTextBox_TextChanged(object sender, EventArgs e)
        {
            if((sender as TextBox).Text == "")
                 (sender as TextBox).Text  = "1";

            switch ((sender as TextBox).Name)
            {
                case "textBoxMaxSpread":
                    maxSpread = float.Parse((sender as TextBox).Text);
                    break;
                case "textboxmeanExchangeRate":
                    meanExchangeRate = float.Parse((sender as TextBox).Text);
                    break;
                case "textBoxnumberOfGenerationPerSec":
                    numberOfGenerationPerSec = int.Parse((sender as TextBox).Text);
                    break;
            }
        }
    

        private void validateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            List<string> splitedText = ((sender as TextBox).Text.Split('.')).ToList();

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (splitedText.Count == 2)
            {
                if (splitedText[1].Length >= 4)
                {
                    e.Handled = true;
                }
            }

            // deny '.' as a first char 
            if (e.KeyChar == '.' && (sender as TextBox).Text == "")
            {
                e.Handled = true;
            }
        }

        private void notNullValidateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonRecording_Click(object sender, EventArgs e)
        {
            if (isGenerate && !isRecording)
            {
                this.chartRateCoin.Paint += new System.Windows.Forms.PaintEventHandler(chartRateCoin_AxisViewChanged);
                this.buttonRecord.Text = "Stop record";
                isRecording = true;
            }
            else
            {
                this.chartRateCoin.Paint -= new System.Windows.Forms.PaintEventHandler(chartRateCoin_AxisViewChanged);
                this.buttonRecord.Text = "Record";
                isRecording = false;
            }

        }

        private void chartRateCoin_AxisViewChanged(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            double PurchasePrice = chartRateCoin.Series["Purchase rate"].Points.LastOrDefault().YValues.LastOrDefault();
            double SalePrice = chartRateCoin.Series["Sales rate"].Points.LastOrDefault().YValues.LastOrDefault();
            Coin CinkciarzCoin = new Coin(PurchasePrice, SalePrice);

            ListOfRecordedCinkciarzCoins.Add(CinkciarzCoin);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (ListOfRecordedCinkciarzCoins.Count > 0)
            {
                saveDataToFile();
                this.buttonSave.Visible = false;
            }
        }

        private async void saveDataToFile()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "*CSV|*csv", ValidateNames = true })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder csvcontent = new StringBuilder();
                    string csvpath = saveFileDialog.FileName;

                    csvcontent.AppendLine("Purchase Price, Sales Price, Date of rate");
                    foreach (Coin CoinDetail in ListOfRecordedCinkciarzCoins)
                    {
                        csvcontent.AppendLine(string.Format("{0},{1},{2}", CoinDetail.dtDateOfRate, CoinDetail.dPurchasePrice.ToString("0.0000"), CoinDetail.dSalesPrice.ToString("0.0000")));
                    }

                    using (StreamWriter sw = new StreamWriter(new FileStream(csvpath, FileMode.Create), Encoding.UTF8))
                    {
                        try
                        {
                            await sw.WriteLineAsync(csvcontent.ToString());
                            ListOfRecordedCinkciarzCoins.Clear();
                            MessageBox.Show("Your data has been successfully saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.GetType().Name + " The write operation could not " +
                                               "be performed because the specified " +
                                                 "part of the file is locked.",
                                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (ListOfRecordedCinkciarzCoins.Count > 0)
            {
                if (MessageBox.Show("Your data will be lost. Do you want to save them?", "Note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    saveDataToFile();
                    this.Close();
                }
                else if (MessageBox.Show("Your data will be lost. Do you want to save them?", "Note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.No)
                {
                    this.Close();
                }
            }
        }

        private void AddValueToSeriesLabel(double salesRateValueY, double purchaseRateValueY)
        {
            if (chartRateCoin.Series["Sales rate"].Points.Count >= 2)
            {
                chartRateCoin.Series["Sales rate"].Points.ElementAt(chartRateCoin.Series["Sales rate"].Points.Count - 2).Label = "";
                chartRateCoin.Series["Purchase rate"].Points.ElementAt(chartRateCoin.Series["Purchase rate"].Points.Count - 2).Label = "";
            }
            chartRateCoin.Series["Sales rate"].Points.LastOrDefault().Label = salesRateValueY.ToString("F4", CultureInfo.InvariantCulture);
            chartRateCoin.Series["Purchase rate"].Points.LastOrDefault().Label = purchaseRateValueY.ToString("F4", CultureInfo.InvariantCulture);
        }
    }
}
