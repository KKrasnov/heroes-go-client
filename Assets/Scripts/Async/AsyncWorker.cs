using System;
using UnityEngine;
using Assets.Scripts.UI;

namespace Assets.Scripts.Async
{
	public class AsyncWorker<TResult>
	{
		private AsyncJob<TResult> _asyncJob = null;
		private Spinner _spinner;
		private Action<TResult> _processResultCallback;
		private Action<Exception> _processExceptionCallback;

		public void StartJob(Func<TResult> job, Action<TResult> processResultCallback, Spinner spinner)
		{
			StartJob(job, processResultCallback, null, spinner);
		}

		public void StartJob(Func<TResult> job, Action<TResult> processResultCallback, Action<Exception> processExceptionCallback, Spinner spinner)
		{
			_spinner = spinner;
			_processResultCallback = processResultCallback;
			_processExceptionCallback = processExceptionCallback;
			_asyncJob = new AsyncJob<TResult>(job);
			_asyncJob.Do();
			if (_spinner != null)
			{
				_spinner.IsActive = true;
			}
		}

		public void CheckAndProcessResult()
		{
			if (_asyncJob != null && _asyncJob.IsDone)
			{
				if (_spinner != null)
				{
					_spinner.IsActive = false;
				}

				TResult result = _asyncJob.Result;
				Exception exception = _asyncJob.Exception;
				_asyncJob = null;

				if (exception == null)
				{
					if (_processResultCallback != null)
					{
						_processResultCallback(result);
					}
				}
				else
				{
					if (_processExceptionCallback == null)
					{
						Debug.LogException(exception);
					}
					else
					{
						_processExceptionCallback(exception);
					}
				}
			}
		}
	}
}