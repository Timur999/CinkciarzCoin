using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinkciarzCoin.Model
{
    class ValidatorTextBox
    {
        public bool NotNullValidateTextBox(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                return true;
            }

            if (!char.IsControl(e.KeyChar) && (sender as TextBox).Text.Length >= 4)
            {
                return true;
            }

            return false;
        }


        public bool IsDoubleValidateTextBox(object sender, KeyPressEventArgs e)
        {
            List<string> splitedText = ((sender as TextBox).Text.Split('.')).ToList();

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                return true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                return true;
            }

            if (splitedText[0].Length >= 4 && !char.IsControl(e.KeyChar))
            {
                return true;
            }

            if (splitedText.Count == 2)
            {
                if (splitedText[1].Length >= 4 && !char.IsControl(e.KeyChar))
                {
                    return true;
                }
            }

            // deny '.' as a first char 
            if (e.KeyChar == '.' && (sender as TextBox).Text == "")
            {
                return true;
            }

            return false;
        }
    }
}
