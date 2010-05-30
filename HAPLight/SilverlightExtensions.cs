using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HtmlAgilityPack
{
    public static class SilverlightExtensions
    {
        public static string[] Split(this string @this, char[] chars, int count)
        {
            var items = @this.Split(chars);
            return items.Length > 2 ? items.Take(2).ToArray() : items;
        }

        public static string[] Split(this string @this, string[] chars, int count)
        {
            var items = @this.Split(chars, StringSplitOptions.None);
            return items.Length > 2 ? items.Take(2).ToArray() : items;
        }

        
    }
}
