using System;
using System.Collections.Generic;
using System.Text;

namespace DstPasswordsCore.model
{
    public interface ISettings
    {
        void Init();

        string DB_PATH
        {
            get;set;
        }
        string KEYS_PATH
        {
            get; set;
        }
        bool CPY_PW
        {
            get;set;
        }
    }
}
