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
	public class StatisticService
	{
		private IRepositoryTask _repositoryTask;
		private IRepositoryCompletedTask _repositoryCompletedTask;

		public StatisticService(IRepositoryTask repositoryTask, IRepositoryCompletedTask repositoryCompletedTask)
		{
			_repositoryTask = repositoryTask;
			_repositoryCompletedTask = repositoryCompletedTask;
		}

		public List<bl.Task> GetStatistic()
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
	}
}