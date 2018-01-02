using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Filter.Base
{
    /// <summary>
    /// Output processor
    /// </summary>
    public interface IOutputProcessor
    {
        /// <summary>
        /// Process the string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>Output string</returns>
        string Process(string input);
    }
}