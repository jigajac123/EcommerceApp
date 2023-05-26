using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestFullApi.Controllers;
using RestFullApi.Models;
using RestFullApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFullApiTests.TestControllers
{
    [TestClass()]
    public class OrderControllersTest
    {
        [TestMethod()]
        public async Task Get_on_Success_Invok_AllOrdersTest_OnlyOnce()
        {
            //Arrange

            var mockOrderServise = new Mock<IOrderServices>();
            mockOrderServise.Setup(service => service.AllOrders())
                .ReturnsAsync(new List<Order>());

            var sut = new OrderController(mockOrderServise.Object);

            //Act

            var result = await sut.AllOrders();

            //Assert

            mockOrderServise.Verify(service => service.AllOrders(), Times.Once);
        }



        [TestMethod()]
        public async Task Get_On_Success_AllOrders() 
        {
            //Arrange

            var mockOrderServise = new Mock<IOrderServices>();
            mockOrderServise.Setup(service => service.AllOrders())
                .ReturnsAsync(new List<Order>() {
                new (){
                    OrderID = 1,
                    CustomerID= 4,
                    Status="shipped"

                } });

            var sut = new OrderController(mockOrderServise.Object);

            //Act

            var result = await sut.AllOrders();

            //Assert

            result.Should().BeOfType<OkObjectResult>();
            var objRes = (OkObjectResult)result;
            objRes.Value.Should().BeOfType<List<Order>>();
        }

        [TestMethod()]
        public async Task Get_OnNotFoundResult_AllOrders() 
        {
            //Arrange

            var mockOrderServise = new Mock<IOrderServices>();
            mockOrderServise.Setup(service => service.AllOrders())
                .ReturnsAsync(new List<Order>());

            var sut = new OrderController(mockOrderServise.Object);

            //Act

            var result = await sut.AllOrders();

            //Assert

            result.Should().BeOfType<NotFoundResult>();

        }
    }
}
