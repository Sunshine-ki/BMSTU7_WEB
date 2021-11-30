using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Npgsql;

using bl;
using db;

namespace Head.Services
{
	public class UserService
	{
		private IRepositoryUser _repositoryUser;

		public UserService(IRepositoryUser repositoryUser)
		{
			_repositoryUser = repositoryUser;
		}

		private Head.Answer CheckPassword(string password)
		{
			if (password.Length < 6)
				return new Head.Answer((int)Constants.Errors.ShortLengthPassword, "Длина пароля должна быть больше 5 символов");

			if (Extensions.IsNumeric(password))
				return new Head.Answer((int)Constants.Errors.OnlyNumericPassword, "Пароль не должен состоять только из цифр");

			return new Head.Answer(Constants.OK, "Ok");
		}

		private Head.Answer IsUserExist(bl.User user)
		{
			var userOld = Converter.ConvertUserToBL(_repositoryUser.GetUserByEmail(user.Email));
			
			if (userOld != null)
			{
				return new Head.Answer((int)Constants.Errors.EmailUserExists, "Данный email занят");
			}

			userOld = Converter.ConvertUserToBL(_repositoryUser.GetUserByLogin(user.Login));
			if (userOld != null)
			{
				return new Head.Answer((int)Constants.Errors.LoginUserExists, "Данный логин занят");
			}

			return null;
		}

		public Head.Answer AddUser(bl.User user)
		{
			var isExists = IsUserExist(user); 
			if (isExists != null)
			{
				return isExists;
			}

			var checkPassword = CheckPassword(user.Password);
			if (checkPassword.returnValue != Constants.OK)
			{
				return checkPassword;
			}

			user.Id = 0;
			_repositoryUser.Add(Converter.ConvertUserToBD(user));
			_repositoryUser.Save();

			return new Head.Answer(Constants.OK, "Ok");
		}

		public int GetIdByLogin(bl.User user)
		{
			var id = _repositoryUser.GetUserByLogin(user.Login).Id;
			return id;
		}	

		public Head.Answer LogIn(bl.User user)
		{
			var realUser = _repositoryUser.GetUserByLogin(user.Login);

			if (realUser == null)
			{
				return new Head.Answer((int)Constants.Errors.UserNotExist, "Пользователя с таким логином не существует");
			}

			var oldUser = Converter.ConvertUserToBL(realUser);

			if (!oldUser.Password.Equals(user.Password))
			{
				return new Head.Answer((int)Constants.Errors.IncorrectPassword, "Неправильный пароль");
			}

			return new Head.Answer(Constants.OK, "Ok");;
		}

		public Head.Answer UpdateUser(string login, bl.User newUser)
		{
			var realUser = _repositoryUser.GetUserByLogin(login);

			if (realUser == null)
			{
				return new Head.Answer((int)Constants.Errors.UserNotExist, "Пользователя с таким логином не существует");
			}

			var user = new db.User()
			{
				Name = String.IsNullOrEmpty(newUser.Name) ? realUser.Name : newUser.Name,
				Surname = String.IsNullOrEmpty(newUser.Surname) ? realUser.Surname : newUser.Surname,
				Email = String.IsNullOrEmpty(newUser.Email) ? realUser.Email : newUser.Email,
				Login = String.IsNullOrEmpty(newUser.Login) ? realUser.Login : newUser.Login,
				Password = String.IsNullOrEmpty(newUser.Password) ? realUser.Password : newUser.Password,
				UserType = realUser.UserType // UserType cannot be updated
			};

			Console.WriteLine(user);
			Console.WriteLine($"realUser.Id = {realUser.Id}");

			_repositoryUser.Update(realUser.Id, user);
			_repositoryUser.Save();

			return new Head.Answer(Constants.OK, "Ok");;
		}

	}
}