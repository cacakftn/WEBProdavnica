using BusinessLayer.Impl;
using DAL.Abstract;
using Entities;
using Moq;

namespace UnitTest
{
    public class ProductBusinessTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductBusiness _productBusiness;

        public ProductBusinessTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productBusiness = new ProductBusiness(_productRepositoryMock.Object);
        }

        #region Add Method Tests

        [Fact]
        public void Add_UspesnoDodat_VracaStatusTrue()
        {
            // Arrange
            var product = new Product
            {
                IdProduct = 1,
                Name = "Test Proizvod",
                Price = 100,
                Count = 10,
                IdCategory = 1
            };

            _productRepositoryMock.Setup(x => x.Add(product)).Returns(true);

            // Act
            var result = _productBusiness.Add(product);

            // Assert
            Assert.True(result.Status);
            Assert.Equal("Proizvod je uspesno doddat", result.Message);
        }

        [Fact]
        public void Add_NeuspesnoDodavanje_VracaStatusFalse()
        {
            // Arrange
            var product = new Product
            {
                IdProduct = 1,
                Name = "Test Proizvod",
                Price = 100,
                Count = 10,
                IdCategory = 1
            };

            _productRepositoryMock.Setup(x => x.Add(product)).Returns(false);

            // Act
            var result = _productBusiness.Add(product);

            // Assert
            Assert.False(result.Status);
            Assert.Equal("Greska", result.Message);
        }

        [Fact]
        public void Add_ProveriDaLiJeRepositoryMetodaPozvana()
        {
            // Arrange
            var product = new Product
            {
                IdProduct = 1,
                Name = "Test Proizvod",
                Price = 150,
                Count = 5,
                IdCategory = 2
            };

            _productRepositoryMock.Setup(x => x.Add(product)).Returns(true);

            // Act
            _productBusiness.Add(product);

            // Assert
            _productRepositoryMock.Verify(x => x.Add(product), Times.Once);
        }

        [Fact]
        public void Add_SaRazlicitimProizvodima_VracaKorektneRezultate()
        {
            // Arrange
            var product1 = new Product { IdProduct = 1, Name = "Proizvod 1", Price = 50, Count = 20, IdCategory = 1 };
            var product2 = new Product { IdProduct = 2, Name = "Proizvod 2", Price = 75, Count = 15, IdCategory = 2 };

            _productRepositoryMock.Setup(x => x.Add(product1)).Returns(true);
            _productRepositoryMock.Setup(x => x.Add(product2)).Returns(false);

            // Act
            var result1 = _productBusiness.Add(product1);
            var result2 = _productBusiness.Add(product2);

            // Assert
            Assert.True(result1.Status);
            Assert.False(result2.Status);
        }

        #endregion

        #region Delete Method Tests

        [Fact]
        public void Delete_UspesnoBrisanje_VracaStatusTrue()
        {
            // Arrange
            var product = new Product { IdProduct = 1, Name = "Test", Price = 100, Count = 10 };

            _productRepositoryMock.Setup(x => x.Delete(product.IdProduct)).Returns(true);

            // Act
            var result = _productBusiness.Delete(product);

            // Assert
            Assert.True(result.Status);
            Assert.Equal("Proizvod je uspesno obrisan", result.Message);
        }

        [Fact]
        public void Delete_NeuspesnoBrisanje_VracaStatusFalse()
        {
            // Arrange
            var product = new Product { IdProduct = 999, Name = "Test", Price = 100, Count = 10 };

            _productRepositoryMock.Setup(x => x.Delete(product.IdProduct)).Returns(false);

            // Act
            var result = _productBusiness.Delete(product);

            // Assert
            Assert.False(result.Status);
            Assert.Equal("Greska", result.Message);
        }

        #endregion

        #region Get Method Tests

        [Fact]
        public void Get_PostojeciProizvod_VracaProizvod()
        {
            // Arrange
            var expectedProduct = new Product { IdProduct = 1, Name = "Test", Price = 100, Count = 10 };

            _productRepositoryMock.Setup(x => x.Get(1)).Returns(expectedProduct);

            // Act
            var result = _productBusiness.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.IdProduct, result.IdProduct);
            Assert.Equal(expectedProduct.Name, result.Name);
        }

        [Fact]
        public void Get_NepostojeciProizvod_VracaNull()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.Get(999)).Returns((Product)null!);

            // Act
            var result = _productBusiness.Get(999);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region GetAll Method Tests

        [Fact]
        public void GetAll_PostojeProizvodi_VracaListuProizvoda()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { IdProduct = 1, Name = "Proizvod 1", Price = 100, Count = 10 },
                new Product { IdProduct = 2, Name = "Proizvod 2", Price = 200, Count = 20 }
            };

            _productRepositoryMock.Setup(x => x.GetAll()).Returns(products);

            // Act
            var result = _productBusiness.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetAll_NemaProizvoda_VracaPraznuListu()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.GetAll()).Returns(new List<Product>());

            // Act
            var result = _productBusiness.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        #endregion

        #region Update Method Tests

        [Fact]
        public void Update_UspesnaIzmena_VracaStatusTrue()
        {
            // Arrange
            var product = new Product { IdProduct = 1, Name = "Izmenjen", Price = 150, Count = 5 };

            _productRepositoryMock.Setup(x => x.Update(product)).Returns(true);

            // Act
            var result = _productBusiness.Update(product);

            // Assert
            Assert.True(result.Status);
            Assert.Equal("Proizvod je uspesno izmenjen", result.Message);
        }

        [Fact]
        public void Update_NeuspesnaIzmena_VracaStatusFalse()
        {
            // Arrange
            var product = new Product { IdProduct = 999, Name = "Izmenjen", Price = 150, Count = 5 };

            _productRepositoryMock.Setup(x => x.Update(product)).Returns(false);

            // Act
            var result = _productBusiness.Update(product);

            // Assert
            Assert.False(result.Status);
            Assert.Equal("Greska", result.Message);
        }

        #endregion
    }
}