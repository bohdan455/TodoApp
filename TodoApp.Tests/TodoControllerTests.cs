using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApp.Controllers;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Tests
{
    public class TodoControllerTests
    {
        private readonly Mock<ITodoRepository> _mockRepository;
        private readonly TodoController _controller;

        public TodoControllerTests()
        {
            _mockRepository = new Mock<ITodoRepository>();
            _controller = new TodoController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfTodoItems()
        {
            // Arrange
            var expectedItems = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Item 1", Description = "Description 1" },
            new TodoItem { Id = 2, Title = "Item 2", Description = "Description 2" },
            new TodoItem { Id = 3, Title = "Item 3", Description = "Description 3" },
        };
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedItems);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualItems = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(okResult.Value);
            Assert.Equal(expectedItems.Count, actualItems.Count());
            for (int i = 0; i < expectedItems.Count; i++)
            {
                Assert.Equal(expectedItems[i].Id, actualItems.ElementAt(i).Id);
                Assert.Equal(expectedItems[i].Title, actualItems.ElementAt(i).Title);
                Assert.Equal(expectedItems[i].Description, actualItems.ElementAt(i).Description);
            }
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithTodoItem()
        {
            // Arrange
            var id = 1;
            var expectedItem = new TodoItem { Id = id, Title = "Item 1", Description = "Description 1" };
            _mockRepository.Setup(repo => repo.GetById(id)).ReturnsAsync(expectedItem);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualItem = Assert.IsType<TodoItem>(okResult.Value);
            Assert.Equal(expectedItem.Id, actualItem.Id);
            Assert.Equal(expectedItem.Title, actualItem.Title);
            Assert.Equal(expectedItem.Description, actualItem.Description);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(repo => repo.GetById(id)).ThrowsAsync(new ArgumentException());

            // Act
            var result = await _controller.GetById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithCreatedTodoItem()
        {
            // Arrange
            var item = new TodoItem { Id = 1, Title = "Item 1", Description = "Description 1" };
            _mockRepository.Setup(repo => repo.Add(item)).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.Create(item);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualItem = Assert.IsType<TodoItem>(createdAtActionResult.Value);
            Assert.Equal(item.Id, actualItem.Id);
            Assert.Equal(item.Title, actualItem.Title);
            Assert.Equal(item.Description, actualItem.Description);
            _mockRepository.Verify();
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var item = new TodoItem { Id = 1, Title = "Item 1" };
            _controller.ModelState.AddModelError("Description", "The Description field is required.");

            // Act
            var result = await _controller.Create(item);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdateSucceeds()
        {
            // Arrange
            var item = new TodoItem { Id = 1, Title = "Item 1", Description = "Description 1" };
            _mockRepository.Setup(repo => repo.Update(item)).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.Update(item);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockRepository.Verify();
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var item = new TodoItem { Id = 1, Title = "Item 1" };
            _controller.ModelState.AddModelError("Description", "The Description field is required.");

            // Act
            var result = await _controller.Update(item);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var item = new TodoItem { Id = 1, Title = "Item 1", Description = "Description 1" };
            _mockRepository.Setup(repo => repo.Update(item)).ThrowsAsync(new ArgumentException());

            // Act
            var result = await _controller.Update(item);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleteSucceeds()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(repo => repo.Remove(id)).Returns(Task.CompletedTask).Verifiable();

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockRepository.Verify();
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(repo => repo.Remove(id)).ThrowsAsync(new ArgumentException());

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}