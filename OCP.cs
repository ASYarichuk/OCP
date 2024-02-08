using System;
using System.Collections.Generic;

namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            IReadOnlyList<PaymentSystem> paymentSystems = new List<PaymentSystem> {
                new QIWI(),
                new WebMoney(),
                new Card(), };

            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler(paymentSystems);

            var systemId = orderForm.ShowForm();

            paymentHandler.Transition(systemId);
            paymentHandler.ShowPaymentResult(systemId);
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");

            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }

    public class PaymentHandler
    {
        private IReadOnlyList<PaymentSystem> _paymentSystems;
        public PaymentHandler(IReadOnlyList<PaymentSystem> paymentSystems)
        {
            _paymentSystems = paymentSystems;
        }

        public void ShowPaymentResult(string systemId)
        {
            Console.WriteLine($"Вы оплатили с помощью {systemId}");
            Console.WriteLine($"Проверка платежа через {systemId}...");
            Console.WriteLine("Оплата прошла успешно!");
        }

        public void Transition(string systemId)
        {
            foreach (var paymentSystem in _paymentSystems)
            {
                if (paymentSystem.GetPaymentSystemID() == systemId)
                {
                    paymentSystem.Transition();
                }
            }
        }
    }

    public abstract class PaymentSystem
    {
        public abstract string GetPaymentSystemID();
        public abstract void Transition();
    }

    public class QIWI : PaymentSystem
    {
        private string _systemID = "QIWI";
        public override string GetPaymentSystemID()
        {
            return _systemID;
        }

        public override void Transition()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
        }
    }

    public class WebMoney : PaymentSystem
    {
        private string _systemID = "WebMoney";
        public override string GetPaymentSystemID()
        {
            return _systemID;
        }

        public override void Transition()
        {
            Console.WriteLine("Вызов API WebMoney...");
        }
    }

    public class Card : PaymentSystem
    {
        private string _systemID = "Card";
        public override string GetPaymentSystemID()
        {
            return _systemID;
        }

        public override void Transition()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
        }
    }
}