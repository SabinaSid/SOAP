using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_lab1_SOAP.CBR;

namespace Wpf_lab1_SOAP
{
    public class CBRService:INotifyPropertyChanged
    {
        private CBR.DailyInfo _cbrClient;

        // обеспечивает доступ к веб-сервису
        public CBR.DailyInfo Cbr

        {

            get

            {

                if (_cbrClient == null)

                {

                    _cbrClient = new DailyInfo();

                }

                return _cbrClient;

            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
