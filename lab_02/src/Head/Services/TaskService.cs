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
	public class TaskService
	{
		private readonly string _connectionString = "Host=localhost;Port=5432;Database=coursework_db_exec;Username=lis;Password=password";

		private IRepositoryTask _repositoryTask;


		public TaskService(IRepositoryTask repositoryTask)
		{
			_repositoryTask = repositoryTask;
		}

		public bl.Task GetTask(int id)
		{
			var task = _repositoryTask.GetTask(id);

			if (task == null) return new bl.Task();

			return Converter.ConvertTaskToBL(task);
		}

		public List<bl.Task> GetTasks()
		{
			var result = new List<bl.Task>();
			var tasks = _repositoryTask.GetTasks();

			if (tasks == null) return result;

			foreach (var elem in tasks)
			{
				result.Add(Converter.ConvertTaskToBL(elem));
			}

			return result;
		}

		public Head.Answer CompareSolution(string sqlUser, int taskId)
		{
			var con = new NpgsqlConnection(_connectionString);

			bl.Task teacherTask = GetTask(taskId);
			
			string sqlTeacher = teacherTask.Solution;

			List<ArrayList> userResult = null;
			List<ArrayList> teacherResult = null;

			con.Open();
			
			try 
			{
				userResult = Task.ExecTask(sqlUser, con);
			}
			catch (Exception e)
			{
				con.Close();
				return new Head.Answer((int)Constants.Errors.UserExecTask,e.Message);
			}

			try 
			{
				teacherResult = Task.ExecTask(sqlTeacher, con);
			}
			catch (Exception e)
			{
				con.Close();
				return new Head.Answer((int)Constants.Errors.TeachExecTask,e.Message);
			}

			con.Close();

			return Task.CompareResults(userResult, teacherResult);
		}

	}
}