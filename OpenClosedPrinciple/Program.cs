#region OPC_Before
namespace Before
{
    class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
    }
    class Order
    {
        private List<Product> items = new();
        private string Shipping = default;

        public decimal GetTotal()
            => items.Sum(p => p.Price);

        public decimal GetTotalWeight()
            => items.Sum(p => p.Weight);

        public void SetShippingType(string type)
            => Shipping = type;

        public decimal GetShippingCost()
        {
            if (Shipping == "ground")
            {
                if (GetTotal() > 100)
                    return 0;
                return Math.Max(10, GetTotalWeight() * 1.5M);
            }
            if (Shipping == "air")
            {
                return Math.Max(20, GetTotalWeight() * 3);
            }

            throw new ArgumentException(nameof(Shipping));
        }


        public DateTime GetShippingDate()
        {
            if (Shipping == "ground")
            {
                return DateTime.Now.AddDays(7);
            }
            if (Shipping == "air")
            {
                return DateTime.Now.AddDays(2);
            }

            throw new ArgumentException(nameof(Shipping));
        }

    }
}
#endregion



#region OPC_After
namespace After
{
    interface IShipping
    {
        DateTime GetDate();
        decimal GetCost(Order order);
    }

    class Ground : IShipping
    {
        public decimal GetCost(Order order)
        {
            if (order.GetTotal() > 100)
                return 0;
            return Math.Max(10, order.GetTotalWeight() * 1.5M);

        }
        public DateTime GetDate()
        {
            return DateTime.Now.AddDays(7);
        }
    }

    class Air : IShipping
    {
        public decimal GetCost(Order order)
        {
            if (order.GetTotal() > 100)
                return 0;
            return Math.Max(20, order.GetTotalWeight() *3);

        }
        public DateTime GetDate()
        {
            return DateTime.Now.AddDays(2);
        }
    }



    class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
    }
    class Order
    {
        private List<Product> items = new();
        private IShipping Shipping;

        public decimal GetTotal()
            => items.Sum(p => p.Price);

        public decimal GetTotalWeight()
            => items.Sum(p => p.Weight);

        public void SetShippingType(IShipping type)
            => Shipping = type;

        public decimal GetShippingCost()=>Shipping.GetCost(this);

        public DateTime GetShippingDate() => Shipping.GetDate();

    }
}



#endregion
