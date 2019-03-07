using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebsite.BLL.Interfaces
{
    public interface ISettingsProvider
    {
        int GetMaximumProductsCount { get; }
    }
}
