using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinkciarzCoin.Model
{
    class ExportData
    {

        public async Task<bool> ExportDataToCsvFile(List<Coin> ListOfRecordedCinkciarzCoins)
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
    }
}
