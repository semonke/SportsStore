using System.Linq;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};

            Cart target = new Cart();
            
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();
            
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Item()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};

            Cart target = new Cart();
            
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.ToArray();
            
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product {ProductID = 2, Name = "P2"};
            Product p3 = new Product {ProductID = 3, Name = "P3"};

            Cart target = new Cart();
            
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            
            target.RemoveLine(p2);
            
            Assert.Empty(target.Lines.Where(c => c.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1", Price = 100M};
            Product p2 = new Product {ProductID = 2, Name = "P2", Price = 50M};

            Cart target = new Cart();
            
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
            
            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            Product p1 = new Product {ProductID = 1, Name = "P1", Price = 100M};
            Product p2 = new Product {ProductID = 2, Name = "P2", Price = 50M};

            Cart target = new Cart();
            
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            
            target.Clear();
            
            Assert.Empty(target.Lines);
        }
    }
}