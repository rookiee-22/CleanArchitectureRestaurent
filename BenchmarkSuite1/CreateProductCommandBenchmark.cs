using BenchmarkDotNet.Attributes;
using Application.Features.Products.Commands;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.Categories;
using Domain.Entities.Products;
using Moq;
using Microsoft.VSDiagnostics;

namespace Application.Benchmarks
{
    [SimpleJob(warmupCount: 3, targetCount: 5)]
    [CPUUsageDiagnoser]
    public class CreateProductCommandBenchmark
    {
        private CreateProductCommandHandler _handler;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private CreateProductCommand _testCommand;
        [GlobalSetup]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateProductCommandHandler(_mockMapper.Object, _mockUnitOfWork.Object);
            // Setup test data
            _testCommand = new CreateProductCommand
            {
                Name = "Margherita Pizza",
                Price = 500,
                Profile = "pizza.jpg",
                Description = "Classic cheese pizza",
                CategoryId = 1
            };
            // Mock repository behavior
            var mockCategoryRepo = new Mock<IGenericRepository<Category>>();
            mockCategoryRepo.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Category { Id = 1, Name = "Pizza" });
            var mockProductRepo = new Mock<IGenericRepository<Product>>();
            mockProductRepo.Setup(x => x.CreateAsync(It.IsAny<Product>())).ReturnsAsync(new Product { Id = 1, Name = "Margherita Pizza", Price = 500, CategoryId = 1 });
            _mockUnitOfWork.Setup(x => x.Repository<Category>()).Returns(mockCategoryRepo.Object);
            _mockUnitOfWork.Setup(x => x.Repository<Product>()).Returns(mockProductRepo.Object);
            _mockUnitOfWork.Setup(x => x.Save(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mockMapper.Setup(x => x.Map<Product>(_testCommand)).Returns(new Product { Name = _testCommand.Name, Price = _testCommand.Price, Profile = _testCommand.Profile, Description = _testCommand.Description, CategoryId = _testCommand.CategoryId });
        }

        [Benchmark]
        public async Task CreateProductWithValidCategory()
        {
            await _handler.Handle(_testCommand, CancellationToken.None);
        }

        [Benchmark]
        public async Task CreateProductWithoutCategory()
        {
            var commandWithoutCategory = new CreateProductCommand
            {
                Name = "Appetizer",
                Price = 200,
                Profile = "appetizer.jpg",
                Description = "Starter dish",
                CategoryId = null
            };
            await _handler.Handle(commandWithoutCategory, CancellationToken.None);
        }
    }
}