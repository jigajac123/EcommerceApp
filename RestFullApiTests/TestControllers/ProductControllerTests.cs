using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestFullApi.Controllers;
using RestFullApi.DataBase;
using RestFullApi.Models;
using RestFullApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace RestFullApi.Controllers.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {

        [TestMethod()]
        public async Task Get_on_Success_Invok_AllProductsTest_OnlyOnce() 
        {
            //Arrange

            var mockProductServise = new Mock<IProductServices>();
            mockProductServise.Setup(service => service.AllProducts())
                .ReturnsAsync(new List<Product>());

            var sut = new ProductController(mockProductServise.Object);

            //Act

            var result = await sut.AllProducts();

            //Assert

            mockProductServise.Verify(service => service.AllProducts(), Times.Once);
        }



        [TestMethod()]
        public async Task Get_On_Success_AllProducts()
        {
            //Arrange

            var mockProductServise = new Mock<IProductServices>();
            mockProductServise.Setup(service => service.AllProducts())
                .ReturnsAsync(new List<Product>() {
                new (){
                    ProductID = 1,
                    ProductName= "Test"

                } });

            var sut = new ProductController(mockProductServise.Object);

            //Act

            var result = await sut.AllProducts();

            //Assert

            result.Should().BeOfType<OkObjectResult>();
            var objRes=  (OkObjectResult)result;
            objRes.Value.Should().BeOfType<List<Product>>();
        }

        [TestMethod()]
        public async Task Get_OnNotFoundResult_AllProducts() 
        {
            //Arrange

            var mockProductServise = new Mock<IProductServices>();
            mockProductServise.Setup(service => service.AllProducts())
                .ReturnsAsync(new List<Product>());

            var sut = new ProductController(mockProductServise.Object);

            //Act

            var result = await sut.AllProducts();

            //Assert

            result.Should().BeOfType<NotFoundResult>();

        }
    }
}