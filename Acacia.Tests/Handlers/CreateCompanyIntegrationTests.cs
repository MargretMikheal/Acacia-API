using System.Threading;
using System.Threading.Tasks;
using Acacia.Core.Features.Companies;
using Acacia.Core.Features.Companies.Commands.CreateCompany;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace Acacia.Tests.Handlers
{
    public class CreateCompanyHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUow = new();
        private readonly Mock<ICompanyRepository> _mockCompanyRepo = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly Mock<IStringLocalizer<SharedResources>> _mockLocalizer = new();
        private readonly Mock<ICloudinaryService> _mockFileService = new();
        private readonly CreateCompanyHandler _handler;

        public CreateCompanyHandlerTests()
        {
            _mockUow.Setup(u => u.companyRepository).Returns(_mockCompanyRepo.Object);
            _mockLocalizer.Setup(l => l[It.IsAny<string>()]).Returns(new LocalizedString("Key", "Value"));

            _handler = new CreateCompanyHandler(
                _mockUow.Object,
                _mockMapper.Object,
                _mockLocalizer.Object,
                _mockFileService.Object
            );
        }

        [Fact]
        public async Task Handle_WhenCompanyDoesNotExist_CreatesSuccessfully()
        {
            var command = new CreateCompanyCommand { Name = "FragranceCorp", Image = null! };
            var entity = new Company { Id = 1, Name = "FragranceCorp" };
            var responseDto = new CompanyResponse { Id = 1, Name = "FragranceCorp" };

            _mockCompanyRepo.Setup(r => r.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Company, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            _mockMapper.Setup(m => m.Map<Company>(command)).Returns(entity);
            _mockCompanyRepo.Setup(r => r.AddAsync(entity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<CompanyResponse>(entity)).Returns(responseDto);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();
            result.Data.Name.Should().Be("FragranceCorp");
        }
    }
}
