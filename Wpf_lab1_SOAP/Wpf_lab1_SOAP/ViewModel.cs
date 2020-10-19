using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf_lab1_SOAP.CBR;

namespace Wpf_lab1_SOAP
{
    public class ViewModel : INotifyPropertyChanged
    {
        CBRService cbr;
        public ViewModel()
        {
            cbr = new CBRService();
            cbr.Cbr.GetCursOnDateCompleted += new GetCursOnDateCompletedEventHandler(_cbrClient_GetCursOnDateCompleted);
        }
        
        
        private string lastQueriedRate;
        public string LastQueriedRate
        {
            get => lastQueriedRate;

            set
            {
                if (lastQueriedRate == value) return;
                lastQueriedRate = value;
                NotifyPropertyChanged(nameof(LastQueriedRate));
            }
        }

        private ICommand buttonCLick;
        public ICommand ButtonCLick
        {
            get
            {
                return buttonCLick ??
                  (buttonCLick = new RelayCommand(obj =>
                  {
                      LastQueriedRate = ExtractCurrencyRate(cbr.Cbr.GetCursOnDate(DateTime.Now), "USD").ToString();
                  }));
            }
        }
        public void AsyncGetCurrencyRateOnDate(DateTime dateTime, string currencyCode)
        {
            cbr.Cbr.GetCursOnDateAsync(dateTime, currencyCode);
        }
        void _cbrClient_GetCursOnDateCompleted(object sender, GetCursOnDateCompletedEventArgs e)
        {
            LastQueriedRate = ExtractCurrencyRate(e.Result, (string)e.UserState).ToString();
            
        }
        /// <summary>
        /// Извлечение валютного курса
        /// </summary>
        /// <param name="ds"> срез базы данных</param>
        /// <param name="currencyCode">код валюты</param>
        /// <returns></returns>
        private static decimal ExtractCurrencyRate(DataSet ds, string currencyCode)
        {
            if (ds == null)
                throw new ArgumentNullException("ds", "Параметр ds не может быть null.");

            if (string.IsNullOrEmpty(currencyCode))
                throw new ArgumentNullException("currencyCode", "Параметр currencyCode не может быть null.");

            DataTable dt = ds.Tables["ValuteCursOnDate"];

            DataRow[] rows = dt.Select(string.Format("VchCode=\'{0}\'", currencyCode));

            if (rows.Length > 0)
            {
                decimal result;
                if (decimal.TryParse(rows[0]["Vcurs"].ToString(), out result))
                    return result;
                throw new InvalidCastException("От службы ожидалось значение курса валют.");

            }
            throw new KeyNotFoundException("Для заданной валюты не найден курс.");

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
