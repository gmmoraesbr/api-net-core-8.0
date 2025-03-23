using Base.Application.Interfaces;
using Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using FluentAssertions;
using Base.Infrastructure.Services;

namespace Base.UnitTests;

public class UserAuthServiceTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly UserAuthService _authService;

    public UserAuthServiceTests()
    {
        var userStoreMock = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);

        _jwtServiceMock = new Mock<IJwtService>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();

        _authService = new UserAuthService(
            _userManagerMock.Object);
    }

    //[Fact]
    //public async Task Should_ThrowException_When_UserIsNull()
    //{
    //    // Arrange
    //    User user = null;

    //    // Act
    //    Func<Task> act = async () => await _authService.GerarJwt(user);

    //    // Assert
    //    await act.Should().ThrowAsync<ArgumentNullException>()
    //        .WithMessage("*user*");
    //}

    //[Fact]
    //public async Task Should_Return_Token_When_User_IsValid()
    //{
    //    // Arrange
    //    var user = User.Create
    //        (
    //            "Moraes",
    //            "admin@admin.com",
    //            "admin@admin.com"
    //        );

    //    _userManagerMock.Setup(m => m.GetRolesAsync(user))
    //        .ReturnsAsync(new[] { "Admin" });

    //    _jwtServiceMock.Setup(j => j.GerarToken(It.IsAny<User>(), It.IsAny<IList<string>>()))
    //        .Returns("fake-jwt-token");

    //    // Act
    //    var result = await _authService.GerarJwt(user);

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Token.Should().Be("fake-jwt-token");
    //}
}
