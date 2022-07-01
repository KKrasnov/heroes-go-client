using System;
using System.Threading;

namespace Assets.Scripts.Async
{
	public class AsyncJob<TResult>
	{
		private readonly object _lockDone = new object();
		private bool _isDone = false;
		public bool IsDone
		{
			get
			{
				bool isDone;
				lock (_lockDone)
				{
					isDone = _isDone;
				}
				return isDone;
			}
			set
			{
				lock (_lockDone)
				{
					_isDone = value;
				}
			}
		}

		private readonly object _lockResult = new object();
		private TResult _result = default(TResult);
		public TResult Result
		{
			get
			{
				TResult result;
				lock (_lockResult)
				{
					result = _result;
				}
				return result;
			}
			private set
			{
				lock (_lockResult)
				{
					_result = value;
				}
			}
		}

		private readonly object _lockException = new object();
		private Exception _exception = null;
		public Exception Exception
		{
			get
			{
				Exception exception;
				lock (_lockException)
				{
					exception = _exception;
				}
				return exception;
			}
			private set
			{
				lock (_lockException)
				{
					_exception = value;
				}
			}
		}

		private readonly Func<TResult> _function;

		public AsyncJob(Func<TResult> function)
		{
			_function = function;
		}

		public void Do()
		{
			IsDone = false;
			Result = default(TResult);

			ThreadPool.QueueUserWorkItem(DoWork);
		}

		private void DoWork(object state)
		{
			try
			{
				Result = _function();
			}
			catch (Exception ex)
			{
				Exception = ex;
			}
			finally
			{
				IsDone = true;
			}
		}
	}
}