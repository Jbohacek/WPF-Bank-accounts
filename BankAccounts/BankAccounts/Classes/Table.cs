using BankAccounts.Database.Tables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace BankAccounts.Classes
{
    public class MoneyTable
    {
        public List<Sloupec> Income { get; set; }
        public List<Sloupec> DeIncome { get; set; }

        public MoneyTable(List<Sloupec> _Income, List<Sloupec> _DeIncome)
        {
            Income = _Income;
            DeIncome = _DeIncome;


        }

        public void OrderByID(List<Transaction> _Transakce)
        {
            if (_Transakce.Count > 0)
            {
                Transaction[] transakce = new Transaction[7];
                transakce = _Transakce.Take(7).Reverse().ToArray();

                int maxVelikost = 196;

                Transaction NejvetsiIncome = transakce.OrderBy(x => x.Payment).Reverse().First();
                Transaction NejvetsiDeIncome = transakce.OrderBy(x => x.Payment).Reverse().Last();

                for (int i = 0; i != 7; i++)
                {
                    double velikost = 0;
                    if (i < transakce.Length)
                    {
                        DeIncome[i].BoxIncome.Visibility = System.Windows.Visibility.Visible;
                        DeIncome[i].Amount.Visibility = System.Windows.Visibility.Visible;
                        Income[i].BoxIncome.Visibility = System.Windows.Visibility.Visible;
                        Income[i].Amount.Visibility = System.Windows.Visibility.Visible;

                        switch (transakce[i].Payment)
                        {
                            case > 0:
                                velikost = (transakce[i].Payment * maxVelikost) / NejvetsiIncome.Payment;
                                Income[i].BoxIncome.Height = velikost;
                                Income[i].Amount.Text = "+ " + transakce[i].GetPayment + " CZK";
                                DeIncome[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                                DeIncome[i].Amount.Visibility = System.Windows.Visibility.Hidden;

                                DoubleAnimation Zvetsi = new DoubleAnimation(velikost, new Duration(TimeSpan.FromSeconds(0.5))) { From = 0 };
                                Income[i].BoxIncome.BeginAnimation(FrameworkElement.HeightProperty, Zvetsi);

                                break;
                            case < 0:
                                velikost = (transakce[i].Payment * maxVelikost) / NejvetsiDeIncome.Payment;
                                DeIncome[i].BoxIncome.Height = velikost;
                                DeIncome[i].Amount.Text = transakce[i].GetPayment + " CZK";
                                Income[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                                Income[i].Amount.Visibility = System.Windows.Visibility.Hidden;

                                DoubleAnimation Zmensi = new DoubleAnimation(velikost, new Duration(TimeSpan.FromSeconds(0.5))) { From = 0 };
                                DeIncome[i].BoxIncome.BeginAnimation(FrameworkElement.HeightProperty, Zmensi);
                                break;
                            default:
                                Income[i].Amount.Text = "0 CZK";
                                DeIncome[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                                DeIncome[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                                Income[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                                Income[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                                break;
                        }

                        Income[i].Date.Text = "#" + transakce[i].id.ToString();
                    }
                    else
                    {
                        Income[i].Amount.Text = "0 CZK";
                        DeIncome[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                        DeIncome[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                        Income[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                        Income[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                        Income[i].Date.Text = "";
                    }

                }
            }else
            {
                for (int i = 0; i!= 7; i++) 
                {
                    Income[i].Amount.Text = "0 CZK";
                    DeIncome[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                    DeIncome[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                    Income[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                    Income[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                    Income[i].Date.Text = "";
                }

            }
        }


        public void OrderByDate(List<Transaction> _Transakce)
        {
            var x = _Transakce.GroupBy(x => x.PaymentDay.Day).ToList();
            List<OrderHodnota> Hodnoty = new List<OrderHodnota>();

            for (int i = 0; i != 7; i++)
            {
                if (i < x.Count)
                { 
                    double Celkem = 0;
                    foreach (Transaction Item in x[i])
                    {
                        Celkem += Item.Payment;
                    }

                    Hodnoty.Add(new OrderHodnota() { Amount = Celkem, PaymentDate = (x[i].First() as Transaction).GetDate });
                }
            }


            Hodnoty.Reverse();

            double NejvetsiIncome = Hodnoty.Max(x => x.Amount);
            double NejvetsiDeIncome = Hodnoty.Min(x => x.Amount);

            double maxVelikost = 196;
            double velikost = 0;

            for (int i = 0; i != 7; i++)
            {
                if (i < Hodnoty.Count)
                {
                    DeIncome[i].BoxIncome.Visibility = System.Windows.Visibility.Visible;
                    DeIncome[i].Amount.Visibility = System.Windows.Visibility.Visible;
                    Income[i].BoxIncome.Visibility = System.Windows.Visibility.Visible;
                    Income[i].Amount.Visibility = System.Windows.Visibility.Visible;

                    switch (Hodnoty[i].Amount)
                    {
                        case > 0:
                            velikost = (Hodnoty[i].Amount * maxVelikost) / NejvetsiIncome;
                            Income[i].BoxIncome.Height = velikost;
                            Income[i].Amount.Text = "+ " + Hodnoty[i].GetAmount + " CZK";
                            DeIncome[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                            DeIncome[i].Amount.Visibility = System.Windows.Visibility.Hidden;

                            DoubleAnimation Zvetsi = new DoubleAnimation(velikost, new Duration(TimeSpan.FromSeconds(0.5))) { From = 0 };
                            Income[i].BoxIncome.BeginAnimation(FrameworkElement.HeightProperty, Zvetsi);

                            break;
                        case < 0:
                            velikost = (Hodnoty[i].Amount * maxVelikost) / NejvetsiDeIncome;
                            DeIncome[i].BoxIncome.Height = velikost;
                            DeIncome[i].Amount.Text = Hodnoty[i].GetAmount + " CZK";
                            Income[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                            Income[i].Amount.Visibility = System.Windows.Visibility.Hidden;

                            DoubleAnimation Zmensi = new DoubleAnimation(velikost, new Duration(TimeSpan.FromSeconds(0.5))) { From = 0 };
                            DeIncome[i].BoxIncome.BeginAnimation(FrameworkElement.HeightProperty, Zmensi);
                            break;
                        default:
                            Income[i].Amount.Text = "0 CZK";
                            DeIncome[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                            DeIncome[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                            Income[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                            Income[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                            break;
                    }

                    Income[i].Date.Text = Hodnoty[i].PaymentDate;
                }
                else
                {
                    Income[i].Amount.Text = "0 CZK";
                    DeIncome[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                    DeIncome[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                    Income[i].BoxIncome.Visibility = System.Windows.Visibility.Hidden;
                    Income[i].Amount.Visibility = System.Windows.Visibility.Hidden;
                    Income[i].Date.Text = "";
                }
            }
            

        }

        private struct OrderHodnota
        {
            public double Amount { get; set; }
            public string PaymentDate { get; set; }

            public string GetAmount { get {

                    var pok = Amount.ToString().ToCharArray();
                    Array.Reverse(pok);

                    string konec = "";
                    for (int i = 0; i < Amount.ToString().Length; i++)
                    {
                        konec += pok[i];
                        if (i % 3 == 2 && i != Amount.ToString().Length)
                        {
                            konec += " ";
                        }
                    }



                    var kon = konec.ToCharArray();
                    Array.Reverse(kon);

                    string Finalni = new string(kon);

                    if (Finalni.Substring(0, 1) == " ")
                    {
                        Finalni = Finalni.Substring(1);
                    }

                    if (Finalni.Substring(0, 1) == "-" && Finalni.Substring(1, 1) != " ")
                    {
                        Finalni = Finalni.Insert(1, " ");
                    }

                    return Finalni + " CZK";

                } }
        }
    }
    
}
