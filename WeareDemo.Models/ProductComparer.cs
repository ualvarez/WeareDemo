using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WeareDemo.Models
{
  public class ProductComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Product p1 = x as Product;
            Product p2 = y as Product;

            if (p1 == null || p2 == null)
            {
                return -1;
            }

            return p1.Name.CompareTo(p2.Name);
        }
    }
}
