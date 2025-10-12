using BuildingBlocks.Application.Identity;
using Fintrack.Ledger.Application.Commands.CreateMovement;
using Fintrack.Ledger.Application.UnitTests.TestSupport.Builders;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.UnitTests.Commands.CreateMovement;

public class CreateMovementCommandHandlerTests
{
    private readonly Mock<IIdentityService> _identityService;
    private readonly Mock<IMovementRepository> _movementRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateMovementCommandHandler _handler;

    public CreateMovementCommandHandlerTests()
    {
        _identityService = new Mock<IIdentityService>();
        _movementRepositoryMock = new Mock<IMovementRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateMovementCommandHandler(
            _identityService.Object,
            _movementRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WhenValid_ThenPersistsAndReturnsSuccess()
    {
        // Arrange
        var command = new CreateMovementCommandBuilder().Build();

        _identityService
            .Setup(i => i.GetUserIdentity())
            .Returns(Guid.NewGuid());

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();

        _identityService.Verify(i => i.GetUserIdentity(), Times.Once());

        _movementRepositoryMock.Verify(r =>
            r.AddAsync(It.IsAny<Movement>(), It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(u =>
            u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _identityService.VerifyNoOtherCalls();
        _movementRepositoryMock.VerifyNoOtherCalls();
        _unitOfWorkMock.VerifyNoOtherCalls();
    }
}
