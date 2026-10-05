using Blog.Application.Abstractions.Authentication;
using Blog.Application.Abstractions.Persistence;
using Blog.Application.Abstractions.Repositories;
using Blog.Application.Common.Exceptions;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Users.RegisterUser
{
    public class RegisterUserHandler(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork, RegisterUserValidator validator)
    {
        private const string ReaderRoleName = "Reader";

        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly RegisterUserValidator _validator = validator;

        public async Task<Guid> HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(error => error.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(error => error.ErrorMessage).ToArray());

                throw new ValidationException(errors);
            }

            var emailExists = await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken);

            if (emailExists)
                throw new ConflictException("A user with this email already exists.");

            var readerRole = await _roleRepository.GetByNameAsync(ReaderRoleName, cancellationToken) ?? throw new NotFoundException("Reader role was not found.");
            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = new User(
                request.FirstName,
                request.LastName,
                request.Email,
                passwordHash,
                readerRole.Id);

            await _userRepository.AddAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
