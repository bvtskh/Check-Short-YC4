using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckShort_YC4.Sigleton
{
    public class YaskawaWebService
    {
        private static readonly object sync = new object();
        private static volatile YaskawaService.YaskawaWebServiceSoapClient _yaskawa_service = null;
        public static YaskawaService.YaskawaWebServiceSoapClient Instance
        {
            get
            {
                if (_yaskawa_service == null)
                {
                    lock (sync)
                    {
                        if (_yaskawa_service == null)
                        {
                            _yaskawa_service = new YaskawaService.YaskawaWebServiceSoapClient();
                        }
                    }
                }
                return _yaskawa_service;
            }
        }
    }
}
