using System;
using System.Collections.Generic;
using System.Linq;

using bl;

namespace db
{
	public class MySQLFacade : bl.IFacade
	{
		public MySQLFacade()
		{
		}

		public List<bl.Task> GetTasks()
		{
			IRepositoryTask db = new MySQLRepositoryTask();

			List<db.Task> tasks = db.GetTasks();
			List<bl.Task> result = new List<bl.Task>();

			foreach (db.Task elem in tasks)
				result.Add(Converter.ConvertTaskToBL(elem));

			return result;
		}
		public List<bl.User> GetUsers()
		{
			IRepositoryUser db = new MySQLRepositoryUser();

			List<db.User> users = db.GetUsers();
			List<bl.User> result = new List<bl.User>();

			foreach (db.User elem in users)
				result.Add(Converter.ConvertUserToBL(elem));

			return result;
		}
		public List<bl.CompletedTask> GetCompletedTasks()
		{
			IRepositoryCompletedTask db = new MySQLRepositoryCompletedTask();

			List<db.CompletedTask> completedTask = db.GetCompletedTasks();
			List<bl.CompletedTask> result = new List<bl.CompletedTask>();

			foreach (db.CompletedTask elem in completedTask)
				result.Add(Converter.ConvertCompletedTaskToBL(elem));

			return result;
		}

		public int AddTask(bl.Task task)
		{
			IRepositoryTask db = new MySQLRepositoryTask();
			db.Task taskDB = Converter.ConvertTaskToBD(task);
			taskDB.Id = 0;
			db.Add(taskDB);
			db.Save();
			return 0;
		}

		public int AddUser(bl.User user)
		{
			IRepositoryUser db = new MySQLRepositoryUser();
			db.User userDB = Converter.ConvertUserToBD(user);
			userDB.Id = 0;
			db.Add(userDB);
			db.Save();
			return 0;
		}

		public int AddCompletedTask(bl.CompletedTask completedTask)
		{
			IRepositoryCompletedTask db = new MySQLRepositoryCompletedTask();
			db.CompletedTask completedTaskDB = Converter.ConvertCompletedTaskToBD(completedTask);

			// Проверка на то, что пользователь уже решил данную задачу.
			CompletedTask tmp =
						(from p in db.GetCompletedTasks()
						 where p.UserId == completedTask.UserId && p.TaskId == completedTask.TaskId
						 select p).FirstOrDefault();
			if (tmp != null)
			{
				Console.WriteLine("User has already solved this task");
				return 0;
			}

			// Проверка на то, что существует пользователь и задача.
			bl.User user = GetUser(completedTask.UserId);
			if (user is null)
			{
				Console.WriteLine("User not found");
				return 0;
			}
			bl.Task task = GetTask(completedTask.TaskId);
			if (task is null)
			{
				Console.WriteLine("Task not found");
				return 0;
			}

			completedTaskDB.Id = 0;
			db.Add(completedTaskDB);
			db.Save();
			return 0;
		}

		public bl.CompletedTask GetCompletedTask(int id)
		{
			IRepositoryCompletedTask db = new MySQLRepositoryCompletedTask();
			db.CompletedTask completedTask = db.GetCompletedTask(id);
			return Converter.ConvertCompletedTaskToBL(completedTask);
		}

		public bl.Task GetTask(int id)
		{
			IRepositoryTask db = new MySQLRepositoryTask();
			db.Task task = db.GetTask(id);
			return Converter.ConvertTaskToBL(task);
		}

		public bl.User GetUser(int id)
		{
			IRepositoryUser db = new MySQLRepositoryUser();
			db.User user = db.GetUser(id);
			return Converter.ConvertUserToBL(user);
		}

		public bl.User GetUserByEmail(string email)
		{
			IRepositoryUser db = new MySQLRepositoryUser();
			db.User user = db.GetUserByEmail(email);
			return Converter.ConvertUserToBL(user);
		}
		public bl.User GetUserByLogin(string login)
		{
			IRepositoryUser db = new MySQLRepositoryUser();
			db.User user = db.GetUserByLogin(login);
			return Converter.ConvertUserToBL(user);
		}


	}
}
