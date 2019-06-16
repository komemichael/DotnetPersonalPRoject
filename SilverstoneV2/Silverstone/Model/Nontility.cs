using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverstone.Model
{
    
    class Nontility
    {
        int key;
        DateTime date;
        String item;
        double amount;

        public Nontility(int key, DateTime date, String item, double amount)
        {
            this.key = key;
            this.date = date;
            this.item = item;
            this.amount = amount;
        }

        public void SetKey(int key)
        {
            this.key = key;
        }

        public void SetDate(DateTime date)
        {
            this.date = date;
        }

        public void SetAmount(double amount)
        {
            this.amount = amount;
        }

        public void SetKey(String item)
        {
            this.item = item;
        }

        public int GetKey()
        {
            return this.key;
        }

        public DateTime GetDate()
        {
            return this.date;
        }

        public String GetItem()
        {
            return this.item;
        }

        public double GetAmount()
        {
            return this.amount;
        }
    }
}
