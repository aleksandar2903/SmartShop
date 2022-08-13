using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Services.Settings
{
    internal interface ISettingsService
    {
        string AuthAccessToken { get; set; }
    }
}
