using StooqProxy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StooqProxy.Company
{
    public interface ICompany
    {
        MCompany GetByShortcut(string shortcut);
    }
}
