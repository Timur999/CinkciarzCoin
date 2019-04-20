using CinkciarzCoin.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            isGenerate = true;
            while (true)
            {
                ratesValue = CurrencyGenerator.GenerateCurrencyRate();
                ewhGenerationOperation.Set();

                // Control.Invoke executes delegate by using the Thread that created the Control. (Invoke(delegate))
                // MethodInvoker is a simple delagate that return void and does not get any parameters
                this.chartRateCoin.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    chartRateCoin.Series["Purchase rate"].Points.AddY(ratesValue["Purchase"]);
                    chartRateCoin.Series["Sales rate"].Points.AddY(ratesValue["Sales"]);

                    AddLabelToPointOnChart(ratesValue["Purchase"], ratesValue["Sales"]);
                });

                if (CurrencyGenerator.iNumberOfGenerationPerSec == 0)
                    Thread.Sleep(1000);
                else
                    Thread.Sleep(1000 / CurrencyGenerator.iNumberOfGenerationPerSec);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;

            ThreadGenerateCoinRates = new Thread(GenerateRate);
            ThreadGenerateCoinRates.IsBackground = true;
            try
            {
                ThreadGenerateCoinRates.Start();
            }catch (Exception ex)
            {
                MessageBox.Show("Thread caught Exception " + ex.Message,
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            isGenerate = false;

            try
            {
                ThreadGenerateCoinRates.Abort();
            }
            catch (ThreadAbortException ex)
            {
                MessageBox.Show("Thread caught ThreadAbortException " + ex.Message,
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
                    CurrencyGenerator.dMaxSpread = float.Parse((sender as TextBox).Text);
                    this.labelMaxSpreadDockedInChartValue.Text = (sender as TextBox).Text;
                    break;
                case "textboxmeanExchangeRate":
                    CurrencyGenerator.dMeanExchangeRate = float.Parse((sender as TextBox).Text);
                    break;
                case "textBoxnumberOfGenerationPerSec":
                    CurrencyGenerator.iNumberOfGenerationPerSec = int.Parse((sender as TextBox).Text);
                    this.labelNumGenPerSecDockedInChartValue.Text = (sender as TextBox).Text;
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

            if (splitedText[0].Length >= 4 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (splitedText.Count == 2)
            {
                if (splitedText[1].Length >= 4 && !char.IsControl(e.KeyChar))
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) )
            {
                e.Handled = true;
            }

            if(!char.IsControl(e.KeyChar) &&(sender as TextBox).Text.Length  >= 4)
            {
                e.Handled = true;
            }
        }

        private void buttonRecording_Click(object sender, EventArgs e)
        {
            if (isGenerate && !isRecording)
            {
                ThreadRecordCoinRates = new Thread(RecordCoin);
                ThreadRecordCoinRates.IsBackground = true;
                try
                {
                    ThreadRecordCoinRates.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thread caught Exception " + ex.Message,
                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                isRecording = true;
                this.buttonRecord.Text = "Stop record";
            }
            else if(isRecording)
            {
                try
                {
                    ThreadRecordCoinRates.Abort();
                }
                catch (ThreadAbortException ex)
                {
                    MessageBox.Show("Thread caught ThreadAbortException " + ex.Message,
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                isRecording = false;
                this.buttonRecord.Text = "Record";

                if (ListOfRecordedCinkciarzCoins.Count > 0)
                {
                    this.buttonSave.Visible = true;
                }
            }
        }

        private void RecordCoin()
        {
            while (ThreadRecordCoinRates.ThreadState != ThreadState.Aborted)
            {
                ewhGenerationOperation.WaitOne();
                Coin CinkciarzCoin = new Coin(ratesValue["Purchase"], ratesValue["Sales"]);

                ListOfRecordedCinkciarzCoins.Add(CinkciarzCoin);
            }
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (ListOfRecordedCinkciarzCoins.Count > 0)
            {
                bool isSuccess = await SaveDataToFile();
                if(isSuccess)
                    this.buttonSave.Visible = false;
            }
        }

        private async Task<bool> SaveDataToFile()
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
                            return true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.GetType().Name + " The write operation could not " +
                                               "be performed because the specified " +
                                                 "part of the file is locked.",
                                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return false;
                    }
                }
                return false;
            }
        }

        private void AddLabelToPointOnChart(double purchaseRateValueY, double salesRateValueY)
        {
            if (chartRateCoin.Series["Sales rate"].Points.Count >= 2)
            {
                chartRateCoin.Series["Sales rate"].Points.ElementAt(chartRateCoin.Series["Sales rate"].Points.Count - 2).Label = "";
                chartRateCoin.Series["Purchase rate"].Points.ElementAt(chartRateCoin.Series["Purchase rate"].Points.Count - 2).Label = "";
            }
            chartRateCoin.Series["Sales rate"].Points.LastOrDefault().Label = salesRateValueY.ToString("F4", CultureInfo.InvariantCulture);
            chartRateCoin.Series["Purchase rate"].Points.LastOrDefault().Label = purchaseRateValueY.ToString("F4", CultureInfo.InvariantCulture);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (ListOfRecordedCinkciarzCoins.Count > 0)
            {
                DialogResult userAnswer = MessageBox.Show("Your data will be lost. Do you want to save them?", "Note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (userAnswer == DialogResult.Yes)
                {
                    SaveDataToFile();
                    try
                    {
                        this.Dispose();
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.GetType().Name + " Caught a problem while closing program " + ex.Message,
                       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (userAnswer == DialogResult.No)
                {
                    try
                    {
                        this.Dispose();
                    }catch(Exception ex)
                    {
                        MessageBox.Show(ex.GetType().Name + " Caught a problem while closing program " + ex.Message,
                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Close();
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

    }
}


//old code: record coin-rates base on event.

//if (CurrencyGenerator.IsGenerate && !isRecording)
//{
//    this.chartRateCoin.Paint += new System.Windows.Forms.PaintEventHandler(chartRateCoin_AxisViewChanged);
//    this.buttonRecord.Text = "Stop record";
//    isRecording = true;
//}
//else
//{
//    this.chartRateCoin.Paint -= new System.Windows.Forms.PaintEventHandler(chartRateCoin_AxisViewChanged);
//    this.buttonRecord.Text = "Record";
//    isRecording = false;
//}
//private void chartRateCoin_AxisViewChanged(object sender, System.Windows.Forms.PaintEventArgs e)
//{
//    double PurchasePrice = chartRateCoin.Series["Purchase rate"].Points.LastOrDefault().YValues.LastOrDefault();
//    double SalePrice = chartRateCoin.Series["Sales rate"].Points.LastOrDefault().YValues.LastOrDefault();
//    Coin CinkciarzCoin = new Coin(PurchasePrice, SalePrice);

//    ListOfRecordedCinkciarzCoins.Add(CinkciarzCoin);
//}