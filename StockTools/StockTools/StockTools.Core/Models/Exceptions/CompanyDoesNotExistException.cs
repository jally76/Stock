using System;

namespace StockTools.Core.Models.Exceptions
{
    public class CompanyDoesNotExistException : Exception
    {
        public CompanyDoesNotExistException(string companyName)
        {
            this.Data["CompanyName"] = companyName;
        }
    }
}