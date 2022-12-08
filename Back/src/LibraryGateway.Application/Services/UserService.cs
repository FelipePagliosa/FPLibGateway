using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LibraryGateway.Application.Interfaces;
using LibraryGateway.Application.Requests.UserRequests;
using LibraryGateway.Domain.Exceptions;
using LibraryGateway.Domain.Models;
using LibraryGateway.Domain.Repository;

namespace LibraryGateway.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Add(UserInsertRequest userInsertRequest)
    {
        var existente = await UserExists(userInsertRequest.UserName);

        if(existente){
            throw new LibraryGatewayExceptions("Este registro de Login já existe");
        }

        _unitOfWork.Iniciar();

        var user = _mapper.Map<User>(userInsertRequest);
        user.Senha =  BCrypt.Net.BCrypt.HashPassword(userInsertRequest.Senha);

        _unitOfWork.UserRepository.Add(user);

        await _unitOfWork.CommitarAsync();
    }

    public async Task Update(UserUpdateRequest userUpdateRequest)
    {
        var existente = await _unitOfWork.UserRepository.GetUserByIdAsync(userUpdateRequest.Id);

        if (userUpdateRequest.Id == 0 || existente == null){
            return;
        }

        _unitOfWork.Iniciar();

        existente.Nome = userUpdateRequest.Nome;
        existente.UserName = userUpdateRequest.UserName;
        existente.Email = userUpdateRequest.Email;
        existente.Perfil = userUpdateRequest.Perfil;
        existente.Ativo = userUpdateRequest.Ativo;

        _unitOfWork.UserRepository.Update(existente);
        await _unitOfWork.CommitarAsync();
    }

    public async Task ChangePassword(UserChangePasswordRequest userChangePasswordRequest)
    {
        var existente = await _unitOfWork.UserRepository.GetUserByIdAsync(userChangePasswordRequest.Id);

        if (userChangePasswordRequest.Id == 0 || existente == null){
            return;
        }

        _unitOfWork.Iniciar();

        existente.Senha = BCrypt.Net.BCrypt.HashPassword(userChangePasswordRequest.Senha);

        _unitOfWork.UserRepository.Update(existente);
        await _unitOfWork.CommitarAsync();
    }

    public async Task Delete(int userId)
    {

        var user = await _unitOfWork.UserRepository
            .GetUserByIdAsync(userId)
        ;
        if (user == null) throw new LibraryGatewayExceptions("Registro não encontrado.");

        _unitOfWork.Iniciar();
        _unitOfWork.UserRepository.Delete(user);
        await _unitOfWork.CommitarAsync();
    }

    public async Task<bool> CheckUserPassword(string passwordHash, string passwordTyped)
    {
        return BCrypt.Net.BCrypt.Verify(passwordTyped, passwordHash);
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(userName);
        return user;
    }

    private async Task<bool> UserExists(string userName)
    {
        var user =  await _unitOfWork.UserRepository.GetUserByUserNameAsync(userName);
        return user != null;
    }

    public async Task<List<User>> GetAll()
    {
        return await _unitOfWork.UserRepository.GetUsersAsync();
    }
}
